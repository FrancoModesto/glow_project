using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    //RUNTIME
    [SerializeField] private Button continueButton;

    //FUNCTIONS
    void Update()
    {
        if(GameManager.instance.actualLvl != 0){
            continueButton.interactable = true;
        } else{
            continueButton.interactable = false;
        }
    }
}
