using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject optionsPanel;

    public Slider volumeSlider;
    public TMP_Dropdown graphicsDropdown;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);

        volumeSlider.onValueChanged.AddListener(SetVolume);
        graphicsDropdown.onValueChanged.AddListener(SetGraphicsQuality);

        // Initialize graphics dropdown
        graphicsDropdown.ClearOptions();
        graphicsDropdown.AddOptions(new System.Collections.Generic.List<string>(QualitySettings.names));
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.RefreshShownValue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!");
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetGraphicsQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
