using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    public static DataPersistenceManager instance { get; private set; }

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
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //if no data can be loaded, initialize to a new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found. initializing data to defaults.");
            NewGame();
        }
    }

    public void SaveGame()
    {

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
