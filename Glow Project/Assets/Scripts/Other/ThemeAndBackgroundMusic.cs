using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeAndBackgroundMusic : MonoBehaviour
{
    //RUNTIME
    private AudioSource audioMusic;
    [SerializeField] private AudioClip retroSoundtrack;
    [SerializeField] private AudioClip rickLull;
    private int rand = 0;
    [SerializeField] private GameObject normalLights;
    [SerializeField] private GameObject purpleLights;
    [SerializeField] private GameObject normalCubeSkin;
    [SerializeField] private GameObject greyCubeSkin;
    private float musicVol = 1f;

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            musicVol = GameManager.instance.musicVol;
        }

        audioMusic = GetComponent<AudioSource>();
        audioMusic.loop = true;
        rand = Random.Range(1,11);
        switch(rand){
            case 4:
                audioMusic.clip = rickLull;
                audioMusic.volume = 0.45f * musicVol;
                audioMusic.Play();
                normalLights.SetActive(false);
                purpleLights.SetActive(true);
                normalCubeSkin.SetActive(false);
                greyCubeSkin.SetActive(true);
                break;
            default:
                audioMusic.clip = retroSoundtrack;
                audioMusic.volume = 0.3f * musicVol;
                audioMusic.Play();
                purpleLights.SetActive(false);
                normalLights.SetActive(true);
                greyCubeSkin.SetActive(false);
                normalCubeSkin.SetActive(true);
                break;
        }
    }

    void Update()
    {
        if(GameManager.instance != null){
            musicVol = GameManager.instance.musicVol;
        }
        
        if(rand == 4){
            audioMusic.volume = 0.45f * musicVol;
        } else{
            audioMusic.volume = 0.3f * musicVol;
        }
    }
}
