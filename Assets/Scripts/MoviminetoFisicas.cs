using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoviminetoFisicas : MonoBehaviour
{
    private bool canJump;

    private float gravedadInicial;
    private Animator anim;
    private CircleCollider2D colliderAtaque;
    public bool muerte;
    public GameObject controlador;

    void Start()
    {
        gravedadInicial = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        anim = gameObject.GetComponent<Animator>();
        //Collider del ataque del jugador desactivado por defecto
        colliderAtaque = transform.GetChild(0).GetComponent<CircleCollider2D>();
        colliderAtaque.enabled = false;
        muerte = false;
        controlador = GameObject.Find("Controlador");
    }

    private void FixedUpdate()
    {
        if (!muerte)
        {
            if (!canJump)
            {
                MoverIzquierda(-500f);

                MoverDerecha(500f);
            }
            else
            {
                MoverIzquierda(-1000f);

                MoverDerecha(1000f);
            }
        }

        anim.SetBool("muerte", muerte);
    }

    void Update()
    {
        if (!Input.GetKey("right") && !Input.GetKey("left"))
        {
            anim.SetBool("moving", false);
        }
        //Aplicamos una correccion a la gravedad para para realizar el salto y que la animación
        //Se vea correctamente
        if (Input.GetKeyDown("up") && canJump)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300f));
            canJump = false;
        }

        if (!canJump)
        {
            anim.SetBool("salto", true);
            anim.SetBool("moving", false);
        }

        // Comprobamos si se esta reproduciendo la animacion de ataque
        AnimatorStateInfo estadoAnimator = anim.GetCurrentAnimatorStateInfo(0);
        bool ataque = estadoAnimator.IsName("ataqueEspada");
        //En caso de no estar reproducionedose la animacion podremos volver a atacar
        if (Input.GetKeyDown("space") && !ataque)
        {
            transform.GetChild(0).GetComponent<AudioSource>().Play();
            anim.SetTrigger("ataque");
        }

        //Seccion de ataque
        //Recolocamos el circle collider del ataque dependiendo donde este mirando el personaje
        if (gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            colliderAtaque.offset = new Vector2(-1.7f, 0);
        }
        else
        {
            colliderAtaque.offset = new Vector2(1, 0);
        }

        //Activamos el collider del ataque en el momento que la animacion muestre la espada
        //La animacion del ataque dura aproximadamente 1.2 segundos
        //Activaremos el collider una vez inicie la animacion y lo desactivaremos antes de que termine entre 0.5 y 1
        if (ataque)
        {
            float tiempoTranscurridoAnimacion = estadoAnimator.normalizedTime;
            //print(tiempoTranscurridoAnimacion);
            if (tiempoTranscurridoAnimacion > 0.5 && tiempoTranscurridoAnimacion < 1)
            {
                colliderAtaque.enabled = true;
            }
            else
            {
                colliderAtaque.enabled = false;
            }
        }
    }

    private void MoverIzquierda(float fuerza)
    {
        if (Input.GetKey("left"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(fuerza * Time.deltaTime, 0));
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MoverDerecha(float fuerza)
    {
        if (Input.GetKey("right"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(fuerza * Time.deltaTime, 0));
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    
    //En el momento de tocar  el suelo reestablecemos la gravedad inicial
    private void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.transform.tag == "ground")
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gravedadInicial;
            canJump = true;
            gameObject.GetComponent<Animator>().SetBool("salto", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Al momento de entrar en contacto con un respawn (MAPA) establecemos esa zona como checkpoint en controlador
        if (other.CompareTag("Respawn"))
        {
            controlador.GetComponent<Controlador>().checkPoint = other.transform.position;
        }
        //Al enmtrar en contacto con una poción reestablecemos una vida y activamos la imagen del corazon pertienete
        if (other.CompareTag("Pocion"))
        {
            if (controlador.GetComponent<Controlador>().vidas < 3)
            {
                controlador.GetComponent<Controlador>().corazones[controlador.GetComponent<Controlador>().vidas]
                    .SetActive(true);
                controlador.GetComponent<Controlador>().vidas++;
                other.GetComponent<AudioSource>().Play();
                other.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(other.gameObject, 1);
            }
        }
        
    }

   
}