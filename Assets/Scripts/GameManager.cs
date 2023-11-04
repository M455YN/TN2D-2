using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isPaused;
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1; // Zatrzymaj czas lub wznow jego dzia³anie
                                           // Dodaj tutaj kod do wyœwietlenia lub ukrycia interfejsu pauzy

        //SceneManager.LoadScene("MainMenu");
    }
}
