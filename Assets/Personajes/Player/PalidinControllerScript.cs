using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalidinControllerScript : MonoBehaviour
{
    Vector3 target;
    float speed = 1.0f;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        SetNewTarget(new Vector3(transform.position.x+10,
        transform.position.y,
        transform.position.z+10));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target-transform.position;
        transform.Translate(direction.normalized*speed*Time.deltaTime,Space.World);
   
     if (Input.GetKeyDown(KeyCode.M))
        {
            animator.SetTrigger("Die");
        }
    }

    void SetNewTarget(Vector3 newTarget){
        target = newTarget;
        transform.LookAt(target);
    }
}
