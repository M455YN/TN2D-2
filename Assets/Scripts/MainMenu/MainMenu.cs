using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "SampleScene"; // Name of the scene to load
    public GameObject optionsMenu; // Reference to the options menu object

    // Settings variables
    public float defaultVolume = 0.5f; // Default volume
    public int defaultQuality = 2; // Default graphics quality

    // UI elements for options menu
    public Slider volumeSlider;
    public Dropdown qualityDropdown;

    private void Start()
    {
        // Set default values for settings on game start
        PlayerPrefs.SetFloat("Volume", defaultVolume);
        PlayerPrefs.SetInt("Quality", defaultQuality);
        ApplySettings();
    }

    public void OnNewGameClick()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void OnContinueGameClick()
    {

    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true); // Activate the options menu
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false); // Deactivate the options menu
    }

    public void ApplySettings()
    {
        // Apply volume settings
        float volume = PlayerPrefs.GetFloat("Volume", defaultVolume);
        // Add code here to apply volume settings in the game, for example using AudioListener.volume

        // Apply graphics quality settings
        int quality = PlayerPrefs.GetInt("Quality", defaultQuality);
        QualitySettings.SetQualityLevel(quality);
    }

    public void SaveSettings()
    {
        // Save settings to PlayerPrefs
        float volume = volumeSlider.value;
        int quality = qualityDropdown.value;

        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("Quality", quality);
        PlayerPrefs.Save();
        ApplySettings(); // Apply changes immediately
    }

    public void SetDefaultSettings()
    {
        // Set default values for volume and quality
        PlayerPrefs.SetFloat("Volume", defaultVolume);
        PlayerPrefs.SetInt("Quality", defaultQuality);
        PlayerPrefs.Save();
        ApplySettings(); // Apply changes immediately
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
