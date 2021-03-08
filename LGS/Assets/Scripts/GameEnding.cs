using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fateDuration = 0.5f;
    public float displayImageDuration = 1f;

    private bool isPlayerAtExit;
    private bool isPlayerCaught;
    public GameObject player;
    private float timer;

    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;

    public AudioSource exitAudio, caughtAudio;
    private bool hasAudioPlayed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            if(this.transform.tag == "Finish")
            {
                isPlayerAtExit = true;
                StartCoroutine(Fade(0, 1, fateDuration, exitBackgroundImageCanvasGroup, false, exitAudio));
            }
            
        }
    }

    public void CaughtEndGame()
    {
        isPlayerCaught = true;
        StartCoroutine(Fade(0, 1, fateDuration, caughtBackgroundImageCanvasGroup, true, caughtAudio));
    }

    /// <summary>
    /// Lanza la imagen de fin de partida
    /// </summary>
    /// <param name="from">De donde parte el alpha</param>
    /// <param name="to">A donde llega el alpha</param>
    /// <param name="duration">Cuanto tarda en llegar de from to to</param>
    /// <param name="cg">Imagen de fin de partida correspondiente</param>
    /// <returns></returns>
    IEnumerator Fade(float from, float to, float duration, CanvasGroup cg, bool doRestart, AudioSource audioSource)
    {
        if(!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;

        }

        float startTime = Time.time;
        while(Time.time - startTime < duration)
        {
            cg.alpha = Mathf.Lerp(from, to, (Time.time - startTime) / duration);
            yield return 0;

        }
        yield return new WaitForSeconds(duration * 2);
        EndLevel(doRestart);
    }



    void EndLevel(bool doRestart)
    {
        Debug.Log("Fin del juego");
        if(doRestart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            isPlayerCaught = false;
            Application.Quit();
        }
    }
}
