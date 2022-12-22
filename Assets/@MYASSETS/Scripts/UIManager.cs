using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
    private float timer  = 0;
    private int counter = 0;
    private void Start()
    {
        this.record.text = "" + PlayerPrefs.GetInt("counter");
    }
    private void Update()
    { 

        timer += Time.deltaTime;

        if (timer >= 0.01)
        {
            timer = 0f;
            counter++;
        }
        if (puntuacion.isActiveAndEnabled == true)
        {
            puntuacion.text = "" + counter;
        }
    }

    public void textosJugando()
    {
        this.jugarBtn.transform.gameObject.SetActive(false);
        this.salirBtn.transform.gameObject.SetActive(false);
        this.record.transform.gameObject.SetActive(false);
        this.recordTexto.transform.gameObject.SetActive(false);
        this.titulo.transform.gameObject.SetActive(false);
        this.puntuacion.transform.gameObject.SetActive(true);
        this.puntuacionTexto.transform.gameObject.SetActive(true);
        counter = 0;
    }

    public void textosParado()
    {
        this.record.text = "" + PlayerPrefs.GetInt("counter");
        this.record.transform.gameObject.SetActive(true);
        this.recordTexto.transform.gameObject.SetActive(true);
        this.jugarBtn.transform.gameObject.SetActive(true);
        this.salirBtn.transform.gameObject.SetActive(true);
        this.titulo.transform.gameObject.SetActive(true);
        this.puntuacion.transform.gameObject.SetActive(false);
        this.puntuacionTexto.transform.gameObject.SetActive(false);
        
        counter = 0;
    }

    public int getScore()
    {
        return this.counter;
    }
}
