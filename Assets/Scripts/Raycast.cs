using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray(transform.position, -transform.up);
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, -transform.up);
        Debug.DrawRay(ray.origin, ray.direction * 2);
    }
}
