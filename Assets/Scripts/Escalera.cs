using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalera : MonoBehaviour
{
    // Variable de apoyo para marcar cuando se encuentra en el area
    private bool estaCerca = false;
    // Objeto vinculado al canvas que muestra el texto
    //[SerializeField] private Canvas canvas;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject puntoAlto;
    [SerializeField] private GameObject puntoBajo;

    private bool arriba;
    private bool escalera;

    public float velocidad = 0.1f; // Velocidad de movimiento del jugador

    private bool moviendose = false;


    // Start is called before the first frame update
    void Start()
    {
        // Al iniciar el juego, el canvas se oculta
        //canvas.enabled = false;
        arriba = false;
        escalera = false;
        Debug.Log("Punto Alto: " + puntoAlto.transform.position);
        Debug.Log("Punto Bajo: " + puntoBajo.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        if (!moviendose && Input.GetMouseButtonUp(0) && estaCerca)
        {
            // Calcular las posiciones absolutas de los puntos alto y bajo con respecto al mundo
            Vector3 posicionPuntoAlto = puntoAlto.transform.position;
            Vector3 posicionPuntoBajo = puntoBajo.transform.position;

            Vector3 posicionJugador = player.transform.position;

            // Verificar si el jugador está más cerca del punto alto o del punto bajo
            if (Vector3.Distance(posicionJugador, posicionPuntoAlto) < Vector3.Distance(posicionJugador, posicionPuntoBajo))
            {
                // El jugador está más cerca del punto alto, moverlo hacia abajo
                StartCoroutine(Bajar(posicionPuntoAlto, posicionPuntoBajo));
            }
            else
            {
                // El jugador está más cerca del punto bajo, moverlo hacia arriba
                //StartCoroutine(MoverJugador(posicionPuntoBajo, posicionPuntoAlto));
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
            // Señal para desarrollo
            Debug.Log("Se ha entrado en el collider");
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
            // Señal para desarrollo
            Debug.Log("Se ha salido del collider");
        }
    }

    IEnumerator Subir(Vector3 inicio, Vector3 fin)
    {
        moviendose = true;
        float t = 0f;
        float tiempoVertical = 2f;
        float tiempoHorizontal = 0.7f;

        while (player.transform.position.y < fin.y && t < tiempoVertical)
        {
            t += Time.deltaTime;
            player.transform.position = Vector3.Lerp(inicio, new Vector3(inicio.x, fin.y, inicio.z), t / tiempoVertical);

            yield return null;
        }
        Vector3 posicionIntermedia = player.transform.position;
        t = 0f;
        while (player.transform.position != fin && t < tiempoHorizontal)
        {
            t += Time.deltaTime;
            player.transform.position = Vector3.Lerp(posicionIntermedia, fin, t / tiempoHorizontal);

            yield return null;
        }

        moviendose = false;
    }

    IEnumerator Bajar(Vector3 inicio, Vector3 fin)
    {
        moviendose = true;
        float t = 0f;
        float tiempoVertical = 2f;
        float tiempoHorizontal = 0.7f;

        while (player.transform.position != fin && t < tiempoHorizontal)
        {
            t += Time.deltaTime;
            player.transform.position = Vector3.Lerp(inicio, new Vector3(fin.x, inicio.y, fin.z), t / tiempoHorizontal);

            yield return null;
        }
        Vector3 posicionIntermedia = player.transform.position;
        t = 0f;

        while (player.transform.position.y < fin.y && t < tiempoVertical)
        {
            t += Time.deltaTime;
            player.transform.position = Vector3.Lerp(posicionIntermedia, fin, t / tiempoVertical);

            yield return null;
        }
        //player.GetComponent<Rigidbody>().useGravity = true;
        moviendose = false;
    }

}
