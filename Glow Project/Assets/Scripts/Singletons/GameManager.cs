using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //RUNTIME
    public Texture2D cursor;
    public static GameManager instance;
    public int actualLvl;
    public int lvlKilledSlimes;
    public int globalKilledSlimes;
    public float musicVol;
    public float seVol;

    //FUNCTIONS
    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
            actualLvl = PlayerPrefs.GetInt("ActualLvl");
            lvlKilledSlimes = 0;
            globalKilledSlimes = PlayerPrefs.GetInt("GlobalKilledSlimes");
            if(PlayerPrefs.GetFloat("MusicVol") != 0){
                musicVol = PlayerPrefs.GetFloat("MusicVol");
            } else{
                musicVol = 1f;
            }
            if(PlayerPrefs.GetFloat("SEVol") != 0){
                seVol = PlayerPrefs.GetFloat("SEVol");
            } else{
                seVol = 1f;
            }
        } else {
        Destroy(gameObject);
        }
    }

    void Start(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public static void SaveActualLvl(int lvl){
        instance.actualLvl = lvl;
        PlayerPrefs.SetInt("ActualLvl", instance.actualLvl);
    }
    
    public static void SaveGlobalKilledSlimes(int killed){
        instance.globalKilledSlimes = killed;
        PlayerPrefs.SetInt("GlobalKilledSlimes", instance.globalKilledSlimes);
    }

    public static void SaveMusicVol(float vol){
        instance.musicVol = vol;
        PlayerPrefs.SetFloat("MusicVol", instance.musicVol);
    }

    public static void SaveSEVol(float vol){
        instance.seVol = vol;
        PlayerPrefs.SetFloat("SEVol", instance.seVol);
    }
}
