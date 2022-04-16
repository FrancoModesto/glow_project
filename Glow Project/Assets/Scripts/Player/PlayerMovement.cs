using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //DESIGN
    [SerializeField] private PlayerData playerData;

    //RUNTIME
    private Rigidbody rbPlayer;
    private Vector3 originalScale = Vector3.zero;
    [SerializeField] private bool canJump = false;
    private float jumpForce = 1f;
    private bool canShift = true;
    private bool canFall = true;
    [SerializeField] private GameObject vCam0;
    [SerializeField] private GameObject vCam1;
    private AudioSource[] audios;
    private AudioSource audioPlayer;
    private AudioSource audioPlayerExtra;
    private GameObject audioSourceExtra;
    [SerializeField] private AudioClip chargeSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip shiftSound;
    [SerializeField] private AudioClip fallSound;
    private float timePassJ = 0f;
    [SerializeField] private GameObject skin;
    [SerializeField] private GameObject skinBright;
    [SerializeField] private GameObject skinShift;
    private bool skinIsActive = true;
    [SerializeField] private GameObject cameraCM;
    private bool canPress = true;
    private float seVol = 1f;
    private GameObject UI;
    [SerializeField] private GameObject shiftVFXPrefab;
    [SerializeField] private GameObject shiftVFXPrefabSmall;

    //GETTER Y SETTER
    public PlayerData GetPMplayerData(){
        return playerData;
    }

    public GameObject GetPMvCam0(){
        return vCam0;
    }

    public GameObject GetPMvCam1(){
        return vCam1;
    }

    public void SetPMrbPlayerVelocity(Vector3 value){
        rbPlayer.velocity = value;
    }

    public void SetPMrbPlayerGravity(bool status){
        rbPlayer.useGravity = status;
    }

    public Vector3 GetPMoriginalScale(){
        return originalScale;
    }

    public void SetPMcanJump(bool status){
        canJump = status;
    }

    public float GetPMjumpForce(){
        return jumpForce;
    }

    public void SetPMjumpForce(float value){
        jumpForce = value;
    }

    public bool GetPMcanShift(){
        return canShift;
    }

    public bool GetPMcanFall(){
        return canFall;
    }

    public AudioSource GetPMaudioPlayerExtra(){
        return audioPlayerExtra;
    }

    public AudioSource[] GetPMaudios(){
        return audios;
    }

    public void SetPMtimePassJ(float value){
        timePassJ = value;
    }

    public GameObject GetPMcameraCM(){
        return cameraCM;
    }

    public void SetPMcanPress(bool status){
        canPress = status;
    }

    //FUNCTIONS
    void Start()
    {
        Cursor.visible = false;

        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        UI = GameObject.Find("---UI---");

        rbPlayer = GetComponent<Rigidbody>();

        audioPlayer = GetComponent<AudioSource>();

        audioSourceExtra = GameObject.Find("Audio Source Extra");
        audioPlayerExtra = audioSourceExtra.GetComponent<AudioSource>();
        audioPlayerExtra.clip = chargeSound;
        audioPlayerExtra.volume = 0.35f * seVol;
        audioPlayerExtra.loop = true;

        originalScale = transform.localScale;

        audios = FindObjectsOfType<AudioSource>();
    }

    void FixedUpdate(){
        if(canPress && !GetComponent<PlayerCollision>().GetPCwin()){
            if(Input.GetKey(KeyCode.W)){
                Move(Vector3.right, playerData.GetPDpSpeed());
            }
            if(Input.GetKey(KeyCode.A)){
                Move(Vector3.forward, playerData.GetPDpSpeed());
            }
            if(Input.GetKey(KeyCode.S)){
                Move(Vector3.left, playerData.GetPDpSpeed());
            }
            if(Input.GetKey(KeyCode.D)){
                Move(Vector3.back, playerData.GetPDpSpeed());
            }
            if(GetComponent<PlayerCollision>().GetPChasBubble()){
                if(Input.GetKey(KeyCode.Space)){
                    Move(Vector3.up, playerData.GetPDpSpeed());
                }
                if(Input.GetKey(KeyCode.E)){
                    Move(Vector3.down, playerData.GetPDpSpeed());
                }
            }
        }
    }

    void Update()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        if(GetComponent<PlayerCollision>().GetPChasBubble()){
            rbPlayer.useGravity = false;
        } else{
            rbPlayer.useGravity = true;
        }
        
        audioPlayerExtra.volume = 0.35f * seVol;

         if(transform.position.y >= 15 && !GetComponent<PlayerCollision>().GetPCisDead()){
                vCam0.SetActive(false);
                vCam1.SetActive(true);
            } else{
                vCam1.SetActive(false);
                vCam0.SetActive(true);
            }

        if(canPress){
            if(!GetComponent<PlayerCollision>().GetPChasBubble()){
                if(Input.GetKeyDown(KeyCode.Space) && !GetComponent<PlayerCollision>().GetPCwin()){
                    audioPlayerExtra.Play();
                    UI.GetComponent<UIManager>().GetUIMjumpPanel().SetActive(false);
                    UI.GetComponent<UIManager>().GetUIMjumpPanelPressed().SetActive(true);
                }
                if(Input.GetKey(KeyCode.Space) && !GetComponent<PlayerCollision>().GetPCwin()){
                    timePassJ += Time.deltaTime;
                    if(timePassJ >= 0.4f){
                        if(!skinShift.activeInHierarchy){
                            ChargeJump();
                        }
                    }
                }
                if(Input.GetKeyUp(KeyCode.Space) && !GetComponent<PlayerCollision>().GetPCwin()){
                    timePassJ = 0f;
                    audioPlayerExtra.Stop();
                    Jump();
                    UI.GetComponent<UIManager>().GetUIMjumpPanelPressed().SetActive(false);
                    UI.GetComponent<UIManager>().GetUIMjumpPanel().SetActive(true);
                }
            } else if(!GetComponent<PlayerCollision>().GetPCwin()){
                if(Input.GetKeyDown(KeyCode.Space)){
                    UI.GetComponent<UIManager>().GetUIMupPanel().SetActive(false);
                    UI.GetComponent<UIManager>().GetUIMupPanelPressed().SetActive(true);
                }
                if(Input.GetKeyDown(KeyCode.E)){
                    UI.GetComponent<UIManager>().GetUIMdownPanel().SetActive(false);
                    UI.GetComponent<UIManager>().GetUIMdownPanelPressed().SetActive(true);
                }
                if(Input.GetKeyUp(KeyCode.Space)){
                    UI.GetComponent<UIManager>().GetUIMupPanelPressed().SetActive(false);
                    UI.GetComponent<UIManager>().GetUIMupPanel().SetActive(true);
                }
                if(Input.GetKeyUp(KeyCode.E)){
                    UI.GetComponent<UIManager>().GetUIMdownPanelPressed().SetActive(false);
                    UI.GetComponent<UIManager>().GetUIMdownPanel().SetActive(true);
                } 
            }
            if(Input.GetKeyDown(KeyCode.LeftShift) && canShift && !GetComponent<PlayerCollision>().GetPCwin()){
                if(Input.GetKey(KeyCode.W)){
                    Instantiate(shiftVFXPrefab, transform.position, Quaternion.identity);
                    UI.GetComponent<UIManager>().GetUIMshiftPanel().SetActive(false);
                    UI.GetComponent<UIManager>().GetUIMshiftPanelCooldown().SetActive(true);
                    Shift(Vector3.right);
                }
                if(Input.GetKey(KeyCode.A)){
                    Instantiate(shiftVFXPrefab, transform.position, Quaternion.identity);
                    UI.GetComponent<UIManager>().GetUIMshiftPanel().SetActive(false);
                    UI.GetComponent<UIManager>().GetUIMshiftPanelCooldown().SetActive(true);
                    Shift(Vector3.forward);
                }
                if(Input.GetKey(KeyCode.S)){
                    Instantiate(shiftVFXPrefab, transform.position, Quaternion.identity);
                    UI.GetComponent<UIManager>().GetUIMshiftPanel().SetActive(false);
                    UI.GetComponent<UIManager>().GetUIMshiftPanelCooldown().SetActive(true);
                    Shift(Vector3.left);
                }
                if(Input.GetKey(KeyCode.D)){
                    Instantiate(shiftVFXPrefab, transform.position, Quaternion.identity);
                    UI.GetComponent<UIManager>().GetUIMshiftPanel().SetActive(false);
                    UI.GetComponent<UIManager>().GetUIMshiftPanelCooldown().SetActive(true);
                    Shift(Vector3.back);
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftControl) && canFall && !GetComponent<PlayerCollision>().GetPCwin()){
                Instantiate(shiftVFXPrefab, transform.position, Quaternion.identity);
                UI.GetComponent<UIManager>().GetUIMfallPanel().SetActive(false);
                UI.GetComponent<UIManager>().GetUIMfallPanelCooldown().SetActive(true);
                Fall();
            }
            if(Input.GetKeyDown(KeyCode.R)){
                if(GameManager.instance != null){
                    GameManager.instance.lvlKilledSlimes = 0;
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            if(UI.GetComponent<UIManager>().GetUIMpauseMenu().activeInHierarchy || UI.GetComponent<UIManager>().GetUIMoptionsInGameMenu().activeInHierarchy){
                Cursor.visible = false;
                UI.GetComponent<UIManager>().GetUIMpauseMenu().SetActive(false);
                UI.GetComponent<UIManager>().GetUIMoptionsInGameMenu().SetActive(false);
                UI.GetComponent<UIManager>().GetUIMhudPanel().SetActive(true);
                Time.timeScale = 1;
                foreach(AudioSource audio in audios){
                    if(audio != null && audio != audioPlayerExtra){
                        audio.Play();
                    }
                }
                canPress = true;
                cameraCM.SetActive(true);
                if(!GetComponent<PlayerCollision>().GetPChasBubble()){
                    UI.GetComponent<UIManager>().GetUIMjumpPanelPressed().SetActive(false);
                    UI.GetComponent<UIManager>().GetUIMjumpPanel().SetActive(true);
                    if(jumpForce > 1){
                        timePassJ = 0f;
                        Jump();
                    }
                } else{
                    UI.GetComponent<UIManager>().GetUIMupPanelPressed().SetActive(false);
                    UI.GetComponent<UIManager>().GetUIMupPanel().SetActive(true);
                    UI.GetComponent<UIManager>().GetUIMdownPanelPressed().SetActive(false);
                    UI.GetComponent<UIManager>().GetUIMdownPanel().SetActive(true);
                }
            } else if(!GetComponent<PlayerCollision>().GetPCwin()){
                Cursor.visible = true;
                canPress = false;
                UI.GetComponent<UIManager>().GetUIMhudPanel().SetActive(false);
                UI.GetComponent<UIManager>().GetUIMpauseMenu().SetActive(true);
                Time.timeScale = 0;
                foreach(AudioSource audio in audios){
                    if(audio != null && audio != audioPlayer && audio != audioPlayerExtra){
                        audio.Pause();
                    } else if(audio != null){
                        audio.Stop();
                    }
                }
            }
        }
    }

    private void Move(Vector3 direction, float speed){
        rbPlayer.AddForce(direction.normalized * speed * Time.deltaTime, ForceMode.Acceleration);
    }

    public void Jump(){
        transform.localScale = originalScale;
        skinBright.SetActive(false);
        skin.SetActive(true);
        if(canJump){
            Instantiate(shiftVFXPrefabSmall, transform.position, Quaternion.identity);
            rbPlayer.AddForce(Vector3.up * playerData.GetPDjumpPower() * jumpForce, ForceMode.Impulse);
            audioPlayer.PlayOneShot(jumpSound, 0.4f * seVol);
            Invoke("LittleCooldownToFalseJump", 0.05f);
        }
        jumpForce = 1f;
    }

    private void ChargeJump(){
        skin.SetActive(false);
        skinBright.SetActive(true);
        if(jumpForce <= playerData.GetPDmaxJumpForce()){
            jumpForce += Time.deltaTime;
            transform.localScale += new Vector3(-Time.deltaTime*0.3f, -Time.deltaTime*0.3f, -Time.deltaTime*0.3f);
        }
    }

    private void LittleCooldownToFalseJump(){
        canJump = false;
    }

    private void Shift(Vector3 direction){
        skinIsActive = skin.activeInHierarchy;
        skin.SetActive(false);
        skinBright.SetActive(false);
        skinShift.SetActive(true);
        rbPlayer.AddForce(direction * playerData.GetPDshiftPower(), ForceMode.Impulse);
        canShift = false;
        audioPlayer.PlayOneShot(shiftSound, 1.2f * seVol);
        Invoke("RemoveShiftSkin", 0.5f);
        Invoke("RemoveShiftCooldown", playerData.GetPDshiftCooldown());
    }

    private void RemoveShiftCooldown(){
        canShift = true;
        UI.GetComponent<UIManager>().GetUIMshiftPanelCooldown().SetActive(false);
        UI.GetComponent<UIManager>().GetUIMshiftPanel().SetActive(true);
        UI.GetComponent<UIManager>().SetUIMtimePassS(playerData.GetPDshiftCooldown());
    }

    private void RemoveShiftSkin(){
        if(!GetComponent<PlayerCollision>().GetPCisDead()){
            skinShift.SetActive(false);
            skinBright.SetActive(false);
            skin.SetActive(true);
        }
    }

    private void Fall(){
        canFall = false;
        audioPlayer.PlayOneShot(fallSound, 2.5f * seVol);
        rbPlayer.AddForce(Vector3.down *  playerData.GetPDfallPower(), ForceMode.Impulse);
        Invoke("RemoveFallCooldown", playerData.GetPDfallCooldown());
    }

    private void RemoveFallCooldown(){
        canFall = true;
        UI.GetComponent<UIManager>().GetUIMfallPanelCooldown().SetActive(false);
        UI.GetComponent<UIManager>().GetUIMfallPanel().SetActive(true);
        UI.GetComponent<UIManager>().SetUIMtimePassF(playerData.GetPDfallCooldown());
    }
} 