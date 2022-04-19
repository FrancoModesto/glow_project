using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTheme : MonoBehaviour
{
    //RUNTIME
    [SerializeField] private GameObject themeAndBckMusic;
    [SerializeField] private GameObject normalLights;
    [SerializeField] private GameObject purpleLights;
    [SerializeField] private GameObject normalRoomSkin;
    [SerializeField] private GameObject greyRoomSkin;
    private bool firstTime = true;

    void Update()
    {
        if(firstTime){
            firstTime = false;
            switch(themeAndBckMusic.GetComponent<ThemeAndBackgroundMusic>().GetTABMrand()){
            case 1:
                normalLights.SetActive(false);
                purpleLights.SetActive(true);
                normalRoomSkin.SetActive(false);
                greyRoomSkin.SetActive(true);
                break;
            default:
                purpleLights.SetActive(false);
                normalLights.SetActive(true);
                greyRoomSkin.SetActive(false);
                normalRoomSkin.SetActive(true);
                break;
        }
        }
    }
}
