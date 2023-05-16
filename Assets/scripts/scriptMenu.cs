using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptMenu : MonoBehaviour
{
    

    public void quitGame (){
        //chiudo l'applicazione
        Application.Quit();

        //metto come debug "QUIT!" visto che unity in questo caso è stupido
        Debug.Log("QUIT!");
    }
}