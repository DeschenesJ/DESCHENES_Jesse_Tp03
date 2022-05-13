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

    public Carte strike;

    private List<Carte> lstCardType;
    private List<Carte> lstAllCards;
    private List<Carte> lstdeckZone;
    private List<Carte> lsthandZone;
    private List<Carte> lstdiscardZone;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
