using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    // Les UI qui vont être gérés par le UI manager
    public GameObject menuPause;
    public GameObject menuCombat;
    public GameObject menuTransition;
    public GameObject menuGameOver;
    public GameObject menuAudio;

    // Les valeur modifiables des UI notemment le UI de combat
    public Text txt_turn;
    public Text txt_PvJoueur;
    public Text txt_PvEnnemi;

    // Variable qqui vérifie si la partie est terminée
    bool isGameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        
        // Initialisation des menus
        menuCombat.SetActive(true);
        menuPause.SetActive(false);
        menuTransition.SetActive(false);
        menuGameOver.SetActive(false);
        menuAudio.SetActive(false);
        // Par défaut c'est le tour du joueur
        UImessages(txt_turn, 0);
        UIupdatePV(txt_PvJoueur, Player.joueurPV, Player.joueurPVMax);
    }

    // Update is called once per frame
    void Update()
    {
        // les mises à jour ne se fon que lorsque la partie est en cours
        if (isGameOver == false)
        {
            if (GameManager.isPlayerTurn == true)
                UImessages(txt_turn, 0);
            else if (GameManager.isPlayerTurn == false && Ennemi.isTurnEnd == false)
                UImessages(txt_turn, 1);

        }
        // Lorsque la partie est terminé
        if (UI_GameOver.isGameOver == true && isGameOver == false)
        {
            menuCombat.SetActive(false);
            menuPause.SetActive(false);
            menuTransition.SetActive(false);
            isGameOver = true;
        }


    }

    // Méthode qui détermine ce qui est affiché à l'écran comme message
    public void UImessages(Text txt_UI, int msgCheck)
    {
        // Si c'et le tour du joueur (msgCheck == 0)
        if (msgCheck == 0)
        {
            txt_UI.text = "Tour du joueur";
            return;
        }
        // Si c'est le tour ennemi (msgCheck == 1)
        if (msgCheck == 1)
        {
            txt_UI.text = "Tour Ennemi";
            return;
        }
        // si la partie se termine sur une victoire (msgCheck == 2)
        if (msgCheck == 2)
        {
            txt_UI.text = "Vous êtes Victorieux";
            txt_UI.color = Color.yellow;
            return;
        }
        // Si la partie se termine sur un échec (msgCheck == 3)
        if (msgCheck == 3)
        {
            txt_UI.text = "Félicitation! Vous êtes nul";
            txt_UI.color = Color.red;
            return;
        }
    }

    //Méthode qui met à jour les Pv du joueur ou de l'ennemi
    public void UIupdatePV(Text txt_UI, float valActive, float valMax)
    {
        txt_UI.text = $"{valActive} / {valMax}";
    }
}
