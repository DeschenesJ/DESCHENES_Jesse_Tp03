using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // L'animator du joueur
    public Animator playerAnimator;
    // Les cam?ras de combat et hors combat
    public GameObject camExploration;
    public GameObject camCombat;

    public CardManager cardManager;


    // Valeur pour activer d?sactiver coroutine
    private bool isRoutineStarted;
    
    public bool IsRoutineStarted { get { return isRoutineStarted; } }

    // d?termine c'est le tour ? qui
    private bool isPlayerTurn;
    // d?termine s'il y a un combat ou non
    private bool isFighting;

    public bool IsPlayerTurn { get { return isPlayerTurn; } set { isPlayerTurn = value; } }
    public bool IsFighting { get { return isFighting; } set { isFighting = value; } }

    // Start is called before the first frame update
    void Start()
    {
        // Va chercher le CardManager dans la sc?ne
        cardManager = GetComponent<CardManager>();

        isPlayerTurn = true;
        isFighting = false;
        isRoutineStarted = false;
        playerAnimator = GetComponent<Animator>();
        camExploration = GetComponent<GameObject>();
        camCombat = GetComponent<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isFighting == true && isRoutineStarted == false)
        {
            isRoutineStarted = true;
            StartCoroutine(StartCombat());
        }
        else if (isFighting == false && isRoutineStarted == true)
        {
            isRoutineStarted = false;
            StopAllCoroutines();
        }

    }

    IEnumerator StartCombat()
    {
        // Change la cam?ra
        camExploration.SetActive(false);
        camCombat.SetActive(true);

        // Commence l'animation de combat
        playerAnimator.SetBool("CanMove", false);
        playerAnimator.SetBool("IsFighting", true);


        yield return true;
    
    
    }
}
