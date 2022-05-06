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
    float speedInterval;
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
        if (passiveOrAttack.GetBool("IsFighting") == true && passiveOrAttack.GetBool("CanMove") == false)
        {
            // peut-être les placer dans une fonction si nécessaire 
            speedInterval = 0.5f;
            isActive = true;
            StartCoroutine(SwordSwitchDelay());
        }
        else
        {
            speedInterval = 1f;
            isActive = false;
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
