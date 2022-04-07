using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeOptionsSliderFix : MonoBehaviour
{
    //RUNTIME
    [SerializeField] private Slider sliderMusicVol;
    [SerializeField] private Slider sliderSEVol;

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            sliderMusicVol.value = GameManager.instance.musicVol;
            sliderSEVol.value = GameManager.instance.seVol;
        } else{
            sliderMusicVol.value = 1f;
            sliderSEVol.value = 1f;
        }
    }

    void Update()
    {
        if(GameManager.instance != null){
            sliderMusicVol.value = GameManager.instance.musicVol;
            sliderSEVol.value = GameManager.instance.seVol;
        } else{
            sliderMusicVol.value = 1f;
            sliderSEVol.value = 1f;
        }
    }
}
