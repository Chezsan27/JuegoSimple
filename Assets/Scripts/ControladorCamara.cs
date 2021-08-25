using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorCamara : MonoBehaviour
{
    public Transform posicionCamara;

    public Transform posicionJugador;
    void Start()
    {
        posicionCamara = gameObject.transform;
        posicionJugador = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        //Si la escena donde se encuentra es el Nivel-2 establecerá un limite en el eje Y evitando que la camara
        //Se salga del mapa en caso contrario seguira en todos los ejes sin limite al jugador
        if (SceneManager.GetActiveScene().name.Equals("Nivel-2"))
        {
            if (posicionJugador.position.x>=0 && posicionJugador.position.y>-66)
            {
            
                Vector3 position = posicionCamara.position;
                position = new Vector3(posicionJugador.position.x, posicionJugador.position.y, position.z);
                posicionCamara.position = position;
           
            }
            else
            {
                Vector3 position = posicionCamara.position;
                position = new Vector3(posicionJugador.position.x, position.y , position.z);
                posicionCamara.position = position;
            }
        }
        else
        {
            if (posicionJugador.position.x>=0)
            {
            
                Vector3 position = posicionCamara.position;
                position = new Vector3(posicionJugador.position.x, posicionJugador.position.y, position.z);
                posicionCamara.position = position;
           
            }
        }
      
       
    }
}
