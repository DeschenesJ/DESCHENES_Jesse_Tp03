using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Le GameObject du joueur
    public GameObject joueur;
    // L'animator du joueur
    private Animator animatorJoueur;
    // Le script du joueur
    private Player scriptJoueur;
    // le spawnpoint des ennemis
    public Transform ennemiPositionner;

    // les types d'ennemies
    public GameObject ennemiPaladin;

    //Le script de l'ennemi
    private Ennemi scriptEnnemi;
    //L'animator de l'ennemi
    private Animator animatorEnnemi;

    // La variable qui détermine quel combat que le joueur fait
    [SerializeField]
    private float vagueCombat;
    public float VagueCombat { get {return vagueCombat; } }

    // Valeur pour activer d?sactiver coroutine
    private bool isRoutineStarted;
    public bool IsRoutineStarted { get { return isRoutineStarted; } }

    // d?termine c'est le tour ? qui
    private bool isPlayerTurn;
    private bool isEnnemiTurn;

    public bool IsPlayerTurn { get { return isPlayerTurn; } set { isPlayerTurn = value; } }

    // Start is called before the first frame update
    void Start()
    {

        //ennemi = FindObjectOfType<Ennemi>();
        vagueCombat = 1f;
        isPlayerTurn = true;
        isEnnemiTurn = false;
        isRoutineStarted = false;
        // Va chercher l'animator du gameobject joueur et son script
        animatorJoueur = joueur.GetComponent<Animator>();
        scriptJoueur = joueur.GetComponent<Player>();


        Debug.Log(VagueCombat);

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
        // vérifie si le joueur attaque
        if (animatorJoueur.GetBool("IsAttacking") == true)
        {
            scriptEnnemi.IsEnnemiHit = true;
        }

        // va vérifier si quelqu'un se prend des dégâts
        if (scriptJoueur.IsPlayerHit == true)
            scriptJoueur.TakeDamage(scriptJoueur.IsPlayerHit);
        
        if (scriptEnnemi.IsEnnemiHit == true)
            scriptEnnemi.TakeDamage(scriptEnnemi.IsEnnemiHit);
            
        //vérifie si la partie est terminé et que l'ennemi est vaincu
        if (vagueCombat == 11)
            GameOver();
        if (animatorJoueur.GetBool("IsDefeated") == true)
            GameOver();
        if (animatorEnnemi.GetBool("IsDefeated") == true)
            vagueCombat++;
    }
    
    // Coroutine des combats va me permettre de faire apparaitre les mobs et possiblement faire les transitions entre les combats
    IEnumerator Combats()
    {
        // Pour le moment c'est une valeur bidon que je retourne afin de ne pas avoir de problème de code
        yield return new WaitForSeconds(1f);
    }

    // Méthode pour faire apparaître les types d'ennemis
    void EnnemiSpawn(GameObject ennemiType)
    {
        // fait apparaitre un préfab de l'ennemi désiré
        GameObject objEnnemi = Instantiate(ennemiType, ennemiPositionner.position, Quaternion.Euler(0f, 180f, 0f)).gameObject;
        // détermine la cible de l'ennemi
        scriptEnnemi = objEnnemi.GetComponent<Ennemi>();
        // Va chercher l'animator du gameobject de l'ennemi et son script
        animatorEnnemi = objEnnemi.GetComponent<Animator>();
    }
    void GameOver()
    {
        StopAllCoroutines();
        if (animatorJoueur.GetBool("IsDefeated") == true)
            Debug.Log("vous avez perdu");
        if (vagueCombat == 11)
            Debug.Log("Vous avez gagnez!");
        
    }


}
