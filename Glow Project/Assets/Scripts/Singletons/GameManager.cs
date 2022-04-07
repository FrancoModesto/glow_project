using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //RUNTIME
    public static GameManager instance;
    public int actualLvl;
    public float musicVol;
    public float seVol;

    //FUNCTIONS
    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
            actualLvl = PlayerPrefs.GetInt("ActualLvl");
            musicVol = 1f;
            seVol = 1f;
        } else {
        Destroy(gameObject);
        }
    }

    public static void SaveActualLvl(int lvl){
        instance.actualLvl = lvl;
        PlayerPrefs.SetInt("ActualLvl", instance.actualLvl);
    }
}
