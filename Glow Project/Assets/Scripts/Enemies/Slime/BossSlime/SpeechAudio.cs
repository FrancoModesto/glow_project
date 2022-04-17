using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechAudio : MonoBehaviour
{
    //RUNTIME
    private AudioSource audioSpeech;
    [SerializeField] private AudioClip speech;
    [SerializeField] private GameObject trapsTrigger;
    [SerializeField] private GameObject subtitles;
    private bool firstTime = true;
    private bool finishedPlaying = false;
    private float seVol = 1f;

    //GETTER y SETTER
    public bool GetSAfinishedPlaying(){
        return finishedPlaying;
    }

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        audioSpeech = GetComponent<AudioSource>();
        audioSpeech.volume = 1f * seVol;
    }

    void Update()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
            audioSpeech.volume = 1f * seVol;
        }


        if(trapsTrigger.GetComponent<TrapsTrigger>().GetTTfinish() && firstTime){
            firstTime = false;
            Invoke("DelayedSpeech", 3f);
        }
    }

    private void DelayedSpeech(){
        Invoke("DelayedFinishedPlaying", 10f);
        audioSpeech.clip = speech;
        audioSpeech.Play();

        subtitles.GetComponent<TMPro.TextMeshProUGUI>().text = "YOU";
        Invoke("DelayedHKMS", 2f);
        Invoke("DelayedAN", 5f);
        Invoke("DelayedYWPTP", 7f);
    }

    private void DelayedFinishedPlaying(){
        finishedPlaying = true;
        audioSpeech.clip = null;
        subtitles.GetComponent<TMPro.TextMeshProUGUI>().text = "";
    }

    private void DelayedHKMS(){
        subtitles.GetComponent<TMPro.TextMeshProUGUI>().text = "HAVE KILLED MY SONS";
    }

    private void DelayedAN(){
        subtitles.GetComponent<TMPro.TextMeshProUGUI>().text = "AND NOW";
    }

    private void DelayedYWPTP(){
        subtitles.GetComponent<TMPro.TextMeshProUGUI>().text = "YOU'LL PAY THE PRICE";
    }
}
