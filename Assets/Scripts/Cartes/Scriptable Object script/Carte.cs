using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//tutoriel pour le scriptable object: https://www.youtube.com/watch?v=aPXvoWVabPY

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Carte : ScriptableObject
{
    // Valeurs de la carte 
    public new string nomCarte;
    public string effetCarte;

    public Sprite imageCarte;

    public float actionCost;


    public float attack;
    public float defense;
    public float heal;

    // les référence physique de l'UI de la carte



    public void Print()
    {


    }


    // call l'enum
    //public lol potatoMan;


}

// Me permettre de choisir ce que la carte peut faire entre des valeurs donnés. Je vais juste avoir besoin de le vérifier lorsque j'utilise la carte
//[System.Serializable]
//public enum lol
//{
//    attack, defense, heal


//}
