using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCamera : MonoBehaviour
{
    //RUNTIME
    [SerializeField] private GameObject player;

    //FUNCTIONS
    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
}
