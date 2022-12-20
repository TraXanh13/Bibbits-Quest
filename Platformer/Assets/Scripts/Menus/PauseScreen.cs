using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI;

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused) {
                resume();
                isPaused = false;
            } else {
                paused();
                isPaused = true;
            }
        }
    }

    public void resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void paused(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void quit(){
        Debug.Log("Quit");
        Application.Quit();
    }
}
