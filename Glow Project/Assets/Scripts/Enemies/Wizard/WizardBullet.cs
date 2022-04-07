using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBullet : MonoBehaviour
{
    //DESIGN
    [SerializeField] private float speed = 0f;
    [SerializeField] private float duration = 0f;

    //RUNTIME
    private float timePass = 0f;

    //FUNCTIONS
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        timePass += Time.deltaTime;
        if(timePass >= duration){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground")){
            Destroy(gameObject);
        }
    }
}
