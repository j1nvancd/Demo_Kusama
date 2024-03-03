using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class CodigoPausa : MonoBehaviour
{
    //Referencia al AudioMixer
    [SerializeField] private AudioMixer audioMixer;

    public GameObject ObjetoMenuPausa;
    public bool Pausa = false;
    public GameObject MenuSalir;

    //Poner a pantalla completa el juego en el menu de opciones
    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    //Cambiar el volumen del juego
    public void CambiarVolumen(float volumen)
    {
        //Volumen del audiomixer = al volumen del parámetro
        audioMixer.SetFloat("Volumen", volumen);
    }

    //Cambiar la calidad de los gráficos
    public void CambiarCalidad(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    void Start()
    {
        
    }

    void Update()
    {
        //Si se  presiona la tecla escape
        if(Input.GetKeyDown(KeyCode.Escape))
        {   //Y el juego no esta en pausa
            if (Pausa == false)
            {   //El juego se pausa
                ObjetoMenuPausa.SetActive(true);
                Pausa = true;

                //Y se para por completo
                Time.timeScale = 0;
                //Para que se vea el ratón
                Cursor.visible = true;
                //Para que el raton no este bloqueado
                Cursor.lockState = CursorLockMode.None;

                //Buscamos las fuentes de tipo audio en la escena
                AudioSource[] sonidos = FindObjectsOfType<AudioSource>();

                //Pausar todas las pistas de audio encontradas
                for (int i = 0; i < sonidos.Length; i++)
                {
                    sonidos[i].Pause();
                }
            }
            //Si pulso otra vez la tecla escape, se llama al metodo resumir y por tanto se reanuda el juego
            else if (Pausa == true) 
            {
                Resumir();
            }
        }
    }
    //Ponemos exactamente lo mismo que en el Update per al revés porque queremos que hago justo lo contrario al menú de pausa
    public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        //Para que al pulsar escape otra vez no salga este menu en vez del primero
        MenuSalir.SetActive(false);
        Pausa = false;

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Buscamos las fuentes de tipo audio en la escena
        AudioSource[] sonidos = FindObjectsOfType<AudioSource>();

        //Reproducir todas las pistas de audio encontradas
        for (int i = 0; i < sonidos.Length; i++)
        {
            sonidos[i].Play();
        }
    }

    //Ir al menu Principal
    public void IrAlMenu(string NombreMenu)
    {
        SceneManager.LoadScene(NombreMenu);
    }

   
}
