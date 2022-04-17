using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossTrigger : MonoBehaviour
{
    //RUNTIME
    [SerializeField] private GameObject secondBoss;
    [SerializeField] private GameObject trapsTrigger;
    private bool firstTime = true;

    //FUNCTIONS
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player") && firstTime){
            firstTime = false;
            secondBoss.SetActive(true);
            Invoke("DelayedTrapsTrigger", 0.5f);
        }
    }

    private void DelayedTrapsTrigger(){
        trapsTrigger.SetActive(true);
    }
}
