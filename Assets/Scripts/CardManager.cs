using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // Va chercher le cardDisplay du gameobject de la carte
    public CardDisplay cardDisplay;

    // Va chercher le frame physique de la carte
    public GameObject CardFrame;

    public Carte strikeCard;
    public Carte guardCard;
    public Carte healCard;

    // Les listes pour les différentes zones
    private List<Carte> lstAllCards; // Liste des cartes du joueur peu importe la zone
    private List<Carte> lstdeckZone; // Liste des cartes encore non pigés dans le deck du joueur
    private List<Carte> lsthandZone; // Liste des cartes dans la main que le joueur peut jouer
    private List<Carte> lstdiscardZone; // Liste des cartes que le joueur a utilisées

    //Variable pour la variable qui détermine la taille du deck de départ
    private int startingDeck = 15;

    // Variable qui mélange les cartes du deck du joueur et lorsque les cartes retourne dans ce dernier
    private bool isDeckShuffle;

    // Variable qui détermine s'il reste des cartes dans le deck du joueur au début de son tour.
    private bool isDeckEmpty;
    
    // Start is called before the first frame update
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
