using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script vinculado al GameObject del jugador - PJ
 */
public class Player : MonoBehaviour
{
    // Variable para almacenar el valor de la velocidad de movimiento que se aplicará al personaje
    [SerializeField] private float velocidadMovimiento = 2f;
    // Variable interna dedicada al input introducido por el jugador para el eje horizontal
    private float horizontalInput;
    // Variable interna dedicada al input introducido por el jugador para el eje vertical
    private float verticalInput;
    // Elemento Raycast
    Ray ray;
    // Objeto estatico del jugador para el resto de scripts
    public static Player jugador;
    // Variable para almacenar el objeto que lleva
    public Objeto objeto;

    private void Awake()
    {
        // Asignacion de la instancia del jugador mediante patron Singleton
        if (jugador == null)
        {
            jugador = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Creacion del Raycast
        ray = new Ray(transform.position, -transform.up);
    }

    // Update is called once per frame
    void Update()
    {
        movimiento();
        raycast(horizontalInput, verticalInput);
    }

    /*
     * Metodo de apoyo para el movimiento del objeto
     */
    private void movimiento() 
    {
        // Variables de apoyo para almacenar que direccion se bloqueara (Visto desde arriba)
        // Movimiento Horizontal
        bool horPos = true;
        bool horNeg = true;
        // Movimiento Vertical
        bool verPos = true;
        bool verNeg = true;


        // Recogida de los inputs del jugador
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Se comprueba si el Raycast esta colisionando con algo(Si colisiona con algo es con el suelo)
        if (Physics.Raycast(ray.origin, ray.direction, 1))
        {
            // Desplazamiento del objeto en funcion de los inputs, la velocidad y el tiempo
            transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * velocidadMovimiento * Time.deltaTime);
            // En caso de seguir detectando colision, se puede desplazar en cualquier direccion
            horPos = true;
            horNeg = true;
            verPos = true;
            verNeg = true;
        }
        else
        {
            // En funcion del valor de la direccion, se bloquea hacia un lado u otro.
            // Se comprueban ambos para bloquear, en caso necesario, la diagonal

            // Comprobacion del movimiento horizontal
            if (horizontalInput > 0)
            {
                horPos = false;
                
            } else if (horizontalInput < 0)
            {
                horNeg = false;
            }

            // Comprobacion del movimiento vertical
            if (verticalInput > 0)
            {
                verPos = false;

            } else if (verticalInput < 0)
            {
                verNeg = false;
            }

            // Se aplican las restricciones necesarias en funcion de la direccion bloqueada
            float movimientoHorizontal = !horPos ? Mathf.Clamp(horizontalInput, -1, 0) : !horNeg ? Mathf.Clamp(horizontalInput, 0, 1) : 0;
            float movimientoVertical = !verPos ? Mathf.Clamp(verticalInput, -1, 0) : !verNeg ? Mathf.Clamp(verticalInput, 0, 1) : 0;
            // Desplazamiento aplicando las distintas restricciones
            transform.Translate(new Vector3(movimientoHorizontal, 0, movimientoVertical) * velocidadMovimiento * Time.deltaTime);

        }
        // Herramienta para el desarrollo para mostrar el raycast
        Debug.DrawRay(ray.origin, ray.direction);

    }

    /*
     * Metodo de apoyo para la creacion y actualizacion del raycast
     */
    private void raycast(float posX, float posZ)
    {
        // Modificacion de los valores
        posX = posX > 0 ? 0.1f : posX < 0 ? -0.1f : 0;
        posZ = posZ > 0 ? 0.1f : posZ < 0 ? -0.1f : 0;
        // Actualizacion del raycast
        ray = new Ray(new Vector3(transform.position.x + posX, transform.position.y, transform.position.z + posZ), -transform.up);
    }

    /*
     * Metodo para recoger el objeto deseado
     * 
     * Parametros: Object
     */
    public void recogerObjeto(Objeto objeto)
    {
        // En caso de ser nulo(no tiene objeto vinculado/recogido) se asigna el objeto
        if (this.objeto == null)
        {
            this.objeto = objeto;
        }
    }

    /*
     * Metodo para soltar el objeto
     */
    public void soltarObjeto()
    {
        // En caso de tener un objeto recogido, se suelta el mismo
        if (objeto != null)
        {
            objeto.soltado = true;
            objeto.transform.SetParent(null);
            objeto = null;
        }
    }
}
