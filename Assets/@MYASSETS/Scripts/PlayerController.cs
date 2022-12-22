using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravedad;
    public float velocidadSalto; //Velocidad en y que adquirir� el objeto cuando se realize elm salto.
    public ParticleSystem deathParticles; //Sistema de particulas


    private GameManager gameManager;

    private bool tocaSuelo = true; //Se inicializa como true porque est� tocando el suelo al inicio.
    private bool hasJumped = false; //Se inicializa como false porque por defecto el (espacio / click derecho / t�ctil m�vil) no est� presionado.
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        Physics.gravity = new Vector3(0, gravedad, 0); //Setea la f�sica de la gravedad del juego
        rb = GetComponent<Rigidbody>(); //Obtiene el componente RigidBody del GameObject.
        animator = GetComponent<Animator>(); //Obtiene el componente Animator de este GameObject.
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //Obtiene el GameManager de la escena.
        
    }

    //Parte l�gica
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) //Si se ha presionado el espacio / click derecho / t�ctil m�vil setea el bool como true.
        {
            animator.SetBool("hasJumped", true);
            hasJumped = GetComponent<Animator>().GetBool("hasJumped");
        }
    }
    
    //Parte f�sica
    private void FixedUpdate()
    {
        if (tocaSuelo && hasJumped) //Si est� tocando el suelo y ha presionado el bot�n pertinente (espacio / click derecho / t�ctil m�vil) se realiza el salto.
        {
            rb.velocity = new Vector3(0, velocidadSalto, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Suelo")) //Si est� tocando el suelo el boolean tocaSuelo ser� true.
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
     * Este m�todo se encarga de instanciar las particulas del jugador de llamar al metodo gameOver del game manager y de destruir el objeto actual.
     */
    private void playerDeath()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        gameManager.gameOver();
        Destroy(gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag.Equals("Suelo")) //Si no est� tocando el suelo el boolean tocaSuelo ser� false.
        {
            animator.SetBool("touchingFloor", false);
            tocaSuelo = GetComponent<Animator>().GetBool("touchingFloor");
        }
    }
}
