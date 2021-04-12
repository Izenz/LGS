using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool g_GameIsPaused = false;

    const int k_MenuSceneIndex = 0;

    public GameObject m_PauseUI;
    public GameObject m_PauseSettingsUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (g_GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        m_PauseUI.SetActive(false);
        m_PauseSettingsUI.SetActive(false);
        Time.timeScale = 1f;
        g_GameIsPaused = false;
    }

    public void Pause()
    {
        m_PauseUI.SetActive(true);
        Time.timeScale = 0f;
        g_GameIsPaused = true;
    }

    public void Quit()
    {
        Resume();
        SceneManager.LoadScene(k_MenuSceneIndex);
    }

    public void OpenSettings()
    {
        m_PauseUI.SetActive(false);
        m_PauseSettingsUI.SetActive(true);
    }
}
