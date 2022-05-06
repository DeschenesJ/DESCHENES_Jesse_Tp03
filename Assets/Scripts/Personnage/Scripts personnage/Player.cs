using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Variables de combat
    public GameObject armeJoueurSheath;
    public GameObject armeJoueurAtk;

    // Pv du joueur
    private float joueurPV;
    public float JoueurPV { get {return joueurPV ; } set {joueurPV = value ; } }

    // les points d'actions
    private float joueurPA;
    
    Animator passiveOrAttack;
    // vitesse d'interval pour la coroutine
    float speedInterval = 0.5f;
    // valeur booléenne pour la coroutine
    bool isActive = false;


    // Start is called before the first frame update
    void Start()
    {
        //persoJoueur = GetComponent<GameObject>();
        passiveOrAttack = GetComponent<Animator>();
        combatActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        // && passiveOrAttack.GetBool("CanMove") == false
        if (passiveOrAttack.GetBool("IsFighting") == true && FindObjectOfType<GameManager>().IsRoutineStarted == false)
        {
            isActive = true;
            StartCoroutine(SwordSwitchDelay());
            StopCoroutine(SwordSwitchDelay());
        }
        else if (passiveOrAttack.GetBool("IsFighting") == false && FindObjectOfType<GameManager>().IsRoutineStarted == true)
        {
            isActive = false;
            StartCoroutine(SwordSwitchDelay());
            StopCoroutine(SwordSwitchDelay());
        }


    }

    // fonction exécuté dans le start du joueur afin de masquer et afficher correctement le bon type d'épée
    void combatActive(bool isActive)
    {
        armeJoueurSheath.SetActive(!isActive);
        armeJoueurAtk.SetActive(isActive);
    }

    //coroutine pour sortir/rangé l'arme du personnage joueur
    IEnumerator SwordSwitchDelay()
    {
        yield return new WaitForSeconds(speedInterval);
        armeJoueurSheath.SetActive(!isActive);
        armeJoueurAtk.SetActive(isActive);


    }
}
