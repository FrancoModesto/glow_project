using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    //RUNTIME
    private AudioSource audioLootBox;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private GameObject spawnpoint;
    [SerializeField] private GameObject armorItemPrefab;
    [SerializeField] private GameObject bubbleItemPrefab;
    private float seVol = 1f;
    private int rand = 0;

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        audioLootBox = GetComponent<AudioSource>();

        rand = Random.Range(1,3);
    }

    void Update()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }
        
        transform.Rotate(0f, 30f * Time.deltaTime, 0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            audioLootBox.PlayOneShot(breakSound, 1f * seVol);
            if(rand == 1){
                Instantiate(armorItemPrefab, spawnpoint.transform.position, spawnpoint.transform.rotation);
            }
            if(rand == 2){
               Instantiate(bubbleItemPrefab, spawnpoint.transform.position, spawnpoint.transform.rotation); 
            }
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Invoke("DelayDestroy", 2f);
        }
    }

    private void DelayDestroy(){
        Destroy(gameObject);
    }
}
