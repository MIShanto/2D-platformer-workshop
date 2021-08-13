using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Animator animator;
    public void PlayButtonPressed()
    {
        animator.SetTrigger("Transition"); // 0.7 sec wait. 

        Invoke("Load", 0.7f);
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    void Load()
    {
        SceneManager.LoadScene(1);
    }
}
