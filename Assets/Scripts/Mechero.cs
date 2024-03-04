using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechero : MonoBehaviour
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

    private void Update()
    {
        if (estaCerca && Input.GetMouseButtonUp(0))
        {
            jugador.mechero = true;
            Destroy(gameObject);
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
            estaCerca=false;
            canvas.enabled = false;
        }
    }
}
