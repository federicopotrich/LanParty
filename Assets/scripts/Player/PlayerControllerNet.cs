using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerControllerNet : NetworkBehaviour
{
    public float speed;
    public override void OnNetworkSpawn() { // This is basically a Start method
        transform.Find("MainCameraPlayer").gameObject.SetActive(IsOwner);
        base.OnNetworkSpawn(); // Not sure if this is needed though, but good to have it.

        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner){
            return;
        }
        float x = 0;
        float y = 0;
        if(Input.GetKey(KeyCode.A) != Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) != Input.GetKey(KeyCode.RightArrow)){
            x = Input.GetAxis("Horizontal");
        }
        if(Input.GetKey(KeyCode.W) != Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) != Input.GetKey(KeyCode.DownArrow)){
            y = Input.GetAxis("Vertical");
        }

        this.gameObject.transform.Translate(new Vector3(x*Time.deltaTime*speed, y*Time.deltaTime*speed, 0));

    }
}
