using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    //DESIGN
    [SerializeField] private float eSpeed = 0f;
    [SerializeField] private float speedToLook = 0f;
    [SerializeField] private float slimeRange = 0f;

    //RUNTIME
    private CharacterController ccSlime;
    private GameObject player;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private AudioSource audioSlime;
    private Vector3 velocity;
    [SerializeField] float gravity = 0f;
    [SerializeField] private AudioSource audioExtraSlime;
    [SerializeField] private Animator slimeAnimator;
    [SerializeField] private GameObject skinGreen;
    [SerializeField] private GameObject skinYellow;
    [SerializeField] private AudioClip alertSound;
    [SerializeField] private AudioClip noAlertSound;
    [SerializeField] private AudioClip runSound;
    private float seVol = 1f;
    private bool alertFirstTime = true;
    private bool noAlertFirstTime = true;
    private bool noAlertFirstTimeFix = true;
    private bool firstRun = true;


    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        ccSlime = GetComponent<CharacterController>();
        player = GameObject.Find("Player");

        originalPos = transform.position;
        originalRot = transform.rotation;

        audioSlime = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
            audioExtraSlime.volume = 1f * seVol;
        }

        if(ccSlime.enabled == true){
            velocity.y += gravity * 0.2f * Time.deltaTime;
            ccSlime.Move(velocity * Time.deltaTime);
        }

        if(ccSlime.enabled == true && !player.GetComponent<PlayerCollision>().GetPCwin() && !player.GetComponent<PlayerCollision>().GetPCisDead() && !gameObject.GetComponent<SlimeCollision>().GetSCisDead()){
            MoveAwayFromPlayer();
            DontLookAtPlayerLerp();
        }
    }

    private void MoveAwayFromPlayer(){
        Vector3 vector = player.transform.position - transform.position;
        Vector3 direction = new Vector3(-vector.normalized.x, 0f, -vector.normalized.z);
        if(vector.magnitude <= (slimeRange + 3) && vector.magnitude > slimeRange){
            if(alertFirstTime){
                alertFirstTime = false;
                noAlertFirstTime = true;
                slimeAnimator.SetBool("alert", true);
                audioSlime.PlayOneShot(alertSound, 5f * seVol);
                slimeAnimator.SetBool("arrivedToPos", true);
                skinGreen.SetActive(false);
                skinYellow.SetActive(true);
            }
        }
        if(vector.magnitude <= slimeRange){
            slimeAnimator.SetBool("isRange", true);
            ccSlime.Move(direction * eSpeed * Time.deltaTime);
            if(firstRun){
                firstRun = false;
                audioExtraSlime.clip = runSound;
                audioExtraSlime.Play();
            }
        } else{
            slimeAnimator.SetBool("isRange", false);
            if(!firstRun){
                firstRun = true;
                audioExtraSlime.Stop();
            }
        }
        if(vector.magnitude >= slimeRange + 5){
            if(noAlertFirstTime){
                noAlertFirstTime = false;
                if(!noAlertFirstTimeFix){
                    slimeAnimator.SetBool("alert", false);
                    audioSlime.PlayOneShot(noAlertSound, 5f * seVol);
                    skinYellow.SetActive(false);
                    skinGreen.SetActive(true);
                }
                noAlertFirstTimeFix = false;
            }
            Vector3 vectorBack = originalPos - transform.position;
            Vector3 directionBack = new Vector3(vectorBack.normalized.x, 0f, vectorBack.normalized.z);
            if(vectorBack.magnitude >= 1){
                alertFirstTime = true;
                slimeAnimator.SetBool("arrivedToPos", false);
                ccSlime.Move(directionBack * eSpeed * Time.deltaTime);
            } else{
                slimeAnimator.SetBool("arrivedToPos", true);
                alertFirstTime = true;
            }
        }
    }

    private void DontLookAtPlayerLerp(){
        Vector3 vector = player.transform.position - transform.position;
        if(vector.magnitude <= (slimeRange + 5) && vector.magnitude > slimeRange){
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(vector.x, 0f, vector.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime);
        } else if(vector.magnitude <= slimeRange){
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(-vector.x, 0f, -vector.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime);
        } else{
            Vector3 vectorBack = originalPos - transform.position;
            if(vectorBack.magnitude >= 1){
                Quaternion newRotationBack = Quaternion.LookRotation(new Vector3(vectorBack.x, 0f, vectorBack.z));
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotationBack, speedToLook * Time.deltaTime);
            } else{
                transform.rotation = Quaternion.Lerp(transform.rotation, originalRot, speedToLook * Time.deltaTime);
            }
        }
    }
}
