using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeOptionsLabelFix : MonoBehaviour
{
    //RUNTIME
    [SerializeField] private GameObject musicVolLabel;
    [SerializeField] private GameObject seVolLabel;
    private TMPro.TextMeshProUGUI textMusicVolObject;
    private TMPro.TextMeshProUGUI textSEVolObject;
    private float textMusicVol;
    private float textSEVol;
    private float musicVol = 1f;
    private float seVol = 1f;

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            musicVol = GameManager.instance.musicVol;
            seVol = GameManager.instance.seVol;
        }

        textMusicVolObject = musicVolLabel.GetComponent<TMPro.TextMeshProUGUI>();
        textSEVolObject = seVolLabel.GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        if(GameManager.instance != null){
            musicVol = GameManager.instance.musicVol;
            seVol = GameManager.instance.seVol;
        }

        textMusicVol = musicVol * 100;
        textMusicVol = Mathf.Round(textMusicVol);
        textMusicVolObject.text = textMusicVol.ToString() + "%";

        textSEVol = seVol * 100;
        textSEVol = Mathf.Round(textSEVol);
        textSEVolObject.text = textSEVol.ToString() + "%";
    }
}
