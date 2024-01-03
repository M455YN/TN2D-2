using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;
    public static DataPersistenceManager instance { get; private set; }
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistancve Managager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName/*, useEncryption*/);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // load any saved data from a file using the data handler
        this.gameData = dataHandler.Load(/*selectedProfileId*/);

        //if no data can be loaded, initialize to a new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found. initializing data to defaults.");
            NewGame();
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        // save that data to a file using the data handler
        dataHandler.Save(gameData/*, selectedProfileId*/);

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        // FindObjectsofType takes in an optional boolean to include inactive gameobjects
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
