using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : MonoBehaviour
{
    //DESIGN
    [SerializeField] private float shootCooldown = 0f;
    [SerializeField] private float speedToLook = 0f;

    //RUNTIME
    private AudioSource audioWizard;
    [SerializeField] private AudioClip shootSound;
    private GameObject player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletAcumulator;
    [SerializeField] private GameObject shootpoint;
    [SerializeField] private GameObject wizardRange;
    [SerializeField] private Animator wizardAnimator;
    private bool canShoot = true;
    private bool dead = false;
    private float seVol = 1f;

    //GETTER y SETTER
    public bool GetWMdead(){
        return dead;
    }

    public void SetWMdead(bool status){
        dead = status;
    }

    public Animator GetWMwizardAnimator(){
        return wizardAnimator;
    }

    public GameObject GetWMwizardRange(){
        return wizardRange;
    }

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }
        
        audioWizard = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    void Update()
    {   
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }
        
        if(!player.GetComponent<PlayerCollision>().GetPCwin()){
            if(wizardRange.GetComponent<WizardRange>().GetWRisOnRange() && !dead){
            LookAtPlayerLerp();
            ShootpointLookAtPlayerLerp();
        }
        if(canShoot && wizardRange.GetComponent<WizardRange>().GetWRisOnRange() && !dead){
            wizardAnimator.SetBool("isToAttack", true);
            Invoke("WizardShoot", 0.2f);
            canShoot = false;
            Invoke("WizardCanShoot", shootCooldown);
        } else if(!canShoot){
            wizardAnimator.SetBool("isToAttack", false);
        }
        }
    }

    private void LookAtPlayerLerp(){
        Quaternion newRotation = Quaternion.LookRotation(new Vector3((player.transform.position.x - transform.position.x), 0f, (player.transform.position.z - transform.position.z)));
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime);
    }

    private void ShootpointLookAtPlayerLerp(){
        Quaternion newRotation = Quaternion.LookRotation((player.transform.position - shootpoint.transform.position));
        shootpoint.transform.rotation = Quaternion.Lerp(shootpoint.transform.rotation, newRotation, speedToLook * Time.deltaTime);
    }

    private void WizardShoot(){
        audioWizard.PlayOneShot(shootSound, 0.6f * seVol);
        GameObject newBullet = Instantiate(bulletPrefab, shootpoint.transform.position, shootpoint.transform.rotation);
        newBullet.transform.parent = bulletAcumulator.transform;
    }

    private void WizardCanShoot(){
        canShoot = true;
    }
}
