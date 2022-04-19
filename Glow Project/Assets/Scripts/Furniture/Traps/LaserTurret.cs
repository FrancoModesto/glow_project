using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserTurret : MonoBehaviour
{
    //DESIGN
    [SerializeField] private GameObject[] buttonArray;

    //RUNTIME
    private AudioSource childAudio;
    [SerializeField] private GameObject shootpoint;
    [SerializeField] private GameObject laserKillPrefab;
    [SerializeField] private GameObject batExplosionPrefab;
    [SerializeField] private GameObject slimeBloodPrefab;
    [SerializeField] private AudioClip laserKillSound;
    [SerializeField] private AudioClip batExplosionSound;
    [SerializeField] private AudioClip slimeDieSound;
    private GameObject player;
    private LineRenderer laser;
    private int arePressed = 0;
    private float rayDistance = 100f;
    private bool canKill = true;
    private bool canKillBat = true;
    private bool canKillSlime = true;
    private bool enableLaser = true;
    private RaycastHit batHit;
    private RaycastHit slimeHit;
    private float seVol = 1f;

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        laser = GetComponent<LineRenderer>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }
        
        if(buttonArray.Length != 0){
            for(int i=0;i<buttonArray.Length;i++){
                if(buttonArray[i].GetComponent<ButtonCollision>().GetBCisPressed()){
                    arePressed++;
                }
            }

            if(arePressed == buttonArray.Length){
                enableLaser = false;
                laser.enabled = false;
            } else{
                enableLaser = true;
                laser.enabled = true;
            }

            arePressed = 0;
        }
        
        if(enableLaser){
            ShootLaser();
        }
    }

    private void ShootLaser(){
        RaycastHit hit;
        if(Physics.Raycast(shootpoint.transform.position, shootpoint.transform.TransformDirection(Vector3.forward), out hit, rayDistance)){
            if(hit.collider != null){
                laser.SetPosition(0, shootpoint.transform.position);
                laser.SetPosition(1, hit.point);
            }
            if(hit.transform.gameObject.CompareTag("Player") && canKill){
                if(!player.GetComponent<PlayerCollision>().GetPChasArmor()){
                    KillPlayer();
                }
            }
            if(hit.transform.gameObject.CompareTag("Bat") && canKillBat){
                canKillBat = false;
                batHit = hit;
                player.GetComponent<PlayerCollision>().GetPCaudioPlayer().PlayOneShot(laserKillSound, 2f * seVol);
                player.GetComponent<PlayerCollision>().GetPCaudioPlayer().PlayOneShot(batExplosionSound, 1.5f * seVol);
                Instantiate(batExplosionPrefab, hit.transform.gameObject.transform.position, Quaternion.identity);
                Instantiate(laserKillPrefab, new Vector3(hit.transform.gameObject.transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                hit.transform.gameObject.GetComponent<BatMovement>().enabled = false;
                hit.transform.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                hit.transform.gameObject.layer = 3;
                Invoke("DelayedBatDestroy", 0.2f);
            }
            if(hit.transform.gameObject.CompareTag("Slime") && canKillSlime){
                if(GameManager.instance != null){
                    GameManager.instance.lvlKilledSlimes++;
                }
                canKillSlime = false;
                slimeHit = hit;
                player.GetComponent<PlayerCollision>().GetPCaudioPlayer().PlayOneShot(laserKillSound, 2f * seVol);
                player.GetComponent<PlayerCollision>().GetPCaudioPlayer().PlayOneShot(slimeDieSound, 2f * seVol);
                Instantiate(slimeBloodPrefab, new Vector3(hit.transform.gameObject.transform.position.x, hit.transform.gameObject.transform.position.y + 0.5f, hit.transform.gameObject.transform.position.z), slimeBloodPrefab.transform.localRotation);
                Instantiate(laserKillPrefab, new Vector3(hit.transform.gameObject.transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                hit.transform.gameObject.GetComponent<SlimeMovement>().enabled = false;
                foreach(Transform child in hit.transform.gameObject.transform){
                    childAudio = child.gameObject.GetComponent<AudioSource>();
                    if(childAudio == null){
                        child.gameObject.SetActive(false);
                    }
                }
                hit.transform.gameObject.layer = 3;
                Invoke("DelayedSlimeDestroy", 0.2f);
            }
        }
    }

    private void KillPlayer(){
        canKill = false;
        Instantiate(laserKillPrefab, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        player.GetComponent<PlayerMovement>().GetPMaudioPlayerExtra().Stop();
        player.GetComponent<PlayerCollision>().SetPCisDeadByLaser(true);
        player.GetComponent<PlayerCollision>().SetPCisDead(true);
        player.GetComponent<PlayerMovement>().GetPMvCam0().SetActive(false);
        player.GetComponent<PlayerMovement>().GetPMvCam1().SetActive(false);
        player.GetComponent<PlayerCollision>().GetPCvCam2().SetActive(true);
        player.GetComponent<PlayerCollision>().GetPCppGlobal().SetActive(false);
        player.GetComponent<PlayerCollision>().GetPCppGlobalDeath().SetActive(true);
        Time.timeScale = 0.5f;
        player.GetComponent<PlayerCollision>().GetPCaudioPlayer().PlayOneShot(laserKillSound, 1f * seVol);
        player.GetComponent<PlayerCollision>().GetPCaudioPlayer().PlayOneShot(player.GetComponent<PlayerCollision>().GetPCkilledSound(), 0.5f * seVol);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerMovement>().SetPMrbPlayerVelocity(Vector3.zero);
        player.GetComponent<PlayerMovement>().SetPMrbPlayerGravity(false);
        foreach(Transform child in player.transform){
            childAudio = child.gameObject.GetComponent<AudioSource>();
            if(childAudio == null){
                child.gameObject.SetActive(false);
            }
        }
        Invoke("DisableSphereCollider", 0.2f);
        Invoke("Respawn", 1f);
    }

    private void DisableSphereCollider(){
        player.GetComponent<SphereCollider>().enabled = false;
    }

    private void Respawn(){
        Time.timeScale = 1;
        if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 2){
            SceneManager.LoadScene(0);
            Cursor.visible = true;
        } else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void DelayedBatDestroy(){
        if(batHit.collider != null){
            Destroy(batHit.transform.gameObject);
            batHit = new RaycastHit();
            canKillBat = true;
        }
    }

    private void DelayedSlimeDestroy(){
        if(slimeHit.collider != null){
            Destroy(slimeHit.transform.gameObject);
            slimeHit = new RaycastHit();
            canKillSlime = true;
        }
    }
}
