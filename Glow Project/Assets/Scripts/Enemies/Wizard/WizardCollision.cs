using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCollision : MonoBehaviour
{
    //DESIGN
    [SerializeField] private float unknockTime = 0f;

    //RUNTIME
    private AudioSource audioWizard;
    private Animator wizardAnimator;
    private GameObject wizardRange;
    [SerializeField] private AudioClip knockedSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private GameObject wizardBody;
    [SerializeField] private GameObject wizardStaff;
    private Renderer rendBody;
    private Renderer rendStaff;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material redMaterial;
    private float seVol = 1f;

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }

        audioWizard = GetComponent<AudioSource>();
        wizardAnimator = GetComponent<WizardMovement>().GetWMwizardAnimator();
        wizardRange = GetComponent<WizardMovement>().GetWMwizardRange();
        rendBody = wizardBody.GetComponent<Renderer>();
        rendStaff = wizardStaff.GetComponent<Renderer>();
    }

    void Update()
    {
        if(GameManager.instance != null){
            seVol = GameManager.instance.seVol;
        }
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerMovement>().SetPMrbPlayerVelocity(Vector3.zero);
            if(other.gameObject.GetComponent<PlayerCollision>().GetPChasArmor()){
                if(!GetComponent<WizardMovement>().GetWMdead()){
                    GetComponent<WizardMovement>().SetWMdead(true);
                    wizardAnimator.SetBool("isDead", true);
                    wizardRange.GetComponent<WizardRange>().SetWRisReallyDead(true);
                    rendBody.material = redMaterial;
                    rendStaff.material = redMaterial;
                    Invoke("RemoveRedMaterial", 0.5f);
                    audioWizard.PlayOneShot(knockedSound, 1.2f * seVol);
                    audioWizard.PlayOneShot(dieSound, 1.3f * seVol);
                    GetComponent<CapsuleCollider>().enabled = false;
                    Invoke("GetRidOfBody", 5f);
                }
            } else{
                if(!GetComponent<WizardMovement>().GetWMdead()){
                    GetComponent<WizardMovement>().SetWMdead(true);
                    wizardAnimator.SetBool("isKnocked", true);
                    wizardAnimator.SetBool("isToAttack", false);
                    audioWizard.PlayOneShot(knockedSound, 1.2f * seVol);
                    Invoke("Unknock", unknockTime);
                }
            }
        }
    }

    private void Unknock(){
        wizardAnimator.SetBool("isKnocked", false);
        Invoke("UnknockFix", 1f);
    }

    private void UnknockFix(){
        GetComponent<WizardMovement>().SetWMdead(false);
    }

    private void RemoveRedMaterial(){
        rendBody.material = normalMaterial;
        rendStaff.material = normalMaterial;
    }

    private void GetRidOfBody(){
        Destroy(gameObject);
    }
}
