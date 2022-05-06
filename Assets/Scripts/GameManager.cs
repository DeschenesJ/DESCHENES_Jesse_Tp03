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

    // Valeur pour activer d?sactiver coroutine
    bool isRoutineStared;

    // Variables de combats
    private bool isPlayerTurn;
    private bool isFighting;

    public bool IsPlayerTurn { get { return isPlayerTurn; } set { isPlayerTurn = value; } }
    public bool IsFighting { get { return isFighting; } set { isFighting = value; } }

    // Start is called before the first frame update
    void Start()
    {
        isPlayerTurn = true;
        isFighting = false;
        isRoutineStared = false;
        playerAnimator = GetComponent<Animator>();
        camExploration = GetComponent<GameObject>();
        camCombat = GetComponent<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isFighting == true && isRoutineStared == false)
        {
            isRoutineStared = true;
            StartCoroutine(StartCombat());
        }
        else if (isFighting == false && isRoutineStared == true)
        {
            StopAllCoroutines();
            isRoutineStared = false;
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
