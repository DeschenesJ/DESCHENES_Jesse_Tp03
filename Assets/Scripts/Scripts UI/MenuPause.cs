using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject menuPause;
    public GameObject menuCombat;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && UI_GameOver.isGameOver == false)
        {
            if (isGamePaused)
                ResumeGame();
            else
                PauseGame();

        }
    }

    //M�thode qui r�sume le jeu
    public void ResumeGame()
    {
        menuPause.SetActive(false);
        if(InterfaceTransition.isTimePaused == false)
            Time.timeScale = 1f;
        isGamePaused = false;
        if(InterfaceTransition.isTimePaused == false)
            menuCombat.SetActive(true);
    }

    //M�thode qui met le jeu sur pause
    void PauseGame()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        menuCombat.SetActive(false);
    
    }

    //M�thode pour le bouton du menu audio
    public void MenuAudio()
    {


    }

    //M�thode pour le bouton Quitter
    public void Quitter()
    { 
    
    
    
    }

}
