using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour
{

    public void QuitG()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Application.QuitG ();
#endif
    }

}