using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public static int ultimaEscena;
    public int indice;

    public void OnPlay()
    {
        ultimaEscena = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indice);
    }

    public void ReturnToLast ()
    {
        SceneManager.LoadScene(ultimaEscena);
    }

}
