using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessScroll : MonoBehaviour
{
    public float factorScroll = -1;
    public Vector3 velocidadJuego;


    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = velocidadJuego * factorScroll;
    }

    void OnTriggerExit(Collider gameArea)
    {
        transform.position += Vector3.right * (gameArea.bounds.size.x + GetComponent<BoxCollider>().size.x);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "Fondo") 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
