using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool Paused;
    public GameObject PauseMenuCanvas;
    public GameObject OptionMenuCanvas;


    void Awake()
    {
        // PauseMenuCanvas = this.transform.Find("PauseCanvas").gameObject;
        // OptionMenuCanvas = this.transform.Find("PauseOption").gameObject;
        // MapCanvas = this.transform.Find("MAPCanvas").gameObject;
        //HUD = GameObject.Find("HUD");
    }
    // Start is called before the first frame update
    void Start()
    {
        
        PauseMenuCanvas.SetActive(false);
        OptionMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
            return;

        if(Input.GetKeyDown("escape"))
        {
            if (PauseMenuCanvas.activeSelf && Paused)
            {
                Play();
            }
            else if (!Paused)
            {
                Stop();
            }
            else if (Paused && OptionMenuCanvas.activeSelf && !PauseMenuCanvas.activeSelf)
            {
                CloseOptionMenu();
            }
        }

        Cursor.visible = Paused;
    }



    void Stop()
    {
        PauseMenuCanvas.SetActive(true);
        //Time.timeScale = 0f;
        Paused = true;
        // Cursor.visible = false;
    }

    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        //Time.timeScale = 1f;
        Paused = false;
        // Cursor.visible = true;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

     public void ResetButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("Game Restarted");
    }

    public void OpenOptionMenu()
    {
        PauseMenuCanvas.SetActive(false);
        OptionMenuCanvas.SetActive(true);
        Paused = true;
    }
    public void CloseOptionMenu()
    {
        PauseMenuCanvas.SetActive(true);
        OptionMenuCanvas.SetActive(false);
        Paused = true;
    }

    public bool isPaused()
    {
        return Paused;
    }
}
