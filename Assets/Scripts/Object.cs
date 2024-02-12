using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script vinculado al GameObject del objeto - Object
 */
public class Object : MonoBehaviour
{
    // Variable de apoyo para marcar cuando se encuentra en el area
    private bool estaCerca = false;
    // Objeto vinculado al canvas que muestra el texto
    [SerializeField]private Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
        // Al iniciar el juego, el canvas se oculta
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Se comprueba si se encuentra en la zona requerida y si se pulsa el boton requerido
        if (estaCerca && Input.GetMouseButtonUp(0))
        {
            AgarrarObjeto();
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
            canvas.enabled = false;
            // Desactivacion del indicador de zona
            estaCerca = false;
            // Señal para desarrollo
            Debug.Log("Se ha salido del collider");
        }
    }

    /*
     * Metodo de apoyo para ejecutar cuando se vaya recoger el objeto
     */
    void AgarrarObjeto()
    {
        // Señal para desarrollo
        Debug.Log("OBJETO RECOGIDO");
        // Destruccion del objeto
        Destroy(gameObject);
    }
}
