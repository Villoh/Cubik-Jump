using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessScroll : MonoBehaviour
{
    public float factorScroll = -1;
    public float velocidadJuego;


    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3((velocidadJuego * factorScroll), 0, 0);
    }

    void OnTriggerExit(Collider gameArea)
    {
        Destroy(this.gameObject);
    }
}
