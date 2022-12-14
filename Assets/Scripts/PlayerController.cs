using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 gravedad;
    public Vector3 velocidadSalto;
    bool tocaSuelo = false;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = gravedad;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && tocaSuelo == true) 
        {
            rb.velocity = velocidadSalto;
            tocaSuelo=false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.CompareTo("Suelo") == 0)
        { 
            tocaSuelo = true;
        }
    }
}
