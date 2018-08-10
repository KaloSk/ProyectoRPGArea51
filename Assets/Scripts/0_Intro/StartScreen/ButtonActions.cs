using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public int indice;

    public void OnPlay()
    {

        SceneManager.LoadScene(indice);
    }

}
