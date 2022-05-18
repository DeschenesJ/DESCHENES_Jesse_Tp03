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

    // détermine si la coroutine Attack est en cours
    bool isRoutined;

    // Start is called before the first frame update
    void Start()
    {
        isRoutined = false;
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

    void Update()
    {
        // Pour des besoins de test, j'ai fait en sorte que je peux attacker avec la touche A
        if (Input.GetKeyDown("a") && joueurAnimator.GetBool("IsAttacking") == false && isRoutined == false)
        {
            StartCoroutine(Attack());
            //joueurAnimator.SetBool("IsAttacking", true);
        }
        if (isRoutined == true)
        { 
            StopCoroutine(Attack());
            isRoutined = false;
        }
    }

    IEnumerator Attack()
    {
        isRoutined = true;
        joueurAnimator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.01f);
        joueurAnimator.SetBool("IsAttacking", false);
        


    }

}
