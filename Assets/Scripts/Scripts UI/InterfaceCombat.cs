using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceCombat : MonoBehaviour
{
        
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
        if (Player.isActing == true && FindObjectOfType<GameManager>().IsPlayerTurn == true)
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
        if (FindObjectOfType<Ennemi>() == true)
        {
            if (FindObjectOfType<GameManager>().IsPlayerTurn == true)
            {
                if (Player.isActing == true)
                    Player.isActing = false;
                FindObjectOfType<GameManager>().IsPlayerTurn = false;
            }
            if (FindObjectOfType<GameManager>().IsPlayerTurn == false)
                Debug.Log("Ce n'est pas votre tour");
        }
    }
}
