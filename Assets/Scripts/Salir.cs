using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Literalmente un m�todo para irse al men� principal
public class Salir : MonoBehaviour
{
    public void SalirAlMenuPrincipal()
    {
        SceneManager.LoadScene("Principal");
    }
}
