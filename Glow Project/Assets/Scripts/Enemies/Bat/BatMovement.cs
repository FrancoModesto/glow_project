using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    //DESIGN
    [SerializeField] private float eSpeed = 0f;
    [SerializeField] private float speedToLook = 0f;
    [SerializeField] private float batRange = 0f;

    //RUNTIME
    private CharacterController ccBat;
    private GameObject player;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private AudioSource audioBat;
    [SerializeField] private AudioClip flySound;
    private float seVol = 1f;

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        ccBat = GetComponent<CharacterController>();
        player = GameObject.Find("Player");
        
        originalPos = transform.position;
        originalRot = transform.rotation;

        audioBat = GetComponent<AudioSource>();
        audioBat.loop = true;
        audioBat.clip = flySound;
        audioBat.volume = 1f * seVol;
        audioBat.Play();
    }

    void Update()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
            audioBat.volume = 1f * seVol;
        }

        if(!player.GetComponent<PlayerCollision>().GetPCwin() && !player.GetComponent<PlayerCollision>().GetPCisDead()){
            MoveTowardsPlayer();
            LookAtPlayerLerp();
        }
    }

    private void MoveTowardsPlayer(){
        Vector3 vector = player.transform.position - transform.position;
        Vector3 direction = vector.normalized;
        if(vector.magnitude <= batRange){
            ccBat.Move(direction * eSpeed * Time.deltaTime);
        } else{
            Vector3 vectorBack = originalPos - transform.position;
            Vector3 directionBack = vectorBack.normalized;
            if(vectorBack.magnitude >= 1){
                ccBat.Move(directionBack * eSpeed * Time.deltaTime);
            }
        }
    }

    private void LookAtPlayerLerp(){
        Vector3 vector = player.transform.position - transform.position;
        if(vector.magnitude <= batRange){
            Quaternion newRotation = Quaternion.LookRotation(vector);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime);
        } else{
            Vector3 vectorBack = originalPos - transform.position;
            if(vectorBack.magnitude >= 1){
                Quaternion newRotationBack = Quaternion.LookRotation(vectorBack);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotationBack, speedToLook * Time.deltaTime);
            } else{
                transform.rotation = Quaternion.Lerp(transform.rotation, originalRot, speedToLook * Time.deltaTime);
            }
        }
    }
}
