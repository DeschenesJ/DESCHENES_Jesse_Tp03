using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    // Les UI qui vont être gérés par le UI manager
    public GameObject menuPause;
    public GameObject menuCombat;
    public GameObject menuTransition;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        menuCombat.SetActive(true);
        menuPause.SetActive(false);
        menuTransition.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        


    }
    
}
