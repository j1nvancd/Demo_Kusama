using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour
{
    [SerializeField] GameObject parteMovible;
    [SerializeField] Canvas canvas;
    private bool activado;
    private bool estaCerca;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        activado = false;
        estaCerca = false;
        canvas.enabled = false;
        animator = parteMovible.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0) && estaCerca && !activado)
        {
            animator.Play("AnimacionCuchilla");
            activado = true;
            canvas.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador") && !activado)
        {
            canvas.enabled=true;
            estaCerca=true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            canvas.enabled = false;
            estaCerca=false;
        }
    }
}
