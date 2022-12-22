using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravedad;
    public float velocidadSalto; //Velocidad en y que adquirirá el objeto cuando se realize elm salto.
    public ParticleSystem deathParticles; //Sistema de particulas


    private GameManager gameManager;

    private bool tocaSuelo = true; //Se inicializa como true porque está tocando el suelo al inicio.
    private bool hasJumped = false; //Se inicializa como false porque por defecto el (espacio / click derecho / táctil móvil) no está presionado.
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        Physics.gravity = new Vector3(0, gravedad, 0); //Setea la física de la gravedad del juego
        rb = GetComponent<Rigidbody>(); //Obtiene el componente RigidBody del GameObject.
        animator = GetComponent<Animator>(); //Obtiene el componente Animator de este GameObject.
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //Obtiene el GameManager de la escena.
        
    }

    //Parte lógica
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) //Si se ha presionado el espacio / click derecho / táctil móvil setea el bool como true.
        {
            animator.SetBool("hasJumped", true);
            hasJumped = GetComponent<Animator>().GetBool("hasJumped");
        }
    }
    
    //Parte física
    private void FixedUpdate()
    {
        if (tocaSuelo && hasJumped) //Si está tocando el suelo y ha presionado el botón pertinente (espacio / click derecho / táctil móvil) se realiza el salto.
        {
            rb.velocity = new Vector3(0, velocidadSalto, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Suelo")) //Si está tocando el suelo el boolean tocaSuelo será true.
        {
            animator.SetBool("touchingFloor", true);
            tocaSuelo = GetComponent<Animator>().GetBool("touchingFloor");
            animator.SetBool("hasJumped", false); //Si ha tocado el suelo se entiende que no puede estar saltando.
            hasJumped = GetComponent<Animator>().GetBool("hasJumped");
        }
        else if (collision.transform.tag.CompareTo("Obstaculo") == 0) //Si toca un obstaculo se llama al metodo playerDeath() que se encarga de gestionar todo lo relacionado con el GameOver.
        {
            playerDeath();
        }
    }

    /**
     * private void playerDeath()
     * Este método se encarga de instanciar las particulas del jugador de llamar al metodo gameOver del game manager y de destruir el objeto actual.
     */
    private void playerDeath()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        gameManager.gameOver();
        Destroy(gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag.Equals("Suelo")) //Si no está tocando el suelo el boolean tocaSuelo será false.
        {
            animator.SetBool("touchingFloor", false);
            tocaSuelo = GetComponent<Animator>().GetBool("touchingFloor");
        }
    }
}
