using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningPlatform : MonoBehaviour
{
    //DESIGN
    [SerializeField] private GameObject[] buttonArray;

    //RUNTIME
    private GameObject player;
    [SerializeField] private GameObject winPlatformSkin;
    [SerializeField] private GameObject winPlatformWinSkin;
    private int arePressed = 0;

    //FUNCTIONS
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(buttonArray.Length != 0){
            for(int i=0;i<buttonArray.Length;i++){
                if(buttonArray[i].GetComponent<ButtonCollision>().GetBCisPressed()){
                    arePressed++;
                }
            }

            if(arePressed == buttonArray.Length){
                player.GetComponent<PlayerCollision>().SetPCmissionOk(true);
            } else{
                player.GetComponent<PlayerCollision>().SetPCmissionOk(false);
            }

            arePressed = 0;
        } else{
            player.GetComponent<PlayerCollision>().SetPCmissionOk(true);
        }

        if(player.GetComponent<PlayerCollision>().GetPCmissionOk()){
            winPlatformSkin.SetActive(false);
            winPlatformWinSkin.SetActive(true);

        } else{
            winPlatformWinSkin.SetActive(false);
            winPlatformSkin.SetActive(true);
        }
    }
}
