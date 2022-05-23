using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceCombat : MonoBehaviour
{
    // Le bouton pour la fin des tours
    public GameObject btnFinTour;
    // Détermine si le pouton fin est actif. Le script ennemi va le remettre à true lorsque la coroutine attack approche de sa fin
    public static bool isFinActive = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    // La methode pour le bouton d'attaque du joueur
    public void Attaque()
    {
        if (Player.isActing == true && GameManager.isPlayerTurn == true)
        {
            Player.isActing = false;
            Player.isPlayerAtk = true;

        }
        else // Je vais le mettre dans le message d'update du combat manager
            Debug.Log(Player.isActing);
    }

    // Methode pour le bouton de soin du personnage
    public void Soins()
    {
        if (Player.isActing == true && Player.joueurPV < Player.joueurPVMax && Player.joueurPV > 0 && GameManager.isPlayerTurn == true)
        {
            Player.isActing = false;
            Player.isHealing = true;
        
        }
        else
            Debug.Log("You don't need to heal");

    }

    // Methode pour le bouton qui va permettre au joueur de quintupler son attaque
    public void AttaquePlus()
    {
        if (Player.isActing == true && Player.isBuffingAtk == false && Player.isAttackBuffed == false)
        {
            Player.isActing = false;
            Player.isBuffingAtk = true;
        }
        else if (!Player.isActing == true)
            Debug.Log("Vous avez déjà utiliser le buff d'atk");
        else
            Debug.Log("Veuillez appuyer sur fin de tour");
            
        
    }

    public void ResistancePlus()
    {
        if (Player.isActing == true && Player.isBuffingRes == false && Player.isResistanceBuffed == false)
        {
            Player.isActing = false;
            Player.isBuffingRes = true;

        }
        else if (!Player.isActing == true)
            Debug.Log($"Vous avez déjà utilisé le buff de res");
        
        
        
    }
    

    // Methode pour le bouton de fin de tour
    public void FinTour()
    {
        if (FindObjectOfType<Ennemi>() == true && isFinActive == true)
        {
            if (GameManager.isPlayerTurn == true)
            {
                if (Player.isActing == true)
                    Player.isActing = false;
                GameManager.isPlayerTurn = false;
                isFinActive = false;
            }
            else if (GameManager.isPlayerTurn == false)
                Debug.Log("Ce n'est pas votre tour");
        }
    }
}
