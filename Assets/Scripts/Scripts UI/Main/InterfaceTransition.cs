using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceTransition : MonoBehaviour
{
    // Va chercher le menu de transition
    public GameObject menuTransition;
    public GameObject menuCombat;
    public static bool isTimePaused = false;


    // Update is called once per frame
    void Update()
    {
        if (menuTransition.activeSelf == true && isTimePaused == false)
        { 
            Time.timeScale = 0f;
            isTimePaused = true;
            StopCoroutine(FindObjectOfType<GameManager>().Transition());
            menuCombat.SetActive(false);
            Debug.Log("Transition");
        }
        
    }

    // Fonction qui sera utilisé pour le bouton confirmer de l'interface de transition, afin que le joueur puisse confirmer son choix de buffs ou de soins
    public void Confirme()
    {

        isTimePaused = false;
        Time.timeScale = 1f;
        menuTransition.SetActive(false);
        menuCombat.SetActive(true);
        GameManager.isFightOn = true;
        GameManager.isSpawnTime = true;
        FindObjectOfType<GameManager>().PlayerTurnRestored();
    }
}
