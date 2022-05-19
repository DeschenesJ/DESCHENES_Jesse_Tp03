using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject menuPause;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isGamePaused)
                ResumeGame();
            else
                PauseGame();

        }
    }

    public void ResumeGame()
    {
        menuPause.SetActive(false);
        if(InterfaceTransition.isTimePaused == false)
            Time.timeScale = 1f;
        isGamePaused = false;

    }

    void PauseGame()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    
    
    }

    public void MenuSons()
    {


    }

    public void Quitter()
    { 
    
    
    
    }

}
