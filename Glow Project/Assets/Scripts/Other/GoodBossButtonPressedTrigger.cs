using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoodBossButtonPressedTrigger : MonoBehaviour
{
    //RUNTIME
    [SerializeField] private GameObject button;
    private bool firstTime = true;

    //FUNCTIONS
    void Update()
    {
        if(button.GetComponent<ButtonCollision>().GetBCisPressed() && firstTime){
            firstTime = false;
            Invoke("DelayedReturnToMenu", 3f);
        }
    }

    private void DelayedReturnToMenu(){
        SceneManager.LoadScene(0);
        Cursor.visible = true;
    }
}
