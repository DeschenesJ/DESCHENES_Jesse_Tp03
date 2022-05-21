using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    //Variable qui va déterminer si le prochain ennemi peut apparaître
    public static bool isSpawnTime;
    
    // Le menu de transition entre les combats
    public GameObject menuTransition;
    //Le menu de combat peut-être le faire dnas le UI manager si j'ai le temps
    public GameObject menuCombat;
    // Le GameObject du joueur
    public GameObject joueur;
    // Le script du joueur
    private Player scriptJoueur;

    // le spawnpoint des ennemis
    public Transform ennemiPositionner;
    // les types d'ennemies
    public GameObject ennemiPaladin;
    //Le script de l'ennemi
    private Ennemi scriptEnnemi;

    // Variable qui détermine si y a un combat
    public static bool isFightOn;
    // La variable qui détermine quel combat que le joueur fait. Je vais peut-être la changer en public static
    [SerializeField]
    private float vagueCombat;
    public float VagueCombat { get {return vagueCombat; } set { vagueCombat = value; } }

    // Valeur pour activer d?sactiver coroutine
    private bool isRoutineStarted;
    public bool IsRoutineStarted { get { return isRoutineStarted; } }

    // determine c'est le tour de qui
    private bool isPlayerTurn;
    //private bool isEnnemiTurn;

    public bool IsPlayerTurn { get { return isPlayerTurn; } set { isPlayerTurn = value; } }

    // Start is called before the first frame update
    void Start()
    {
        isSpawnTime = false;
        //ennemi = FindObjectOfType<Ennemi>();
        vagueCombat = 1f;
        isPlayerTurn = true;
      //  isEnnemiTurn = false;
        isRoutineStarted = false;
        // Va chercher l'animator du gameobject joueur et son script
        //animatorJoueur = joueur.GetComponent<Animator>();
        scriptJoueur = joueur.GetComponent<Player>();
        

        // La position d'apparition de l'ennemi
        ennemiPositionner = ennemiPositionner.GetComponent<Transform>();
        EnnemiSpawn(ennemiPaladin);
        // Je vais faire une coroutine qui spawn les ennemis seulement au début du combat
        // et dans cette coroutine je vais mettre une distance en x par rapport au spawner pour avoir plusieur ennemis.
        // Ça ne devrait pas être compliqué, puisque les ennemis et encounters ne sont pas randoms et c'est un nombre fixe
        // de combats avant le boss et le boss ou la mort du joueur termine la partie.
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifie si l'ennemi dois apparaître
        if (vagueCombat > 1 && isSpawnTime == true)
            Spawner();

        if (isFightOn == true && scriptEnnemi == true)
        {
            
            // vérifie si le joueur attaque
            if (Player.isPlayerAtk == true)
            {
                Ennemi.isEnnemiHit = true;
                Player.isPlayerAtk = false;
            }
            else if (Ennemi.isEnnemiAtk == true)
            {
                Player.isPlayerHit = true;
                Ennemi.isEnnemiAtk = false;
                PlayerTurnRestored();
            }

            // va vérifier si quelqu'un se prend des dégâts
            if (Player.isPlayerHit == true)
                scriptJoueur.TakeDamage(Player.isPlayerHit);

            if (Ennemi.isEnnemiHit == true)
                scriptEnnemi.TakeDamage(Ennemi.isEnnemiHit);

            //vérifie si la partie est terminé et que l'ennemi est vaincu
            if (vagueCombat == 11)
                GameOver();
            if (Player.joueurAnimator.GetBool("IsDefeated") == true)
                GameOver();
            if (Ennemi.ennemiAnimator == true)
            {
                if (Ennemi.ennemiAnimator.GetBool("IsDefeated") == true)
                { isFightOn = false;
                    //Ajouter une condition si je suis à la 10e vague
                    if (vagueCombat < 11)
                    {
                        Ennemi.ennemiAnimator.SetBool("IsDefeated", false);
                        StartCoroutine(Transition());
                        // Va détruire l'ennnemi s'il le trouve
                        Destroy(FindObjectOfType<Ennemi>().gameObject, 5f);
                        
                    }
                    else
                    {
                        menuCombat.SetActive(false);
                        GameOver();
                    }
                }
            }
        }

    }

    // Méthode qui redonne le tour au joueur
    public void PlayerTurnRestored()
    {
        //Le joueur reprend son tours
        isPlayerTurn = true;
        Player.isActing = true;
    }

    // Va servir à déterminer le type d'ennemi qui apparait
    void Spawner()
    {
        Debug.Log("bonjour");
        //if (vagueCombat <= 3)
            EnnemiSpawn(ennemiPaladin);
        //if (vagueCombat > 3 && vagueCombat <= 6)
        //  EnnemiSpawn(ennemiPlusfort)
        //if (vagueCombat > 6 && vagueCombat < 10)
        // EnnemiSpawn(EnnemiEncorePlusFort)
        //if (vagueCombat == 10)
        // EnnemiSpawn(LeBoss)

        isSpawnTime = false;
    }

    // Méthode pour faire apparaître les types d'ennemis
    void EnnemiSpawn(GameObject ennemiType)
    {
        // fait apparaitre un préfab de l'ennemi désiré
        GameObject objEnnemi = Instantiate(ennemiType, ennemiPositionner.position, Quaternion.Euler(0f, 180f, 0f)).gameObject;
        // détermine la cible de l'ennemi
        scriptEnnemi = objEnnemi.GetComponent<Ennemi>();
        isFightOn = true;
        // Va chercher l'animator du gameobject de l'ennemi et son script
        //animatorEnnemi = objEnnemi.GetComponent<Animator>();

    }
    // Méthode qui détermine si la partie se termine sur une victoire ou sur un échec
    void GameOver()
    {
        StopAllCoroutines();
        isFightOn = false;
        if (Player.joueurAnimator.GetBool("IsDefeated") == true)
        {
            Debug.Log("vous avez perdu");
            //open scene accueil
        }
        else if (vagueCombat == 11)
        {
            Debug.Log("Vous avez gagnez!");
            //open scene Accueil
        }
        
    }

    // Coroutine pour le lapse de temps de la transition
    public IEnumerator Transition()
    {
        yield return new WaitForSeconds(6f);
        menuTransition.SetActive(true);
    }
    // Coroutine des combats va me permettre de faire apparaitre les mobs et possiblement faire les transitions entre les combats
    public IEnumerator Spawn()
    {
        // Pour le moment c'est une valeur bidon que je retourne afin de ne pas avoir de problème de code
        yield return new WaitForSeconds(0.001f);

    }

}
