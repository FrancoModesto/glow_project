using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    //RUNTIME
    [SerializeField] private Slider sliderMusicVol;
    [SerializeField] private Slider sliderSEVol;
    [SerializeField] private Button continueButton;
    private AudioSource audioMenu;
    [SerializeField] private AudioClip menuButtonClickSound;
    [SerializeField] private AudioClip menuButtonEnteredSound;
    private GameObject player;
    private GameObject UI;
    private bool isSandbox = false;

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            audioMenu = GameObject.Find("MenuExtraAudioSource").GetComponent<AudioSource>();
        } else{
            isSandbox = true;
        }


        if(SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1){
            player = GameObject.Find("Player");
            UI = GameObject.Find("---UI---");
        }
    }

    public void ButtonEntered(){
        if(!isSandbox){
            audioMenu.PlayOneShot(menuButtonEnteredSound, 0.5f * GameManager.instance.seVol);
        }

    }

    public void ContinueButtonEntered(){
        if(continueButton.interactable){
            audioMenu.PlayOneShot(menuButtonEnteredSound, 0.5f * GameManager.instance.seVol);
        }
    }

    public void NewGame(){
        audioMenu.PlayOneShot(menuButtonClickSound, 1f * GameManager.instance.seVol);
        SceneManager.LoadScene(2);
        GameManager.SaveActualLvl(2);
        GameManager.SaveGlobalKilledSlimes(0);
    }

    public void Continue(){
        audioMenu.PlayOneShot(menuButtonClickSound, 1f * GameManager.instance.seVol);
        if(GameManager.instance.actualLvl != 0){
            SceneManager.LoadScene(GameManager.instance.actualLvl);
        }
    }

    public void Options(){
        audioMenu.PlayOneShot(menuButtonClickSound, 1f * GameManager.instance.seVol);
        SceneManager.LoadScene(1);
    }

    public void ExitGame(){
        audioMenu.PlayOneShot(menuButtonClickSound, 1f * GameManager.instance.seVol);
        Application.Quit();
    }

    public void Back(){
        audioMenu.PlayOneShot(menuButtonClickSound, 1f * GameManager.instance.seVol);
        SceneManager.LoadScene(0);
    }

    public void MusicVolChange(){
        if(GameManager.instance != null){
            GameManager.instance.musicVol = sliderMusicVol.value;
        }
    }

    public void SEVolChange(){
        if(GameManager.instance != null){
            GameManager.instance.seVol = sliderSEVol.value;
        }
    }

    public void Resume(){
        if(!isSandbox){
            audioMenu.PlayOneShot(menuButtonClickSound, 1f * GameManager.instance.seVol);
        }
        Cursor.visible = false;
        UI.GetComponent<UIManager>().GetUIMpauseMenu().SetActive(false);
        UI.GetComponent<UIManager>().GetUIMhudPanel().SetActive(true);
        Time.timeScale = 1;
        foreach(AudioSource audio in  player.GetComponent<PlayerMovement>().GetPMaudios()){
            if(audio != null && audio != player.GetComponent<PlayerMovement>().GetPMaudioPlayerExtra()){
                audio.Play();
            }
        }
        player.GetComponent<PlayerMovement>().SetPMcanPress(true);
        player.GetComponent<PlayerMovement>().GetPMcameraCM().SetActive(true);
        if(!player.GetComponent<PlayerCollision>().GetPChasBubble()){
            UI.GetComponent<UIManager>().GetUIMjumpPanelPressed().SetActive(false);
            UI.GetComponent<UIManager>().GetUIMjumpPanel().SetActive(true);
            if(player.GetComponent<PlayerMovement>().GetPMjumpForce() > 1){
                player.GetComponent<PlayerMovement>().SetPMtimePassJ(0f);
                player.GetComponent<PlayerMovement>().Jump();
            }
        } else{
            UI.GetComponent<UIManager>().GetUIMupPanelPressed().SetActive(false);
            UI.GetComponent<UIManager>().GetUIMupPanel().SetActive(true);
            UI.GetComponent<UIManager>().GetUIMdownPanelPressed().SetActive(false);
            UI.GetComponent<UIManager>().GetUIMdownPanel().SetActive(true);
        }
        UI.GetComponent<UIManager>().GetUIMresumeButton().interactable = false;
        UI.GetComponent<UIManager>().GetUIMresumeButton().interactable = true;
    }

    public void OptionsInGame(){
        if(!isSandbox){
            audioMenu.PlayOneShot(menuButtonClickSound, 1f * GameManager.instance.seVol);
        }
        UI.GetComponent<UIManager>().GetUIMpauseMenu().SetActive(false);
        UI.GetComponent<UIManager>().GetUIMoptionsInGameMenu().SetActive(true);
        UI.GetComponent<UIManager>().GetUIMoptionsInGameButton().interactable = false;
        UI.GetComponent<UIManager>().GetUIMoptionsInGameButton().interactable = true;
    }

    public void OptionsInGameBack(){
        if(!isSandbox){
            audioMenu.PlayOneShot(menuButtonClickSound, 1f * GameManager.instance.seVol);
        }
        UI.GetComponent<UIManager>().GetUIMoptionsInGameMenu().SetActive(false);
        UI.GetComponent<UIManager>().GetUIMpauseMenu().SetActive(true);
    }

    public void ExitToMenu(){
        if(!isSandbox){
            audioMenu.PlayOneShot(menuButtonClickSound, 1f * GameManager.instance.seVol);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
