 using UnityEngine;

public class EndlessScroll : MonoBehaviour
{
    public float factorScroll = -1; //Direccion en la que se va a efectuar el movimiento.
    public float velocidadJuego; //Velocidad que va a adiquirir el Objeto en el inicio.


    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3((velocidadJuego * factorScroll), 0, 0); //Establece un movimiento en el Rigidbody del objeto.
    }

    void OnTriggerExit(Collider gameArea)
    {
        Destroy(this.gameObject); //Destruye el objeto cuando este fuera del GameArea.
    }
}
