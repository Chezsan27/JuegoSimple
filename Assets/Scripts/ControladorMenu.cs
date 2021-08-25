using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenu : MonoBehaviour
{
    public void jugar()
    {
        SceneManager.LoadScene("Nivel-1");
    }

}
