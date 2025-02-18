using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnimator;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void NextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+1));
    }


    IEnumerator LoadLevel(int levelIndex)
    {
        Gamepad.current.SetMotorSpeeds(0.1f, 0.8f);
        transitionAnimator.SetTrigger("EndScene");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(levelIndex);
        transitionAnimator.SetTrigger("StartScene");
        Gamepad.current.SetMotorSpeeds(0f, 0f);
    }
}
