using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSplashScreen : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName; // Nazwa nastêpnej sceny po zakoñczeniu wideo

    void Start()
    {
        // Ustawienie obs³ugi zdarzenia zakoñczenia odtwarzania wideo
        videoPlayer.loopPointReached += EndReached;

        // Rozpoczêcie odtwarzania wideo
        videoPlayer.Play();
    }

    // Funkcja wywo³ywana po zakoñczeniu odtwarzania wideo
    void EndReached(VideoPlayer vp)
    {
        // Za³aduj nastêpn¹ scenê po zakoñczeniu wideo
        SceneManager.LoadScene(nextSceneName);
    }
}
