using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    // Variable qui di que la partie est termin�
    public static bool isGameOver = false;
    
    // Le menu de
    public GameObject menuGameOver;
    public Text txt_GameOver;



    // Update is called once per frame
    void Update()
    {
        if (isGameOver == true && Player.isDefeated == true)
            StartCoroutine(openGameOverDelay());
        if (isGameOver == true && GameManager.vagueCombat == 11)
            StartCoroutine(openGameOverDelay());
    }

    // M�thode du bouton de retour � l'accueil
    public void Quitter()
    {
        //Charger la sc�ne d'accueil
        SceneManager.LoadScene("Accueil");
    }

    // Coroutine pour ouvrir le menu gameOver avec un d�lais
    IEnumerator openGameOverDelay()
    {
        yield return new WaitForSeconds(2f);
        menuGameOver.SetActive(true);
        if(Player.isDefeated == true)
            FindObjectOfType<UI_Manager>().UImessages(txt_GameOver, 3);
        else
            FindObjectOfType<UI_Manager>().UImessages(txt_GameOver, 4);
        Time.timeScale = 0f;
        StopCoroutine(openGameOverDelay());
    }
}
