using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public float fateDuration = 0.5f;
    public float displayImageDuration = 1f;

    private bool isPlayerAtExit;
    public GameObject player;
    private float timer;

    public CanvasGroup exitBackgroundImageCanvasGroup;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            isPlayerAtExit = true;
            StartCoroutine(Fade(0, 1, fateDuration));
        }
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float startTime = Time.time;
        while(Time.time - startTime < duration)
        {
            exitBackgroundImageCanvasGroup.alpha = Mathf.Lerp(from, to, (Time.time - startTime) / duration);
            yield return 0;

        }
        yield return new WaitForSeconds(duration * 2);
        EndLevel();
    }

    void EndLevel()
    {
        Debug.Log("Fin del juego");
        Application.Quit();
    }
}
