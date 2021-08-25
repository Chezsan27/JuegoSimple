using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escena2Controlador : MonoBehaviour
{
    private GameObject panelVictoria;
    void Start()
    {
        panelVictoria = GameObject.Find("CanvasVictoria");
        panelVictoria.SetActive(false);
    }
    //Comprobará si existe el objeto "Boss", en caso de no encontrarlo el juego habrá finalizado
    //Mostrando el panel de victoria y reproduciendo el sonido
    void Update()
    {
        if (GameObject.Find("Boss")==false)
        {
            Invoke("victoria", 0.2f);
        }
    }

    public void salir()
    {
        Application.Quit();
    }

    private void victoria()
    {
        panelVictoria.SetActive(true);
    }
}
