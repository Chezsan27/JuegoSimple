using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuertaControlador : MonoBehaviour
{
    //Se necesitaran 10 monedas para poder pasar al siguiente mapa
    //Una vez conseguidas se activara la animacion de abrir la puerta
    private GameObject controlador;
    void Start()
    {
        controlador = GameObject.Find("Controlador");
    }
    void Update()
    {
        if (controlador.GetComponent<Controlador>().monedas>=10)
        {
            gameObject.GetComponent<Animator>().SetTrigger("abrir");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (controlador.GetComponent<Controlador>().monedas >= 10)
            {
                GameObject.Find("CanvasMonedas").transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
                controlador.GetComponent<Controlador>().personaje.transform.position = new Vector3(70, -70, 0);
                SceneManager.LoadScene("Nivel-2");
            }
            else
            {
                GameObject.Find("CanvasMonedas").transform.GetChild(0).gameObject.GetComponent<Text>().enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("CanvasMonedas").transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
            
        }
    }
}
