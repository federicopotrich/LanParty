using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailTriggerScript : MonoBehaviour
{
    public bool Touch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Touch=false;
    }

    
    void OnTriggerStay2D(Collider2D coll){  
        if(coll.tag=="Note"){
            Touch=true;
            Destroy(coll.gameObject);
        }
    }
}
