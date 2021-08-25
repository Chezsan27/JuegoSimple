using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoPlataforma : MonoBehaviour
{
    // Las plataformas se moveran mediante el Rigidbody2D asociado
    // Con una determinada fuerza dependiendo si son moviles en su eje X o Y
    public float velocidad;
    
    private Rigidbody2D rb;
    void Start()
    {
        velocidad = 20f;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.name.Equals("Plataforma movil Y"))
        {
            rb.velocity = new Vector2(0, velocidad+15f);

        }
        else
        {
            rb.velocity = new Vector2(velocidad, 0);
        }
    }

    //En el momento de colisionar con uno de los limites establecidos aplicamos un valor negativo a la velocidad
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("limitePlataforma"))
        {
            velocidad = velocidad * -1;
        }
    }
}
