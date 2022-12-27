using System;
using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text puntuacion;
    public TMP_Text puntuacionTexto;
    public TMP_Text titulo;
    public TMP_Text recordTexto;
    public TMP_Text record;
    public Button jugarBtn;
    public Button salirBtn;

    private double timer = 0;
    private int score = 0;

    private void Start()
    {
        this.record.text = "" + PlayerPrefs.GetInt("counter"); 
    }

    private void Update()
    {
        actualizaTexto();
    }

    /**
     * public void textosComienzaJuego()
     * Metodo que controla que textos tienen que estar activos cuando el juego inicia (cuando se presiona el boton jugar).
     */
    public void textosComienzaJuego()
    {
        this.jugarBtn.transform.gameObject.SetActive(false);
        this.salirBtn.transform.gameObject.SetActive(false);
        this.record.transform.gameObject.SetActive(false);
        this.recordTexto.transform.gameObject.SetActive(false);
        this.titulo.transform.gameObject.SetActive(false);
        this.puntuacion.transform.gameObject.SetActive(true);
        this.puntuacionTexto.transform.gameObject.SetActive(true);
        score = 0;
    }

    /**
     * public void textosJuegoPausado()
     * Metodo que controla que textos tienen que estar activos cuando el juego esta pausado (en el inicio del programa o cuando el jugador pierde).
     */
    public void textosJuegoPausado()
    { 
        this.record.text = "" + PlayerPrefs.GetInt("counter");
        this.record.transform.gameObject.SetActive(true);
        this.recordTexto.transform.gameObject.SetActive(true);
        this.jugarBtn.transform.gameObject.SetActive(true);
        this.salirBtn.transform.gameObject.SetActive(true);
        this.titulo.transform.gameObject.SetActive(true);
        this.puntuacion.transform.gameObject.SetActive(false);
        this.puntuacionTexto.transform.gameObject.SetActive(false);
        score = 0;
    }

    /**
     * public int getScore()
     * Obtiene el valor del entero que calcula los puntos.
     */
    public int getScore()
    {
        return this.score;
    }

    /**
     * public void actualizaTexto()
     * Actualiza la puntuación del TMP de la escena mediante la variable score de esta clase. Este metodo tiene que ser llamado en el Update por lo que se reproduciria cada frame.
     * Cuando entra en el método actualiza el valor de la variable mediante el deltaTime que es el valor (como float) desde el anterior frame al actual, 
     * y luego mediante una condición que determina que el timer es mayor o igual que un valor (en mi caso una centesima) se resetea el timer porque ha pasado una décima y aumenta en uno el score.
     * Basicamente cada décima sumaría 1 punto, lo que es igual a 100 puntos por segundo.
     */
    private void actualizaTexto()
    {
        if (puntuacion.isActiveAndEnabled)
        {        
            timer += Time.deltaTime;
            if (timer >= 0.09)
            {
                score++;
                puntuacion.SetText(score.ToString());
                timer = 0;
            }
        }
    }
}
