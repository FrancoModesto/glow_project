using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    //DESIGN
    [SerializeField] private float speedToLook = 0f;

    //RUNTIME
    private GameObject player;
    private AudioSource audioSlimeBoss;
    [SerializeField] private Animator slimeBossAnimator;
    [SerializeField] private GameObject shield;
    private float seVol = 1f;

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        player = GameObject.Find("Player");

        audioSlimeBoss = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
            audioSlimeBoss.volume = 1f * seVol;
        }

        if(true){
            LookAtPlayerLerp();
            ActivateShield();
        }
    }

    private void LookAtPlayerLerp(){
        Vector3 vector = player.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(new Vector3(vector.x, 0f, vector.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime);
    }

    private void ActivateShield(){
        Vector3 vector = new Vector3(0f, 0f, player.transform.position.z) - new Vector3(0f, 0f, shield.transform.position.z);
        if(vector.magnitude <= 10){
            shield.SetActive(true);
        } else{
            shield.SetActive(false);
        }
    }

}
