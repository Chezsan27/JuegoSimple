using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    //El enemigo seguirá al personaje por el mapa si esta en su campo de vision hasta estar a rango
    //entonces realizara la animacion de ataque que extenderá su collider para alcanzar al jugador
    //Podrá atacar cada 3 segundos
    //Si es un enemigo común tendra 2 vidas y el Boss final tendra 4
    private Animator animator;
    public float cooldown;
    private float rango;
    public GameObject personaje;
    private float vision;
    private Vector3 objetivo;
    private int vidas;
    private int vidasBoss;
    private bool atacado;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        cooldown = 0;
        personaje = GameObject.FindWithTag("Player");
        rango = 4;
        vision = 16;
        vidas = 2;
        vidasBoss = 4;

    }

    void Update()
    {
        //Obtenemos la posicion del personaje pero solo en la posicion X ya que no queremos que el enemigo flote si el 
        //personaje salta por encima
        objetivo = new Vector3(personaje.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        cooldown += Time.deltaTime;
        float distancia = Vector2.Distance(personaje.transform.position, gameObject.transform.position);
        
        //Comprobamos donde está el personaje para cambiar la escala del enemigo y asi que esté en
        //todo momento mirando al personaje 
        if (personaje.transform.position.x > gameObject.transform.position.x)
        {
            if (gameObject.name.Equals("Boss"))
            {
                gameObject.transform.localScale = new Vector3(-4, 4, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (gameObject.name.Equals("Boss"))
            {
                gameObject.transform.localScale = new Vector3(4, 4, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }

        }
        //Si distancia es menos que su vision y no está a rango de ataque le seguira alterando su posicion
        // solo podrá moverse si no ha sido atacado dondo tiempo a la animacion de golpe a reproducirse
        if (distancia<vision && distancia>rango && !atacado)
        {
            animator.SetBool("andar", true);

            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, objetivo, 0.03f);
        }
        else
        {
            animator.SetBool("andar", false);
           
        }
        if (distancia<rango && cooldown>3 && !atacado)
        {
            animator.SetTrigger("ataque");
            cooldown = 0;
        }
       
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Comprueba si ha sido golpeado por el collider de la espada del personaje reproduce el sonido la animación
        //Y resta vidas al enemigo
        if (other.gameObject.CompareTag("Ataque"))
        {
            atacado = true;
            gameObject.GetComponent<AudioSource>().Play();
            animator.SetBool("andar", false);
            animator.SetTrigger("golpe");
            if (gameObject.name.Equals("Boss"))
            {
                vidasBoss--;
            }
            else
            {
                vidas--;
            }
            Invoke("recibeGolpe", 1f);
            //En el momento de morir cambiamos su tag para evitar que nos siga golpeando su collider despues lo destruiomos
            if (vidas<=0 || vidasBoss<=0)
            {
                gameObject.GetComponent<BoxCollider2D>().tag = "ground";

                Invoke("Muerte", 1f);
            }

            if (vidasBoss<=0)
            {
                GameObject.Find("Camara").GetComponent<AudioSource>().Play();
            }
        }
        
    }

    private void recibeGolpe()
    {
        atacado = false;
    }
    private void Muerte()
    {
        Destroy(gameObject);
    }
}
