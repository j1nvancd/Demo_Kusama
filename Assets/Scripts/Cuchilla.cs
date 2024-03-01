using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuchilla : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Maleza")
        {
            Destroy(other.gameObject);
        }
    }
}
