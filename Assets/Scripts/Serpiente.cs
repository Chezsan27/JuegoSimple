using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serpiente : MonoBehaviour
{
    //La serpiente podrá atacar cada 3 segundos será un enemigo estatico(No se moverá del sitio)
    //Tendra un rango de ataque cuando el personaje entre en rango realizara la animacion
    private Animator ataque;
    public float cooldown;
    private float rango;
    public GameObject personaje;
    void Start()
    {
        ataque = gameObject.GetComponent<Animator>();
        cooldown = 0;
        personaje = GameObject.FindWithTag("Player");
        rango = 5;
    }

    void Update()
    {
        //calculamos el tiempo transcurrido 
        //Una vez llegado a la marca de 3segundos y estar a rango podra realizar el ataque
        //Será una animacion que extenderá el collider de la serpiente 
        cooldown += Time.deltaTime;
        float distancia = Vector2.Distance(personaje.transform.position, gameObject.transform.position);
        if (distancia<rango && cooldown>3)
        {
            gameObject.GetComponent<AudioSource>().Play();
            ataque.SetTrigger("ataque");
            cooldown = 0;
        }
        //Calculamos la posicion del personaje para realizar un cambio en su scale y asi girar a la sepiente
        if (personaje.transform.position.x > gameObject.transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.name);
        if (other.gameObject.CompareTag("Ataque"))
        {
            gameObject.GetComponent<Animator>().SetBool("muerte", true);
            gameObject.GetComponent<BoxCollider2D>().tag = "ground";
            Invoke("Muerte", 1f);
        };
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }
}