using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    // Pv du joueur
    private float joueurPV;

    public float JoueurPV { get {return joueurPV ; } set {joueurPV = value ; } }

    // la résistance au dégâts du joeur
    private float joueurRes;

    // Va chercher le rigid body
    private Rigidbody rb;

    // Va chercher l'animator du Joueur
    private Animator joueurAnimator;


    // Start is called before the first frame update
    void Start()
    {
        // Initialisation des Pv et de la résistance aux dégats du joueur
        joueurPV = 50f;
        joueurRes = joueurPV*0.1f;
        Debug.Log(joueurPV);
        Debug.Log(joueurRes);
        // Assigne l'animator du joueur pour ses animations de combat
        joueurAnimator = GetComponent<Animator>();
        // Assigne le rigidbody
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
       


    }

}
