using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPaladinController : MonoBehaviour
{
    Vector3 target;
    float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        SetNewTarget(new Vector3(
            transform.position.x + 10,
            transform.position.y,
            transform.position.z + 10
        ));
    }

    // Update is called once per frame
    void Update()
    {
        SetNewTarget(new Vector3(
            transform.position.x + 10,
            transform.position.y,
            transform.position.z + 10
        ));
        Vector3 direction = target - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.F)) {
            Animator animator = GetComponent<Animator>();
            if (animator != null) {
                    animator.Play("Push Up");
            }
        }
    }

    void SetNewTarget(Vector3 newTarget)
    {
        target = newTarget;
        transform.LookAt(target);
    }
}
