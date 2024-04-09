using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
#region Menús
public GameObject PlayMenu;
public GameObject SettingsMenu;
public GameObject ExitMenu;
public GameObject GameSettingsMenu;
public GameObject ControlsSettingsMenu;
public GameObject KeySettingsMenu;
public GameObject MenuPrincipal;
#endregion

#region Escenas y dificultad
public string newGameScene = "Level01";
private string selectedDifficulty = "Normal";
#endregion

#region Elementos de interfaz de usuario
public Button ReturnButton;
public Slider progressBar;
//public TextMeshProUGUI PressAnyKey;
//public TextMeshProUGUI Loading;
#endregion

#region Elementos específicos de configuración
public GameObject LineGame;
public GameObject LineControll;
public GameObject LineVideo;
public GameObject LineKeys;
public GameObject LineMovementKeySettings;
public GameObject LineCombatKeySettings;
public GameObject LineGeneralKeySettings;
public GameObject MovementKeySettings;
public GameObject CombatKeySettings;
public GameObject GeneralKeySettings;
#endregion

#region Otros elementos y variables temporales
public GameObject LoadingBar;
public float duracionProgressBar = 5f;
public float velocidadParpadeo = 0.5f;
private bool progressBarActivo = false;
private Coroutine PressAnyKeyCoroutine;
private Coroutine LoadingCoroutine;
#endregion

   void Start()
    {
        // Desactivar elementos de UI al inicio, excepto el texto de carga
        progressBar.gameObject.SetActive(false); 
        //PressAnyKey.gameObject.SetActive(false);
        //Loading.gameObject.SetActive(true); 

        // Iniciar la corrutina para el texto de carga
        LoadingCoroutine = StartCoroutine(FadeLoading()); 
    }

    
    void Update()
    {
        // Comprobar si se presiona Enter o Intro para avanzar al siguiente nivel
        NextLevelKeyboard();
    }
    
    // Comprobar si se presiona Enter o Intro para avanzar al siguiente nivel
    private void NextLevelKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            NextLevel(); // Llamar al método para avanzar al siguiente nivel
        }
    }

    // Método para avanzar al siguiente nivel
    public void NextLevel()
    {
        MenuPrincipal.SetActive(false); // Ocultar el menú principal
        LoadingBar.SetActive(true); // Mostrar la barra de carga

        // Iniciar la barra de progreso
        StartCoroutine(AumentarProgressBar());
    }

    // Corrutina para aumentar la barra de progreso de carga
    IEnumerator AumentarProgressBar()
    {
        progressBar.gameObject.SetActive(true); // Mostrar la barra de progreso
        progressBarActivo = true; // Establecer la barra de progreso como activa

        float tiempoInicio = Time.time; // Tiempo de inicio
        float progreso = 0f; // Progreso de carga

        // Incrementar el progreso hasta que alcance el valor máximo
        while (progreso < 1f)
        {
            progreso = (Time.time - tiempoInicio) / duracionProgressBar; // Calcular el progreso
            progressBar.value = progreso; // Actualizar el valor de la barra de progreso
            yield return null;
        }

        // Mostrar el texto "Press Any Key" al completarse la carga
        //PressAnyKey.gameObject.SetActive(true);
        PressAnyKeyCoroutine = StartCoroutine(Parpadear());

        // Esperar a que el jugador presione cualquier tecla
        yield return new WaitUntil(() => Input.anyKeyDown);

        // Cargar la siguiente escena
        SceneManager.LoadScene(newGameScene);
    }

    // Corrutina para hacer parpadear el texto "Press Any Key"
    IEnumerator Parpadear()
    {
        while (true)
        {
            //Color colorActual = PressAnyKey.color;
            float alpha = Mathf.PingPong(Time.time * velocidadParpadeo, 1); // Calcular el valor de alpha para el parpadeo

            //colorActual.a = alpha; // Establecer el valor de alpha en el color actual
            //PressAnyKey.color = colorActual; // Aplicar el color al texto

            yield return null;
        }
    }

    // Corrutina para hacer parpadear el texto "Loading"
    IEnumerator FadeLoading() 
    {
        while (true)
        {
            //Color colorActual = Loading.color;
            float alpha = Mathf.PingPong(Time.time * velocidadParpadeo, 1); // Calcular el valor de alpha para el parpadeo

            //colorActual.a = alpha; // Establecer el valor de alpha en el color actual
            //Loading.color = colorActual; // Aplicar el color al texto

            yield return null;
        }
    }

    // Método que se llama cuando se destruye el objeto
    private void OnDestroy()
    {
        // Detener las corrutinas para evitar fugas de memoria
        if (PressAnyKeyCoroutine != null)
            StopCoroutine(PressAnyKeyCoroutine);

        if (LoadingCoroutine != null)
            StopCoroutine(LoadingCoroutine);
    }
