using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mechero : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Canvas canvas;
    private bool estaCerca;
    Player jugador;

    private void Start()
    {
        estaCerca = false;
        canvas.enabled = false;
        jugador = FindObjectOfType<Player>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (estaCerca)
        {
            if (eventData.pointerId == -1) // -1 indica clic izquierdo (táctil en Android)
            {
                EncenderMechero();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            estaCerca = true;
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            estaCerca = false;
            canvas.enabled = false;
        }
    }

    public void EncenderMechero()
    {
        jugador.mechero = true;
        Destroy(gameObject);
    }
}