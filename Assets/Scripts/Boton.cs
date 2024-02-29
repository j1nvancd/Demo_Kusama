using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script asociado al objeto - Boton que activara el objeto - puente
 */
public class Boton : MonoBehaviour
{
    // Variable para el tamaño que debe aumentarse el objeto
    [SerializeField]private float tamanyoAumento = 2f;
    // Variable para el multiplicador que se aplica a la escala
    [SerializeField] private float multiplicadorEscala = 2f;
    // Referncia al objeto que se modificara(Puente)
    [SerializeField]private GameObject puente;
    // Referencia al objeto hacia el que se modifica
    [SerializeField]private GameObject objetoReferencia;
    // Variable interna para guardar el estado del mecanismo. Si se ha pulsado el boton = true
    private bool estaPulsado = false;
    // Variables para almacenar el tamaño y la escala original del objeto
    private Vector3 escalaOriginal;
    private Vector3 posicionOriginal;
    // Variables para almacenar el tamaño y la escala final
    private Vector3 escalaFinal;
    private Vector3 posicionFinal;
    // Variable para almacenar el objeto Player
    //private Player jugador;
    // Variable para vincular el objeto requerido para el boton
    [SerializeField] private Object objetoRequerido;

    void Start()
    {
        //jugador = Player.jugador;
        // Recogida de las medidas originales
        escalaOriginal = puente.transform.localScale;
        posicionOriginal = puente.transform.position;
        // Recogida de la direccion hacia la que se aplicara la transformacion
        Vector3 direccionExtension = objetoReferencia.transform.position - puente.transform.position;

        // Dependiendo de la diferencia, se aplica la transformacion hacia un eje u otro
        if (direccionExtension.x > 0)
        {
            // Se extiende en el eje X de forma positiva
            escalaFinal = new Vector3(escalaOriginal.x * multiplicadorEscala, escalaOriginal.y, escalaOriginal.z);
            posicionFinal = posicionOriginal ;
        }
        else if (direccionExtension.x < 0)
        {
            // Se extiende en el eje X de forma negativa
            escalaFinal = new Vector3(escalaOriginal.x * multiplicadorEscala, escalaOriginal.y, escalaOriginal.z);
            posicionFinal = posicionOriginal;
        }
        else if (direccionExtension.z > 0)
        {
            // Se extiende en el eje Z de forma positiva
            escalaFinal = new Vector3(escalaOriginal.x, escalaOriginal.y, escalaOriginal.z * multiplicadorEscala);
            posicionFinal = posicionOriginal;
        }
        else if (direccionExtension.z < 0)
        {
            // Se extiende en el eje Z de forma negativa
            escalaFinal = new Vector3(escalaOriginal.x, escalaOriginal.y, escalaOriginal.z * multiplicadorEscala);
            posicionFinal = posicionOriginal;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Cambio del estado
        estaPulsado = true;
        StartCoroutine(ExtenderPuente(escalaFinal, posicionFinal));
        
    }

    private void OnCollisionExit(Collision collision)
    {
        // Cambio del estado
        estaPulsado = false;
        StartCoroutine(ContraerPuente(escalaOriginal, posicionOriginal));
        
    }

    private void OnTriggerStay(Collider other)
    {
        // Vinculacion del jugador de la jerarquia
        Player jugador = FindObjectOfType<Player>();
        // Si el jugador no es nulo, el jugador tiene un objeto recogido, el objeto que debe ir en el boton es el mismo que el que lleva el jugador
        // y se pulsa el click derecho, se suelta el objeto en el sitio
        if (jugador != null && jugador.objeto != null && objetoRequerido == jugador.objeto && Input.GetMouseButton(1)) 
        {
            jugador.soltarObjeto();
            objetoRequerido.transform.position = transform.position;
        }
    }

    /*
     * Corrutina interna para realizar la extension del puente
     */
    private IEnumerator ExtenderPuente(Vector3 escalaFinal, Vector3 posicionFinal)
    {
        // Variable para almacenar el tiempo que tarda en ejecutar la accion
        float duracion = 3f;
        // Variable para almacenar el tiempo transcurrido
        float tiempoTranscurrido = 0f;
        // Recogida de la posicion y escala inicial del objeto
        Vector3 escalaInicial = puente.transform.localScale;
        Vector3 posicionInicial = puente.transform.position;

        while (tiempoTranscurrido < duracion && estaPulsado)
        {
            float t = tiempoTranscurrido / duracion;
            puente.transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, t);
            puente.transform.position = Vector3.Lerp(posicionInicial, posicionFinal, t);

            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
    }

    /*
     * Corrutina interna para realizar la contraccion del puente 
     */
    private IEnumerator ContraerPuente(Vector3 escalaFinal, Vector3 posicionFinal)
    {
        // Variable para almacenar el tiempo que tarda en ejecutar la accion
        float duracion = 3f;
        // Variable para almacenar el tiempo transcurrido
        float tiempoTranscurrido = 0f;
        // Recogida de la posicion y escala inicial del objeto
        Vector3 escalaInicial = puente.transform.localScale;
        Vector3 posicionInicial = puente.transform.position;

        while (tiempoTranscurrido < duracion && !estaPulsado)
        {
            float t = tiempoTranscurrido / duracion;
            puente.transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, t);
            puente.transform.position = Vector3.Lerp(posicionInicial, posicionFinal, t);

            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
    }
}
