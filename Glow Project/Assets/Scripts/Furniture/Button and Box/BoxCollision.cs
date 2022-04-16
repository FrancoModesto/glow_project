using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    //RUNTIME
    private GameObject player;
    [SerializeField] private GameObject lavaSplashPrefab;
    [SerializeField] private AudioClip lavaSplashSound;
    private float seVol = 1f;

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Kill")){
            Instantiate(lavaSplashPrefab, new Vector3(transform.position.x, other.gameObject.transform.position.y + 1.2f, transform.position.z), lavaSplashPrefab.transform.localRotation);
            player.GetComponent<PlayerCollision>().GetPCaudioPlayer().PlayOneShot(lavaSplashSound, 1f * seVol);
            Destroy(gameObject);
        }
    }
}
