using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaJuego : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
