using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script vinculado al GameObject del objeto - Object
 */
public class Objeto : MonoBehaviour
{
    // Variable de apoyo para marcar cuando se encuentra en el area
    private bool estaCerca = false;
    // Objeto vinculado al canvas que muestra el texto
    [SerializeField]private Canvas canvas;
    // Variable para almacenar al jugador
    private Player jugador;
    // Variables para almacenar el estado del objeto
    // Recogido = el jugador ha recogido el objeto
    private bool recogido;
    // Soltado = el jugador ha soltado el objeto
    public bool soltado;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializacion de las variables de estado
        recogido = false;
        soltado = false;
        jugador = Player.jugador;
        // Al iniciar el juego, el canvas se oculta
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Se comprueba si se encuentra en la zona requerida y si se pulsa el boton requerido
        if (estaCerca && Input.GetMouseButtonUp(0) && !soltado && !recogido)
        {
            AgarrarObjeto();
        }
        // En caso de que haya sido recogido pero aun no se haya soltado, el objeto sigue al jugador
        if (recogido && !soltado)
        {
            transform.position = new Vector3(jugador.transform.position.x - 1, jugador.transform.position.y, jugador.transform.position.z - 1);
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
            canvas.enabled = true;
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
            canvas.enabled = false;
            // Desactivacion del indicador de zona
            estaCerca = false;
        }
    }

    /*
     * Metodo de apoyo para ejecutar cuando se vaya recoger el objeto
     */
    void AgarrarObjeto()
    {
        // Asociacion del objeto Jugador de la jerarquia
        Player jugador = FindObjectOfType<Player>();
        // En caso de que el jugador no sea nulo y el jugador no haya recogido un objeto aun, se cambia el estado del objeto y se recoge el mismo
        if (jugador != null && jugador.objeto == null)
        {
            recogido = true;
            jugador.recogerObjeto(this);
        }
    }
}
