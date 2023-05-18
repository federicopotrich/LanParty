using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
    bool on, timetook=false;
    public ParticleSystem burst;
    int timer;
    public bool hit=false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hit=false;
        if(!on)
            timetook=false;
    }

    public void TurnOnOff(bool onoff){
        on = onoff;
        SetTimer();
    }

    
    void OnTriggerStay2D(Collider2D coll){
        int tmpTime = ((int)Time.time);
        if(on==true && coll.gameObject.tag=="Note"&&timer>=tmpTime){
            Destroy(coll.gameObject);
            burst.Play();
            hit=true;
        }
    }

    void SetTimer(){
        if(!timetook){
            timer = ((int)Time.time) ;
            timetook=true;
        }
    }

}
