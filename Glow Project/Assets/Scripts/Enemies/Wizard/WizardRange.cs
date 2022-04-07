using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardRange : MonoBehaviour
{
    //RUNTIME
    [SerializeField] private Animator wizardAnimator;
    private bool isOnRange = false;
    private bool isOut = true;
    private bool isReallyDead = false;

    //GETTER y SETTER
    public bool GetWRisOnRange(){
        return isOnRange;
    }

    public void SetWRisReallyDead(bool status){
        isReallyDead = status;
    }

    //FUNCTIONS
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player") && !isReallyDead){
            wizardAnimator.SetBool("isRange", true);
            isOut = false;
            Invoke("OnRangeSet", 2f);
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player") && !isReallyDead){
            isOnRange = false;
            isOut = true;
            wizardAnimator.SetBool("isRange", false);
            wizardAnimator.SetBool("isToAttack", false);
        }
    }

    private void OnRangeSet(){
        if(!isOut){
            isOnRange = true;
        }
    }
}
