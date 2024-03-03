using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Literalmente un método para irse al menú principal
public class Salir : MonoBehaviour
{
    public void SalirAlMenuPrincipal()
    {
        SceneManager.LoadScene("Principal");
    }
}
