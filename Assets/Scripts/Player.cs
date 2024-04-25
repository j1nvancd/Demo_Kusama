
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
    private Ray ray;
    // Objeto estático del jugador para el resto de scripts
    public static Player jugador;
    // Variable para almacenar el objeto que lleva
    public Objeto objeto;
    // Variable para almacenar el estado del mechero. True = mechero recogido
    public bool mechero = false;
     // Variable para almacenar la posición inicial del toque
    private Vector3 direccion = Vector3.zero;

    private bool estadoMovimiento = false;
    private bool movimientoArriba = false;
    private bool movimientoAbajo = false;
    private bool movimientoDerecha = false;
    private bool movimientoIzquierda = false;

    private void Awake()
    {
        // Asignación de la instancia del jugador mediante patrón Singleton
        if (jugador == null)
        {
            jugador = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Creación del Raycast
        ray = new Ray(transform.position, -transform.up);
    }

    // Update is called once per frame
    void Update()
    {
        /*horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");*/
        Vector3 direccion = new Vector3(horizontalInput, 0,  verticalInput);
        //movimientoPantalla(direccion);
        //raycast(horizontalInput, verticalInput);
        //movimiento();
        //raycast(horizontalInput, verticalInput);*/
       
            movimientoPantalla();
        
    }

    
    
public void CancelarMovimiento()
{
    direccion = Vector3.zero;
}
private void movimientoPantalla()
{
       if (movimientoArriba)
        {
            transform.Translate(transform.forward * velocidadMovimiento/2 * Time.deltaTime);
        }

        if (movimientoAbajo)
        {
            transform.Translate(-transform.forward * velocidadMovimiento / 2 * Time.deltaTime);
        }

        if (movimientoIzquierda)
        {
            transform.Translate(-transform.right * velocidadMovimiento / 2 * Time.deltaTime);
        }

        if (movimientoDerecha)
        {
            transform.Translate(transform.right * velocidadMovimiento / 2 * Time.deltaTime);
        }





    }

    public void CambiarEstado()
    {
        estadoMovimiento = !estadoMovimiento;
        Debug.Log("Cambio de estado a " + estadoMovimiento);
    }
public void MoverArriba()
{
    movimientoArriba = true;
}

    public void CancelarArriba()
    {
        movimientoArriba = false;
    }

public void MoverAbajo()
{
        movimientoAbajo = true;
}

    public void CancelarAbajo()
    {
        movimientoAbajo = false;
    }

    public void MoverIzquierda()
{
   movimientoIzquierda = true;
}

    public void CancelarIzquierda()
    {
        movimientoIzquierda = false;
    }

    public void MoverDerecha()
{
    movimientoDerecha = true;
}

    public void CancelarDerecha()
    {
        movimientoDerecha = false;
    }

    /*
     * Método de apoyo para el movimiento del objeto
     */
    private void movimiento() 
    {
        // Variables de apoyo para almacenar qué dirección se bloqueará (visto desde arriba)
        // Movimiento Horizontal
        bool horPos = true;
        bool horNeg = true;
        // Movimiento Vertical
        bool verPos = true;
        bool verNeg = true;

        // Recogida de los inputs del jugador
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Se comprueba si el Raycast está colisionando con algo (Si colisiona con algo es con el suelo)
        if (Physics.Raycast(ray.origin, ray.direction, 1))
        {
            // Desplazamiento del objeto en función de los inputs, la velocidad y el tiempo
            transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * velocidadMovimiento * Time.deltaTime);
            // En caso de seguir detectando colisión, se puede desplazar en cualquier dirección
            horPos = true;
            horNeg = true;
            verPos = true;
            verNeg = true;
        }
        else
        {
            // En función del valor de la dirección, se bloquea hacia un lado u otro.
            // Se comprueban ambos para bloquear, en caso necesario, la diagonal

            // Comprobación del movimiento horizontal
            if (horizontalInput > 0)
            {
                horPos = false;
            } 
            else if (horizontalInput < 0)
            {
                horNeg = false;
            }

            // Comprobación del movimiento vertical
            if (verticalInput > 0)
            {
                verPos = false;
            } 
            else if (verticalInput < 0)
            {
                verNeg = false;
            }

            // Se aplican las restricciones necesarias en función de la dirección bloqueada
            float movimientoHorizontal = !horPos ? Mathf.Clamp(horizontalInput, -1, 0) : !horNeg ? Mathf.Clamp(horizontalInput, 0, 1) : 0;
            float movimientoVertical = !verPos ? Mathf.Clamp(verticalInput, -1, 0) : !verNeg ? Mathf.Clamp(verticalInput, 0, 1) : 0;
            // Desplazamiento aplicando las distintas restricciones
            transform.Translate(new Vector3(movimientoHorizontal, 0, movimientoVertical) * velocidadMovimiento * Time.deltaTime);
        }

        // Herramienta para el desarrollo para mostrar el raycast
        Debug.DrawRay(ray.origin, ray.direction);
    }

    /*
     * Método de apoyo para la creación y actualización del raycast
     */
    private void raycast(float posX, float posZ)
    {
        // Modificación de los valores
        posX = posX > 0 ? 0.1f : posX < 0 ? -0.1f : 0;
        posZ = posZ > 0 ? 0.1f : posZ < 0 ? -0.1f : 0;
        // Actualización del raycast
        ray = new Ray(new Vector3(transform.position.x + posX, transform.position.y, transform.position.z + posZ), -transform.up);
    }

    /*
     * Método para recoger el objeto deseado
     * 
     * Parámetros: Objeto
     */
    public void recogerObjeto(Objeto objeto)
    {
        // En caso de ser nulo (no tiene objeto vinculado/recogido) se asigna el objeto
        if (this.objeto == null)
        {
            this.objeto = objeto;
        }
    }

    /*
     * Método para soltar el objeto
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