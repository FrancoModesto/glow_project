using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //FUNCTIONS
    void Update()
    {
        transform.Rotate(0f, 30f * Time.deltaTime, 0f, Space.Self);
    }
}
