using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puente : MonoBehaviour
{
    

    void Start()
    {
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Objeto")
        {
            Rigidbody box = hit.collider.GetComponent<Rigidbody>();

            if (box != null)
            {
                Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
                box.velocity = pushDirection * 3;
            }
        }
    }
}
