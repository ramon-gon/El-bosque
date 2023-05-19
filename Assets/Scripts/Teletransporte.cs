using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletransporte : MonoBehaviour
{
    public Transform Target;
    

    private void OnTriggerEnter(Collider other){
        
        
            other.transform.position = Target.transform.position;
        
        
        
    }
}
