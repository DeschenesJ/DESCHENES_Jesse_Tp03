using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    


    // variable de la carte qui vont être manipuler par le print de la variable card
    public Text actionCostText;
    public Text nameText;
    public Text effectText;

    public Image artworkImg;

    private float attackValue;
    private float defenseValue;
    private float healValue;



    // Méthode qui instancie le script Carte dans CardDisplay
    public void setCard(Carte card)
    {
        nameText.text = card.nomCarte;
        actionCostText.text = card.actionCost.ToString();
        effectText.text = card.effetCarte;
        artworkImg.sprite = card.imageCarte;

        attackValue = card.attack;
        defenseValue = card.defense;
        healValue = card.heal;

        Debug.Log($"attaque : {attackValue}; defense : {defenseValue}; healing : {healValue}");
    }

}
