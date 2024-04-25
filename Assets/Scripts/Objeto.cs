using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Objeto : MonoBehaviour, IPointerClickHandler
{
    private bool estaCerca = false;
    [SerializeField] private Canvas canvas;
    private Player jugador;

    private bool recogido;
    public bool soltado;
    public bool subiendo;

    void Start()
    {
        recogido = false;
        soltado = false;
        subiendo = false;
        jugador = Player.jugador;
        canvas.enabled = false;
    }

    void Update()
    {
        // No necesitamos chequear Input.GetMouseButtonUp en Update() ya que ahora usamos IPointerClickHandler
        if (recogido && !soltado && !subiendo)
        {
            transform.position = new Vector3(jugador.transform.position.x - 1, jugador.transform.position.y, jugador.transform.position.z - 1);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (estaCerca && !soltado && !recogido)
        {
            if (eventData.pointerId == -1) // -1 indica clic izquierdo
            {
                AgarrarObjeto();
            }   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            canvas.enabled = true;
            estaCerca = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            canvas.enabled = false;
            estaCerca = false;
        }
    }

    public void AgarrarObjeto()
    {
        Player jugador = FindObjectOfType<Player>();
        if (jugador != null && jugador.objeto == null)
        {
            recogido = true;
            jugador.recogerObjeto(this);
        }
    }
}