using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHappy : MonoBehaviour
{
    //DESIGN
    [SerializeField] private float speedToLook = 0f;

    //RUNTIME
    private GameObject player;

    //FUNCTIONS
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        LookAtPlayerLerp();
    }

    private void LookAtPlayerLerp(){
        Vector3 vector = player.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(new Vector3(vector.x, 0f, vector.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime);
    }
}
