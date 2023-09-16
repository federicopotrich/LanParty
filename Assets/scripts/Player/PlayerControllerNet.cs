using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControllerNet : MonoBehaviour
{
    public float speed;
    void Start() {
        speed = 7;
    }

    // Update is called once per frame
    void Update()
    {
        GBGUnity.Utility.MovementObject.MoveTopDown(this.gameObject.GetComponent<Rigidbody2D>(), speed);
    }
}
