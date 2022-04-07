using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //RUNTIME
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsInGameMenu;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsInGameButton;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject jumpPanel;
    [SerializeField] private GameObject jumpPanelPressed;
    [SerializeField] private GameObject shiftPanel;
    [SerializeField] private GameObject shiftPanelCooldown;
    [SerializeField] private GameObject shiftCooldownText;
    [SerializeField] private GameObject fallPanel;
    [SerializeField] private GameObject fallPanelCooldown;
    [SerializeField] private GameObject fallCooldownText;
    [SerializeField] private GameObject upPanel;
    [SerializeField] private GameObject upPanelPressed;
    [SerializeField] private GameObject downPanel;
    [SerializeField] private GameObject downPanelPressed;
    [SerializeField] private GameObject armorBoofImage;
    [SerializeField] private GameObject armorDurationText;
    [SerializeField] private GameObject bubbleBoofImage;
    [SerializeField] private GameObject bubbleDurationText;
    private GameObject player;
    private float timePassS = 0f;
    private float timePassF = 0f;
    private float timePassA = 0f;
    private float timePassB = 0f;

    //GETTER Y SETTER
    public GameObject GetUIMpauseMenu(){
        return pauseMenu;
    }

    public GameObject GetUIMoptionsInGameMenu(){
        return optionsInGameMenu;
    }

    public Button GetUIMresumeButton(){
        return resumeButton;
    }

    public Button GetUIMoptionsInGameButton(){
        return optionsInGameButton;
    }

    public GameObject GetUIMhudPanel(){
        return hudPanel;
    }

    public GameObject GetUIMjumpPanel(){
        return jumpPanel;
    }

    public GameObject GetUIMjumpPanelPressed(){
        return jumpPanelPressed;
    }

    public GameObject GetUIMshiftPanel(){
        return shiftPanel;
    }

    public GameObject GetUIMshiftPanelCooldown(){
        return shiftPanelCooldown;
    }

    public GameObject GetUIMfallPanel(){
        return fallPanel;
    }

    public GameObject GetUIMfallPanelCooldown(){
        return fallPanelCooldown;
    }

    public GameObject GetUIMupPanel(){
        return upPanel;
    }

    public GameObject GetUIMupPanelPressed(){
        return upPanelPressed;
    }

    public GameObject GetUIMdownPanel(){
        return downPanel;
    }

    public GameObject GetUIMdownPanelPressed(){
        return downPanelPressed;
    }

    public void SetUIMtimePassS(float value){
        timePassS = value;
    }

    public void SetUIMtimePassF(float value){
        timePassF = value;
    }
    
    public void SetUIMtimePassA(float value){
        timePassA = value;
    }

    public void SetUIMtimePassB(float value){
        timePassB = value;
    }

    //FUNCTIONS
    void Start()
    {
        player = GameObject.Find("Player");
        timePassS = player.GetComponent<PlayerMovement>().GetPMplayerData().GetPDshiftCooldown();
        timePassF = player.GetComponent<PlayerMovement>().GetPMplayerData().GetPDfallCooldown();
        timePassA = player.GetComponent<PlayerMovement>().GetPMplayerData().GetPDarmorDuration();
        timePassB = player.GetComponent<PlayerMovement>().GetPMplayerData().GetPDbubbleDuration();

        FindObjectOfType<PlayerCollision>().OnDeath += DisableHUD;
        FindObjectOfType<PlayerCollision>().OnArmorPickOrBreak += ArmorBoofImageSwitch;
        FindObjectOfType<PlayerCollision>().OnBubblePick += BubbleBoofImageEnable;
        FindObjectOfType<PlayerCollision>().OnBubbleBreak += BubbleBoofImageDisable;
    }

    void Update()
    {
        if(!player.GetComponent<PlayerMovement>().GetPMcanShift()){
            timePassS += -Time.deltaTime;
            if(timePassS >= 1){
                shiftCooldownText.GetComponent<TMPro.TextMeshProUGUI>().text = (Mathf.Round(timePassS)).ToString();
            } else{
                shiftCooldownText.GetComponent<TMPro.TextMeshProUGUI>().text = "1";
            }
        }

        if(!player.GetComponent<PlayerMovement>().GetPMcanFall()){
            timePassF += -Time.deltaTime;
            if(timePassF >= 1){
                fallCooldownText.GetComponent<TMPro.TextMeshProUGUI>().text = (Mathf.Round(timePassF)).ToString();
            } else{
                fallCooldownText.GetComponent<TMPro.TextMeshProUGUI>().text = "1";
            }
        }

        if(player.GetComponent<PlayerCollision>().GetPChasArmor()){
            timePassA += -Time.deltaTime;
            if(timePassA >= 1){
                armorDurationText.GetComponent<TMPro.TextMeshProUGUI>().text = (Mathf.Round(timePassA)).ToString();
            } else{
                armorDurationText.GetComponent<TMPro.TextMeshProUGUI>().text = "1";
            }
        }

        if(player.GetComponent<PlayerCollision>().GetPChasBubble()){
            timePassB += -Time.deltaTime;
            if(timePassB >= 1){
                bubbleDurationText.GetComponent<TMPro.TextMeshProUGUI>().text = (Mathf.Round(timePassB)).ToString();
            } else{
                bubbleDurationText.GetComponent<TMPro.TextMeshProUGUI>().text = "1";
            }
        }
    }

    private void DisableHUD(){
        hudPanel.SetActive(false);
    }

    private void ArmorBoofImageSwitch(){
        armorBoofImage.SetActive(!armorBoofImage.activeSelf);
    }

    private void BubbleBoofImageEnable(){
        bubbleBoofImage.SetActive(true);
        jumpPanel.SetActive(false);
        jumpPanelPressed.SetActive(false);
        upPanel.SetActive(true);
        downPanel.SetActive(true);
    }

    private void BubbleBoofImageDisable(){
        bubbleBoofImage.SetActive(false);
        upPanel.SetActive(false);
        upPanelPressed.SetActive(false);
        downPanel.SetActive(false);
        downPanelPressed.SetActive(false);
        jumpPanel.SetActive(true);
    }
}
