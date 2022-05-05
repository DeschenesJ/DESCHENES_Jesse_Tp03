using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    //Variables de combats et Pv
    public GameObject armeJoueurSheath;
    public GameObject armeJoueurAtk;
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
        StartCoroutine(combatActive(isActive));
    }

    // Update is called once per frame
    void Update()
    {
        if (passiveOrAttack.GetBool("IsFighting") == true && passiveOrAttack.GetBool("CanMove") == false)
        {
            speedInterval = 0.5f;
            isActive = true;

        }
        else
        {
            speedInterval = 1f;
            isActive = false;
        }


    }

    //void combatActive(bool isActive)
    //{

    //    armeJoueurSheath.SetActive(!isActive);
    //    armeJoueurAtk.SetActive(isActive);
    //}
    IEnumerator combatActive(bool isActive)
    {
        yield return new WaitForSeconds(speedInterval);
        armeJoueurSheath.SetActive(!isActive);
        armeJoueurAtk.SetActive(isActive);

        // Au lieu de faire ça, je crois que je vais faire un script sur mon arme qui ne la fait que s'activer ou se désactiver
    }
}
