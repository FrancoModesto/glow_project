using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCollision : MonoBehaviour
{
    //DESIGN
    [SerializeField] private Animator slimeAnimator;
    [SerializeField] private GameObject slimeBloodPrefab;
    [SerializeField] private GameObject lavaSplashPrefab;
    private AudioSource audioSlime;
    [SerializeField] private AudioSource audioExtraSlime;
    private AudioSource childAudio;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip lavaSplashSound;
    private bool isDead = false;
    [SerializeField] private GameObject skinGreen;
    [SerializeField] private GameObject skinYellow;
    [SerializeField] private GameObject skinRed;
    private float seVol = 1f;

    //GETTER Y SETTER
    public bool GetSCisDead(){
        return isDead;
    }

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        audioSlime = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Player")){
            if(GameManager.instance != null){
                GameManager.instance.lvlKilledSlimes++;
            }
            isDead = true;
            Instantiate(slimeBloodPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), slimeBloodPrefab.transform.localRotation);
            skinGreen.SetActive(false);
            skinYellow.SetActive(false);
            skinRed.SetActive(true);
            audioSlime.PlayOneShot(dieSound, 8f * seVol);
            slimeAnimator.SetBool("isDead", true);
            gameObject.GetComponent<CharacterController>().enabled = false;
            audioExtraSlime.Stop();
            Invoke("DelayedDestroy", 2.5f);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if(hit.gameObject.CompareTag("Kill")){
            if(GameManager.instance != null){
                GameManager.instance.lvlKilledSlimes++;
            }
            gameObject.GetComponent<CharacterController>().enabled = false;
            foreach(Transform child in gameObject.transform){
                childAudio = child.gameObject.GetComponent<AudioSource>();
                if(childAudio == null){
                    child.gameObject.SetActive(false);
                }
            }
            Instantiate(lavaSplashPrefab, new Vector3(transform.position.x, hit.gameObject.transform.position.y + 1.2f, transform.position.z), lavaSplashPrefab.transform.localRotation);
            audioSlime.PlayOneShot(lavaSplashSound, 4f * seVol);
            Invoke("DelayedDestroy", 2f);
        }
    }

    private void DelayedDestroy(){
        Destroy(gameObject);
    }
}
