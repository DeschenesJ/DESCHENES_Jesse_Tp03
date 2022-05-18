using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour, IDamageable
{
    protected GameManager manager;

    protected Animator ennemiAnimator;

    // Les Pv de l'ennemi
    protected float ennemiPV;
    

    // La r�sistance de l'ennemi
    protected float ennemiRes;

    // La puissance d'attaque de l'ennemi
    protected float ennemiAtk;

    public float EnnemiPV { get { return ennemiPV; } set { ennemiPV = value; } }
    public float EnnemiRes { get { return ennemiRes; } set { ennemiRes = value; } }
    public float EnnemiAtk { get { return ennemiAtk; } }

    // Variable qui d�termine si la coroutine de l'adversaire est active ou non
    protected bool isRoutined;

    
    
    // Start is called before the first frame update
    void Start()
    {
        // Va chercher le gamemanager afin d'avoir les variables dont ce script d�pends
        manager = FindObjectOfType<GameManager>();
        // Va chercher son propre animator afin de pouvoir l'utiliser pour lui faire jouer ses animations
        ennemiAnimator = GetComponent<Animator>();
        ennemiProperties();

    }

    // Update is called once per frame
    void Update()
    {
        if (ennemiPV <= 0)
            ennemiDefeat();
    }
    // M�thode pour les propri�t�s des ennemies
    protected virtual void ennemiProperties()
    {
        // les pv, la r�sistance et l'a force d'attaque de l'ennemi varient selon la vague
        ennemiPV = 20f + (5 * manager.VagueCombat);
        ennemiRes = ennemiPV * 0.1f;
        ennemiAtk = 5f * manager.VagueCombat;
    }

    //M�thode qui s'active lorsque l'ennemi est vaincu
    protected void ennemiDefeat()
    {

        ennemiAnimator.SetBool("IsDefeated", true);
        // Je vais jouer un effet sonore et des particules lorsque l'ennemie est mort

        // Vas se d�truire lui m�me apr�s deux secondes
        Destroy(this.gameObject, 2f);

    }

    public void TakeDamage(bool isHit)
    {
        if (isHit == true)
            ennemiPV -= (FindObjectOfType<Player>().JoueurAtk-EnnemiRes);
        
    }

    private void OnMouseDown()
    {
        

    }

}
