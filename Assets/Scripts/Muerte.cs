using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muerte : MonoBehaviour
{
    public GameObject personaje;
    public GameObject controlador;
    public GameObject camara;
    public int toques;
    void Start()
    {
        personaje = GameObject.FindWithTag("Player");
        controlador = GameObject.FindWithTag("Controlador");
        camara = GameObject.FindWithTag("MainCamera");
        toques = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.name == "Muerte")
        {
            toques = 3;
        }
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Enemigo"))
        {
            toques++;
            print(toques);
            personaje.GetComponent<Animator>().SetTrigger("golpe");
            //Comprobamos la orientacion del personaje en el momento de recibir el golpe para aplicar una fuerza 
            //En una direccion u en otra
            if (GetFlipXPersonaje())
            {
                personaje.GetComponent<Rigidbody2D>().AddForce(new Vector2(150 , 200 ));
            }
            else
            {
                personaje.GetComponent<Rigidbody2D>().AddForce(new Vector2(-150 , 200 ));

            }
                  


        }

        if (toques==3 && other.gameObject.CompareTag("Player"))
        {
            controlador.GetComponent<AudioSource>().Play();
            controlador.GetComponent<Controlador>().vidas -= 1;
            personaje.GetComponent<MoviminetoFisicas>().muerte = true;
            //Invocamos la funcion una vez haya terminado la animacion de "desaparece"
            Invoke("ReseteaPosicion",1.189f);
        }
    }

    public void ReseteaPosicion()
    {
        personaje.GetComponent<MoviminetoFisicas>().muerte = false;
        toques = 0;
        personaje.transform.position = controlador.GetComponent<Controlador>().checkPoint;
        camara.transform.position = controlador.GetComponent<Controlador>().posicionInicialCamara;
    }

    private Boolean GetFlipXPersonaje()
    {
        return personaje.GetComponent<SpriteRenderer>().flipX;
    }
}
