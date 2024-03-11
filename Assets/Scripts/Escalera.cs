using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script vinculado al GameObject de la escalera - Escalera
 */
public class Escalera: MonoBehaviour
{
    // Variable de apoyo para marcar cuando se encuentra en el area
    private bool estaCerca = false;
    // Objeto vinculado al canvas que muestra el texto
    //[SerializeField]private Canvas canvas;
    // Objeto para vincular la referencia al jugador
    [SerializeField] private GameObject player;
    // Objeto para vincular la referencia al punto alto de la escalera
    [SerializeField] private GameObject puntoAlto;
    // Objeto para vincular la referencia al punto bajo de la escalera
    [SerializeField] private GameObject puntoBajo;

    // Start is called before the first frame update
    void Start()
    {
        // Al iniciar el juego, el canvas se oculta
        //canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Se activa la funcion para subir/bajar solamente cuando los colliders esten contactando y se pulse el boton principal del raton
        if (Input.GetMouseButtonUp(0) && estaCerca)
        {
            // Obtener las posiciones absolutas de los puntos alto y bajo con respecto al mundo
            Vector3 posicionPuntoAlto = puntoAlto.transform.position;
            Vector3 posicionPuntoBajo = puntoBajo.transform.position;
            // Obtener la posicion absoluta del jugador
            Vector3 posicionJugador = player.transform.position;

            // Verificar si el jugador esta mas cerca del punto alto o del punto bajo
            if (Vector3.Distance(posicionJugador, posicionPuntoAlto) < Vector3.Distance(posicionJugador, posicionPuntoBajo))
            {
                // El jugador esta mas cerca del punto alto, por lo que se desplaza hacia abajo
                StartCoroutine(Bajar(posicionPuntoAlto, posicionPuntoBajo));
            }
            else
            {
                // El jugador esta mas cerca del punto bajo, por lo que se desplaza hacia arriba
                StartCoroutine(Subir(posicionPuntoBajo, posicionPuntoAlto));
            }
        }
    }

    /*
     * Metodo que se activa cuando un collider externo entra en el collider del objeto
     */
    private void OnTriggerEnter(Collider other)
    {
        // Comprobacion de que ese objeto sea el del jugador
        if (other.CompareTag("Jugador"))
        {
            // Activacion del objeto que muestra el texto
            //canvas.enabled = true;
            // Activacion del indicador de zona
            estaCerca = true;
        }
    }

    /*
     * Metodo que se activa cuando un collider externo sale del collider del objeto
     */
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            // Desactivacion del objeto que muestra el texto
            //canvas.enabled = false;
            // Desactivacion del indicador de zona
            estaCerca = false;
        }
    }

    /*
     * Corrutina para realizar el movimiento de subida
     * 
     * Parametros: Posicion inicial, Posicion final
     */
    IEnumerator Subir(Vector3 inicio, Vector3 fin)
    {
        // Variable para el tiempo transcurrido
        float t = 0f;
        // Tiempos asignados a cada movimiento
        float tiempoVertical = 2f;
        float tiempoHorizontal = 0.7f;

        // Se realiza primero el movimiento vertical y, al completarlo, se desplaza horizontalmente

        // Mientras la altura del jugador siga siendo menor a la altura del punto
        while (player.transform.position.y < fin.y && t < tiempoVertical) 
        {
            t += Time.deltaTime;
            player.transform.position = Vector3.Lerp(inicio, new Vector3(inicio.x, fin.y, inicio.z), t / tiempoVertical);
            
            yield return null;
        }
        // Obtencion de la posicion en la que se encuentra el jugador al terminar el desplazamiento vertical para realizar el siguiente movimiento
        Vector3 posicionIntermedia = player.transform.position;
        // Reinicializacion del tiempo transcurrido
        t = 0f;
        // Mientras aun no se haya llegado a la posicion
        while (player.transform.position != fin && t < tiempoHorizontal)
        {
            t += Time.deltaTime;
            player.transform.position = Vector3.Lerp(posicionIntermedia, fin, t / tiempoHorizontal);
            
            yield return null;
        }
    }

    /*
     * Corrutina para realizar el movimiento de bajada
     * 
     * Parametros: Posicion inicial, Posicion final
     */
    IEnumerator Bajar(Vector3 inicio, Vector3 fin)
    {
        // Variable para el tiempo transcurrido
        float t = 0f;
        // Tiempos asignados a cada movimiento
        float tiempoVertical = 2f;
        float tiempoHorizontal = 0.7f;

        // Se realiza primero el movimiento horizontal

        // Mientras aun no se haya llegado a la posicion
        while (player.transform.position != fin && t < tiempoHorizontal)
        {
            t += Time.deltaTime;
            player.transform.position = Vector3.Lerp(inicio, new Vector3(fin.x, inicio.y, fin.z), t / tiempoHorizontal);

            yield return null;
        }
        // Obtencion de la posicion en la que se encuentra el jugador al terminar el desplazamiento vertical para realizar el siguiente movimiento
        Vector3 posicionIntermedia = player.transform.position;
        // Reinicializacion del tiempo transcurrido
        t = 0f;
        
        // Mientras no se haya llegado al punto bajo
        while (player.transform.position.y > fin.y && t < tiempoVertical)
        {
            t += Time.deltaTime;
            player.transform.position = Vector3.Lerp(posicionIntermedia, fin, t / tiempoVertical);

            yield return null;
        }
    }

}