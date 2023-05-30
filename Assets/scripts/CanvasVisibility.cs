using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Unity.Netcode;
public class CanvasVisibility : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 3f;
    private Canvas canvas;
    void Start()
    {
     
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if(distance <= activationDistance){
            canvas.enabled = true;
        }else{
            canvas.enabled = false;
        }
    }
}
