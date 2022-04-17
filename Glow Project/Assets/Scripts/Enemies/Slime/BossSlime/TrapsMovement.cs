using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsMovement : MonoBehaviour
{
    //DESIGN
    [SerializeField] private float speed = 0f;

    //RUNTIME
    private GameObject player;
    [SerializeField] private AudioSource audioTraps;
    [SerializeField] private AudioClip laserMovement;
    [SerializeField] private GameObject speechAudio;
    private bool firstTime = true;
    private float seVol = 1f;

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        player = GameObject.Find("Player");

        audioTraps.loop = true;
        audioTraps.volume = 1f * seVol;
    }

    void Update()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
            audioTraps.volume = 1f * seVol;
        }

        if(speechAudio.GetComponent<SpeechAudio>().GetSAfinishedPlaying()){
            if(firstTime){
                firstTime = false;
                audioTraps.clip = laserMovement;
                audioTraps.Play();
            }
        }
    }

    void FixedUpdate()
    {
        if(speechAudio.GetComponent<SpeechAudio>().GetSAfinishedPlaying()){
            Vector3 vector = Vector3.zero - transform.position;
            Vector3 direction = new Vector3(0f, 0f, vector.normalized.z);
            if(vector.magnitude >= 1){
                transform.Translate(direction * speed * Time.deltaTime);
            } else{
                audioTraps.clip = null;
            }
        }
    }
}