/*
    // Método para seleccionar la dificultad Normal
    public void NormalDif()
    {
        // Mostrar elementos de dificultad Normal y ocultar los de Hardcore
        if (NormalDiff != null)
        {
            NormalDiff.SetActive(true);
        }

        if (HardcoreDiff != null)
        {
            HardcoreDiff.SetActive(false);
        }
    }

    // Método para seleccionar la dificultad Hardcore
    public void HardcoreDif()
    {
        // Mostrar elementos de dificultad Hardcore y ocultar los de Normal
        if (HardcoreDiff != null)
        {
            HardcoreDiff.SetActive(true);
        }

        if (NormalDiff != null)
        {
            NormalDiff.SetActive(false);
        }
    }
*/
    // Método para iniciar un nuevo juego
    public void StartGame()
    {
        ToggleSubMenu(PlayMenu); // Mostrar el menú de juego
    }

    // Método para continuar el juego (aún no implementado)
    public void ContinueGame()
    {
        // Funcionalidad aún por implementar
    }

    // Método para cargar un juego guardado (aún no implementado)
    public void LoadGame()
    {
        // Funcionalidad aún por implementar
    }

    // Método para abrir el menú de configuración
    public void OpenSettings()
    {
        ToggleSubMenu(SettingsMenu); // Mostrar el menú de configuración
    }

    // Métodos para abrir diferentes submenús de configuración
    public void OpenGameSettings()
    {
        // Mostrar el submenú de configuración del juego y ocultar los demás
        GameSettingsMenu.SetActive(true);
        ControlsSettingsMenu.SetActive(false);
        KeySettingsMenu.SetActive(false);

        // Mostrar la línea de indicación de la sección activa y ocultar las demás
        LineGame.SetActive(true);
        LineControll.SetActive(false);
        LineVideo.SetActive(false);
        LineKeys.SetActive(false);
    }

    public void OpenControlsSettings()
    {
        // Mostrar el submenú de configuración de controles y ocultar los demás
        GameSettingsMenu.SetActive(false);
        ControlsSettingsMenu.SetActive(true);
        KeySettingsMenu.SetActive(false);

        // Mostrar la línea de indicación de la sección activa y ocultar las demás
        LineGame.SetActive(false);
        LineControll.SetActive(true);
        LineVideo.SetActive(false);
        LineKeys.SetActive(false);
    }

    public void OpenKeySettings()
    {
        // Mostrar el submenú de configuración de teclas y ocultar los demás
        GameSettingsMenu.SetActive(false);
        ControlsSettingsMenu.SetActive(false);
        KeySettingsMenu.SetActive(true); 

        // Mostrar la línea de indicación de la sección activa y ocultar las demás
        LineGame.SetActive(false);
        LineControll.SetActive(false);
        LineVideo.SetActive(true);
        LineKeys.SetActive(false);
    }

    // Métodos para abrir diferentes submenús de configuración de teclas
    public void OpenMovementKeySettings()
    {
        // Mostrar la configuración de teclas de movimiento y ocultar las demás
        MovementKeySettings.SetActive(true);
        CombatKeySettings.SetActive(false);
        GeneralKeySettings.SetActive(false);

        // Mostrar la línea de indicación de la sección activa y ocultar las demás
        LineMovementKeySettings.SetActive(true);
        LineCombatKeySettings.SetActive(false);
        LineGeneralKeySettings.SetActive(false);
    }

    public void OpenCombatKeySettings()
    {
        // Mostrar la configuración de teclas de combate y ocultar las demás
        MovementKeySettings.SetActive(false);
        CombatKeySettings.SetActive(true);
        GeneralKeySettings.SetActive(false);
        
        // Mostrar la línea de indicación de la sección activa y ocultar las demás
        LineMovementKeySettings.SetActive(false);
        LineCombatKeySettings.SetActive(true);
        LineGeneralKeySettings.SetActive(false);
    }

    public void OpenGeneralKeySettings()
    {
        // Mostrar la configuración de teclas generales y ocultar las demás
        MovementKeySettings.SetActive(false);
        CombatKeySettings.SetActive(false);
        GeneralKeySettings.SetActive(true);
        
        // Mostrar la línea de indicación de la sección activa y ocultar las demás
        LineMovementKeySettings.SetActive(false);
        LineCombatKeySettings.SetActive(false);
        LineGeneralKeySettings.SetActive(true);
    }

    // Método para volver al menú principal desde cualquier submenú
    public void ReturnToMainMenu()
    {
        ToggleSubMenu(SettingsMenu); // Ocultar el menú de configuración
    }

    // Método para abrir el menú de salida
    public void OpenExitMenu()
    {
        ToggleSubMenu(ExitMenu); // Mostrar el menú de salida
    }

    // Método para confirmar la salida del juego
    public void ConfirmExit(bool confirm)
    {
        if (confirm)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Salir del editor de Unity
            #else
                Application.Quit(); // Salir de la aplicación
            #endif
        }
        else
        {
            ToggleSubMenu(ExitMenu); // Ocultar el menú de salida si se cancela
        }
    }

    // Método para cambiar el estado de activación de un submenú
    private void ToggleSubMenu(GameObject subMenu)
    {
        bool isActive = subMenu.activeSelf; // Obtener el estado actual del submenú

        // Ocultar todos los submenús excepto el que se está mostrando actualmente
        PlayMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        ExitMenu.SetActive(false);

        // Mostrar u ocultar el submenú según su estado actual
        subMenu.SetActive(!isActive);
    }
}