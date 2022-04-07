using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatExplosionDestroy : MonoBehaviour
{
    //FUNCTIONS
    void Start()
    {
        Invoke("DestroyParticle", 2f);
    }

    private void DestroyParticle(){
        Destroy(gameObject);
    }
}
