using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceCombat : MonoBehaviour
{
    // Le bouton pour la fin des tours
    public GameObject btnFinTour;
    // Détermine si le pouton fin est actif. Le script ennemi va le remettre à true lorsque la coroutine attack approche de sa fin
    public static bool isFinActive;



    // Start is called before the first frame update
    void Start()
    {
        isFinActive = true;
        
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
        if (Player.isActing == true)
        {
            Player.isActing = false;
            Player.isHealing = true;
        
        }

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
            if (GameManager.isPlayerTurn == false)
                Debug.Log("Ce n'est pas votre tour");
        }
    }
}
