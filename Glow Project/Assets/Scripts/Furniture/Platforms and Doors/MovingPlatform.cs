using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //DESIGN
    [SerializeField] protected float speed = 0f;

    //RUNTIME
    [SerializeField] protected GameObject[] vWaypoints;
    protected int index = 0;
    private bool goBack = false;
    protected bool ok = true;

    //FUNCTIONS
    void FixedUpdate()
    {
        if(ok){
            MoveWithWaypoints();
        }
    }

    protected virtual void MoveWithWaypoints(){
        Vector3 vector = vWaypoints[index].transform.position - transform.position;
        Vector3 direction = vector.normalized;
        if(vector.magnitude >= 0.1){
        transform.position += speed * direction * Time.deltaTime;
        } else{
            ok = false;
            Invoke("Wait", 1.5f);
            if(index < vWaypoints.Length-1 && !goBack){
                index++;
            } else if(index == vWaypoints.Length-1 || goBack){
                goBack = true;
                index--;
                if(index == 0){
                    goBack = false;
                }
            }
        }
    }

    private void Wait(){
        ok = true;
    }
}
