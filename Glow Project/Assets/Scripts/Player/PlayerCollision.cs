using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerCollision : MonoBehaviour
{
    //DESIGN
    private PlayerData playerData;

    //RUNTIME
    private AudioSource audioPlayer;
    private AudioSource audioPlayerExtra;
    private GameObject audioSourceExtra;
    [SerializeField] private GameObject ppGlobal;
    [SerializeField] private GameObject ppGlobalDeath;
    [SerializeField] private GameObject vCam2;
    [SerializeField] private AudioClip itemPickSound;
    [SerializeField] private AudioClip collisionSound;
    [SerializeField] private AudioClip collisionArmorSound;
    [SerializeField] private AudioClip collisionBubbleSound;
    [SerializeField] private AudioClip killedSound;
    [SerializeField] private AudioClip lavaSplashSound;
    [SerializeField] private AudioClip batExplosionSound;
    [SerializeField] private AudioClip wizardBulletKillSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip pickArmorSound;
    [SerializeField] private AudioClip blockArmorSound;
    [SerializeField] private AudioClip pickBubbleSound;
    [SerializeField] private GameObject skin;
    [SerializeField] private GameObject skinBright;
    [SerializeField] private GameObject skinShift;
    [SerializeField] private GameObject skinArmor;
    [SerializeField] private GameObject skinBubble;
    private bool firstTimeCollision = true;
    private bool missionOk = false;
    private bool win = false;
    private Vector3 winPos;
    private Quaternion winRot;
    private Vector3 winScale;
    private bool isDead = false;
    private bool isDeadByLaser = false;
    private float seVol = 1f;
    private bool hasArmor = false;
    private bool hasBubble = false;
    [SerializeField] private GameObject itemPickPrefab;
    [SerializeField] private GameObject armorBreakPrefab;
    [SerializeField] private GameObject bubbleBreakPrefab;
    [SerializeField] private GameObject lavaSplashPrefab;
    [SerializeField] private GameObject batExplosionPrefab;
    [SerializeField] private GameObject wizardBulletHitPrefab;
    private GameObject UI;

    //EVENTS
    public event Action OnDeath;
    public event Action OnArmorPickOrBreak;
    public event Action OnBubblePick;
    public event Action OnBubbleBreak;

    //GETTER y SETTER
    public AudioSource GetPCaudioPlayer(){
        return audioPlayer;
    }

    public GameObject GetPCppGlobal(){
        return ppGlobal;
    }

    public GameObject GetPCppGlobalDeath(){
        return ppGlobalDeath;
    }

    public GameObject GetPCvCam2(){
        return vCam2;
    }

    public AudioClip GetPCkilledSound(){
        return killedSound;
    }

    public bool GetPCmissionOk(){
        return missionOk;
    }

    public void SetPCmissionOk(bool status){
        missionOk = status;
    }

    public bool GetPCwin(){
        return win;
    }

    public bool GetPCisDead(){
        return isDead;
    }

    public void SetPCisDead(bool status){
        isDead = status;
    }

    public void SetPCisDeadByLaser(bool status){
        isDeadByLaser = status;
    }

    public bool GetPChasArmor(){
        return hasArmor;
    }
    public bool GetPChasBubble(){
        return hasBubble;
    }

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        UI = GameObject.Find("---UI---");

        playerData = GetComponent<PlayerMovement>().GetPMplayerData();

        audioPlayer = GetComponent<AudioSource>();
        audioSourceExtra = GameObject.Find("Audio Source Extra");
        audioPlayerExtra = audioSourceExtra.GetComponent<AudioSource>();

        Invoke("FirstTime", 0.5f);
    }

    void Update()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        if(isDeadByLaser){
            OnDeath?.Invoke();
        }
        
        if(win){
            transform.position = winPos;
            transform.rotation = winRot;
            transform.localScale = winScale;
        }
    }

    private void OnCollisionEnter(Collision other){
        if(!firstTimeCollision && !other.gameObject.CompareTag("Kill") && !other.gameObject.CompareTag("Bat")){
            if(!hasArmor && !hasBubble && !other.gameObject.CompareTag("Metallic")){
                audioPlayer.PlayOneShot(collisionSound, 0.2f * seVol);
            } else if((hasArmor || other.gameObject.CompareTag("Metallic")) && !hasBubble){
                audioPlayer.PlayOneShot(collisionArmorSound, 0.5f * seVol);
            } else if(hasBubble){
                audioPlayer.PlayOneShot(collisionBubbleSound, 6f * seVol);
            }
        }
        if(other.gameObject.CompareTag("Ground")){
            GetComponent<PlayerMovement>().SetPMcanJump(true);
        }
        if(other.gameObject.CompareTag("Kill")){
            GetComponent<PlayerMovement>().GetPMaudioPlayerExtra().Stop();
            OnDeath?.Invoke();
            isDead = true;
            GetComponent<PlayerMovement>().GetPMvCam0().SetActive(false);
            GetComponent<PlayerMovement>().GetPMvCam1().SetActive(false);
            vCam2.SetActive(true);
            ppGlobal.SetActive(false);
            ppGlobalDeath.SetActive(true);
            Time.timeScale = 0.5f;
            Instantiate(lavaSplashPrefab, new Vector3(transform.position.x, other.gameObject.transform.position.y + 1.2f, transform.position.z), lavaSplashPrefab.transform.localRotation);            audioPlayer.PlayOneShot(lavaSplashSound, 1f * seVol);
            audioPlayer.PlayOneShot(killedSound, 0.5f * seVol);
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<PlayerMovement>().SetPMrbPlayerVelocity(Vector3.zero);
            gameObject.GetComponent<PlayerMovement>().SetPMrbPlayerGravity(false);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            skin.SetActive(false);
            skinBright.SetActive(false);
            skinShift.SetActive(false);
            skinArmor.SetActive(false);
            skinBubble.SetActive(false);
            Invoke("Respawn", 1f);
        }
        if(other.gameObject.CompareTag("Bat")){
            if(hasArmor){
                audioPlayer.PlayOneShot(batExplosionSound, 1.5f * seVol);
                audioPlayer.PlayOneShot(blockArmorSound, 1f * seVol);
                Instantiate(batExplosionPrefab, other.gameObject.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            } else{
                GetComponent<PlayerMovement>().GetPMaudioPlayerExtra().Stop();
                OnDeath?.Invoke();
                isDead = true;
                GetComponent<PlayerMovement>().GetPMvCam0().SetActive(false);
                GetComponent<PlayerMovement>().GetPMvCam1().SetActive(false);
                vCam2.SetActive(true);
                ppGlobal.SetActive(false);
                ppGlobalDeath.SetActive(true);
                Time.timeScale = 0.5f;
                audioPlayer.PlayOneShot(batExplosionSound, 1.5f * seVol);
                audioPlayer.PlayOneShot(killedSound, 0.5f * seVol);
                Instantiate(batExplosionPrefab, other.gameObject.transform.position, Quaternion.identity);
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                gameObject.GetComponent<PlayerMovement>().SetPMrbPlayerVelocity(Vector3.zero);
                gameObject.GetComponent<PlayerMovement>().SetPMrbPlayerGravity(false);
                gameObject.GetComponent<SphereCollider>().enabled = false;
                skin.SetActive(false);
                skinBright.SetActive(false);
                skinShift.SetActive(false);
                skinArmor.SetActive(false);
                skinBubble.SetActive(false);
                Destroy(other.gameObject);
                Invoke("Respawn", 1.5f);
            }
        }
        if(other.gameObject.name == "WinningPlatform" && missionOk){
            GetComponent<PlayerMovement>().GetPMaudioPlayerExtra().Stop();
            OnDeath?.Invoke();
            win = true;
            winPos = transform.position;
            winRot = transform.rotation;
            winScale = transform.localScale;
            audioPlayer.PlayOneShot(winSound, 1f * seVol);
            gameObject.GetComponent<PlayerMovement>().SetPMrbPlayerVelocity(Vector3.zero);
            gameObject.GetComponent<PlayerMovement>().SetPMrbPlayerGravity(false);
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }

    private void OnCollisionStay(Collision other){
        if(other.gameObject.CompareTag("Ground")){
            GetComponent<PlayerMovement>().SetPMcanJump(true);
        }
    }

    private void OnCollisionExit(Collision other){
        if(other.gameObject.CompareTag("Ground")){
            Invoke("FalseJump", playerData.GetPDextraTimeJump());
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Armor Item") && !hasArmor && !hasBubble){
            Instantiate(itemPickPrefab, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y - 0.5f, other.gameObject.transform.position.z), itemPickPrefab.transform.localRotation);
            Destroy(other.gameObject);
            audioPlayer.PlayOneShot(itemPickSound, 1f * seVol);
            audioPlayer.PlayOneShot(pickArmorSound, 1f * seVol);
            skinArmor.SetActive(true);
            OnArmorPickOrBreak?.Invoke();
            hasArmor = true;
            InvokeRepeating("PreRemoveArmor", playerData.GetPDarmorDuration() - 4f, 0.3f);
            Invoke("RemoveArmor", playerData.GetPDarmorDuration());
        }
        if(other.gameObject.CompareTag("Bubble Item") && !hasArmor && !hasBubble){
            Instantiate(itemPickPrefab, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y - 0.5f, other.gameObject.transform.position.z), itemPickPrefab.transform.localRotation);
            Destroy(other.gameObject);
            audioPlayer.PlayOneShot(itemPickSound, 1f * seVol);
            audioPlayer.PlayOneShot(pickBubbleSound, 12f * seVol);
            gameObject.GetComponent<PlayerMovement>().SetPMtimePassJ(0f);
            audioPlayerExtra.Stop();
            transform.localScale = gameObject.GetComponent<PlayerMovement>().GetPMoriginalScale();
            skinBright.SetActive(false);
            skin.SetActive(true);
            gameObject.GetComponent<PlayerMovement>().SetPMjumpForce(1f);
            skinBubble.SetActive(true);
            OnBubblePick?.Invoke();
            hasBubble = true;
            InvokeRepeating("PreRemoveBubble", playerData.GetPDbubbleDuration() - 4f, 0.3f);
            Invoke("RemoveBubble", playerData.GetPDbubbleDuration());
        }
        if(other.gameObject.CompareTag("WBullet")){
            if(hasArmor){
                Instantiate(wizardBulletHitPrefab, new Vector3(transform.position.x - 1.5f, transform.position.y + 1, transform.position.z), Quaternion.identity);
                audioPlayer.PlayOneShot(blockArmorSound, 1f * seVol);
            }else{
                GetComponent<PlayerMovement>().GetPMaudioPlayerExtra().Stop();
                OnDeath?.Invoke();
                isDead = true;
                GetComponent<PlayerMovement>().GetPMvCam0().SetActive(false);
                GetComponent<PlayerMovement>().GetPMvCam1().SetActive(false);
                vCam2.SetActive(true);
                ppGlobal.SetActive(false);
                ppGlobalDeath.SetActive(true);
                Time.timeScale = 0.5f;
                Instantiate(wizardBulletHitPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
                audioPlayer.PlayOneShot(wizardBulletKillSound, 0.8f * seVol);
                audioPlayer.PlayOneShot(killedSound, 0.5f * seVol);
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                gameObject.GetComponent<PlayerMovement>().SetPMrbPlayerVelocity(Vector3.zero);
                gameObject.GetComponent<PlayerMovement>().SetPMrbPlayerGravity(false);
                gameObject.GetComponent<SphereCollider>().enabled = false;
                skin.SetActive(false);
                skinBright.SetActive(false);
                skinShift.SetActive(false);
                skinArmor.SetActive(false);
                skinBubble.SetActive(false);
                Invoke("Respawn", 1f);
            }
        }
    }

    private void FalseJump(){
        GetComponent<PlayerMovement>().SetPMcanJump(false);
    }

    private void FirstTime(){
        firstTimeCollision = false;
    }

    private void Respawn(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void RemoveArmor(){
        if(hasArmor && !isDead){
            audioPlayer.PlayOneShot(pickArmorSound, 1f * seVol);
        }
        hasArmor = false;
        skinArmor.SetActive(false);
        Instantiate(armorBreakPrefab, transform.position, Quaternion.identity);
        OnArmorPickOrBreak?.Invoke();
        UI.GetComponent<UIManager>().SetUIMtimePassA(playerData.GetPDarmorDuration());
    }

    private void PreRemoveArmor(){
        if(hasArmor && !isDead){
            skinArmor.SetActive(!skinArmor.activeInHierarchy);
        } else if(!isDead){
            CancelInvoke("PreRemoveArmor");
        } else{
            skinArmor.SetActive(false);
        }
    }

    private void RemoveBubble(){
        if(hasBubble && !isDead){
            audioPlayer.PlayOneShot(pickBubbleSound, 12f * seVol);
        }
        hasBubble = false;
        skinBubble.SetActive(false);
        Instantiate(bubbleBreakPrefab, transform.position, Quaternion.identity);
        OnBubbleBreak?.Invoke();
        UI.GetComponent<UIManager>().SetUIMtimePassB(playerData.GetPDbubbleDuration());
    }

    private void PreRemoveBubble(){
        if(hasBubble && !isDead){
            skinBubble.SetActive(!skinBubble.activeInHierarchy);
        } else if(!isDead){
            CancelInvoke("PreRemoveBubble");
        } else{
            skinBubble.SetActive(false);
        }
    }
}