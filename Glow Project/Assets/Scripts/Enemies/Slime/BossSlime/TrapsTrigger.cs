using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsTrigger : MonoBehaviour
{
    //RUNTIME
    [SerializeField] private GameObject traps;
    private bool firstTime = true;
    private bool finish = false;

    //GETTER y SETTER
    public bool GetTTfinish(){
        return finish;
    }

    //FUNCTIONS
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player") && firstTime){
            Invoke("DelayedFirstTime", 0.5f);
        }
        if(other.gameObject.CompareTag("Player") && !firstTime && !finish){
            finish = true;
            traps.SetActive(true);
        }
    }

    private void DelayedFirstTime(){
        firstTime = false;
    }
}
