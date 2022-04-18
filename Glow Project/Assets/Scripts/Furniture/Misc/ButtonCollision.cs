using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCollision : MonoBehaviour
{
    //RUNTIME
    [SerializeField] private GameObject skin;
    [SerializeField] private GameObject skinBright;
    private bool isPressed = false;
    private float originalPosY;
    private AudioSource audioButton;
    [SerializeField] private AudioClip pressedSound;
    private float seVol = 1f;

    //GETTER y SETTER
    public bool GetBCisPressed(){
        return isPressed;
    }

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        audioButton = GetComponent<AudioSource>();

        originalPosY = skin.transform.position.y;
    }

    void Update()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }
    }

    private void OnCollisionEnter(Collision other){
        if(!other.gameObject.name.Equals("Player")){
            isPressed = true;
            audioButton.PlayOneShot(pressedSound, 0.5f * seVol);
            if(skin.transform.position.y == originalPosY){
                skin.transform.position = new Vector3(skin.transform.position.x, originalPosY-0.2f, skin.transform.position.z);
                skinBright.transform.position = new Vector3(skinBright.transform.position.x, originalPosY-0.2f, skinBright.transform.position.z);
                skin.SetActive(false);
                skinBright.SetActive(true);
            }
        }
    }

    private void OnCollisionStay(Collision other){
        if(!other.gameObject.name.Equals("Player")){
            isPressed = true;
            if(skin.transform.position.y == originalPosY){
                skin.transform.position = new Vector3(skin.transform.position.x, originalPosY-0.2f, skin.transform.position.z);
                skinBright.transform.position = new Vector3(skinBright.transform.position.x, originalPosY-0.2f, skinBright.transform.position.z);
                skin.SetActive(false);
                skinBright.SetActive(true);
            }
        }
    }

    private void OnCollisionExit(Collision other){
        if(!other.gameObject.name.Equals("Player")){
            isPressed = false;
            audioButton.PlayOneShot(pressedSound, 0.5f * seVol);
            skin.transform.position = new Vector3(skin.transform.position.x, originalPosY, skin.transform.position.z);
            skinBright.transform.position = new Vector3(skinBright.transform.position.x, originalPosY, skinBright.transform.position.z);
            skinBright.SetActive(false);
            skin.SetActive(true);
        }
    }
}
