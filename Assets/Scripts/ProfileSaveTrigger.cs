using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script dedicado a que si se toca un objeto en especifico se ejecuta la función StorePlayerProfile
public class ProfileSaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Recibe al player porque es el unico objeto que hay en este caso
        profileStorage.StorePlayerProfile(other.gameObject);
    }
}
