using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoor : MovingPlatform
{
    //RUNTIME
    [SerializeField] private AudioSource audioDoor;
    [SerializeField] private AudioClip movingDoorSound;
    [SerializeField] private GameObject[] buttonArray;
    private int arePressed = 0;
    private int preIndex = 0;
    private float seVol = 1f;

    //FUNCTIONS
    void Start(){
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        audioDoor = GetComponent<AudioSource>();
        audioDoor.clip = movingDoorSound;
        audioDoor.volume = 0.5f * seVol;
        audioDoor.loop = true;
    }

    void Update(){
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
            audioDoor.volume = 0.5f * seVol;
        }

        if(buttonArray.Length != 0){
            for(int i=0;i<buttonArray.Length;i++){
                if(buttonArray[i].GetComponent<ButtonCollision>().GetBCisPressed()){
                    arePressed++;
                }
            }

            if(preIndex != index){
                audioDoor.Stop();
                audioDoor.Play();
            }

            preIndex = index;

            if(arePressed == buttonArray.Length){
                index = 1;
            } else{
                index = 0;
            }

            arePressed = 0;
        }
    }

    protected override void MoveWithWaypoints(){
        Vector3 vector = vWaypoints[index].transform.position - transform.position;
        Vector3 direction = vector.normalized;
        if(vector.magnitude >= 0.1){
            transform.position += speed * direction * Time.deltaTime;
        } else{
            audioDoor.Stop();
        }
    }
}
