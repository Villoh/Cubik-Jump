using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float gravedad;
    public float velocidadSalto;
    public ParticleSystem deathParticles;
    private GameManager gameManager;

    bool tocaSuelo = true; //Se inicializa como true porque est� tocando el suelo al inicio.
    bool spacePressed = false; //Se inicializa como false porque por defecto el espacio no est� presionado.
    Rigidbody rb;
    Animator animator;
    void Start()
    {
        Physics.gravity = new Vector3(0, gravedad, 0); //Setea la f�sica de la gravedad del juego
        rb = GetComponent<Rigidbody>(); //Obtiene el componente RigidBody del GameObject.
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    //Parte l�gica
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            animator.SetBool("hasJumped", true);
            spacePressed = GetComponent<Animator>().GetBool("hasJumped");
        }
    }
    
    //Parte f�sica
    private void FixedUpdate()
    {
        if (tocaSuelo && spacePressed)
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
            animator.SetBool("hasJumped", false);
            spacePressed = GetComponent<Animator>().GetBool("hasJumped");
        }
        else if (collision.transform.tag.CompareTo("Obstaculo") == 0) //Si toca un obstaculo se mostrar� el menu de GameOver.
        {
            //collision.transform.GetComponent<Rigidbody>().AddForce(new Vector3(-100, 0, 0));
            playerDeath();
        }
    }

    private void playerDeath()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        gameManager.gameOver();
        Destroy(gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag.Equals("Suelo")) //Si no est� tocando el suelo el boolean tocaSuelo ser� true.
        {
            animator.SetBool("touchingFloor", false);
            tocaSuelo = GetComponent<Animator>().GetBool("touchingFloor");
        }
    }
}
