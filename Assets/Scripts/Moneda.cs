using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public GameObject controlador;

    void Start()
    {
        controlador = GameObject.Find("Controlador");
    }

    // Cuando haga trigger sumamos 1 al contador de monedas y reproducimos el sonido
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            controlador.GetComponent<Controlador>().monedas += 1;
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject,0.5f);
        }
    }
}
