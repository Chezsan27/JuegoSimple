using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controlador : MonoBehaviour
{
    //Posiciones iniciales y checkpoint
    public Vector3 posicionInicialJugador;
    public Vector3 posicionInicialCamara;
    public Vector3 checkPoint;
    public GameObject personaje;
    public int vidas;
    // Monedas
    public int monedas;
    
    // UI monedas / Corazones
    public Text textoMonedas;
    public GameObject[] corazones;
    
    // Canvas GamOver
    public GameObject panelGameOver;
    //Guardamos en el awake los objetos que necesitamos para el siguiente nivel
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("Jugador"));
        DontDestroyOnLoad(GameObject.Find("Canvas"));
    }

    void Start()
    {
        checkPoint = posicionInicialJugador;
        personaje = GameObject.FindWithTag("Player");
        corazones = new GameObject[3];
        posicionInicialJugador = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        posicionInicialCamara = GameObject.FindWithTag("MainCamera").GetComponent<Transform>().position;
        vidas = 3;
        monedas = 0;
        textoMonedas = GameObject.Find("CuentaMonedas").GetComponent<Text>();
        panelGameOver = GameObject.Find("PanelGameOver");
        panelGameOver.SetActive(false);
        corazones[0] = GameObject.Find("Vidas").transform.GetChild(0).gameObject;
        corazones[1] = GameObject.Find("Vidas").transform.GetChild(1).gameObject;
        corazones[2] = GameObject.Find("Vidas").transform.GetChild(2).gameObject;
        GameObject.Find("CanvasMonedas").transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
    }
    
    void Update()
    {
        //Por cada vida que se pierda (Tras 3 toques) se desactivará la imagen del corazon correspondiente
        if (vidas<3)
        {
            corazones[vidas].SetActive(false);
        }
        
        if (vidas<=0)
        {
            gameObject.GetComponent<AudioSource>().Play();
            panelGameOver.SetActive(true);
            personaje.GetComponent<MoviminetoFisicas>().muerte = true;

        }
        textoMonedas.text = "" + monedas;
        
    }
    //Función para reestablecer el nivel y los valores de las vidas desde el ultimo checkpoint
    public void reiniciarNivel()
    {
        personaje.transform.position = checkPoint;
        vidas = 3;
        panelGameOver.SetActive(false);
        personaje.GetComponent<MoviminetoFisicas>().muerte = false;
        corazones[0].SetActive(true);
        corazones[1].SetActive(true);
        corazones[2].SetActive(true);
    }
}
