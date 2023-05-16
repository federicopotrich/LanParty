using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squalifica : MonoBehaviour
{
    
    void Start()
    {
        bool foo;
        if(PlayerPrefs.GetInt("foo") == 1){
            foo = true;
        }else{
            foo = false;
        }

        this.gameObject.SetActive(!foo);
    }
}
