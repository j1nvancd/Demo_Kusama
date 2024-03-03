using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

//Script dedicado a crear variables para botones de diferentes menus
public class Script_Menu : MonoBehaviour
{   //Referencia al AudioMixer
    [SerializeField] private AudioMixer audioMixer;

    //Cargar el nivel en cuestión
    public void EmpezarJuego(string Nivel)
    {
        SceneManager.LoadScene(Nivel);
    }

    //Cerrar el juego
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aqui se cierra el juego");
    }

    //Poner pantalla completa
    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    //Cambiar el volumen del juego
    public void CambiarVolumen (float volumen)
    {
        //Volumen del audiomixer = al volumen del parámetro
        audioMixer.SetFloat("Volumen", volumen);
    }

    //Cambiar la calidad de los gráficos
    public void CambiarCalidad(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    //Cargar la escena donde crearemos una partida
    public void GoToNewGame()
    {
        SceneManager.LoadScene("P_NewGame");
    }

    //Cargar el menu principal
    public void GoToMenu()
    {
        SceneManager.LoadScene("Principal");
    }

    //Cargar el la escena donde estan todas las partidas guardadas
    public void GoToMenuCarga()
    {
        SceneManager.LoadScene("P_Menu");
    }
}