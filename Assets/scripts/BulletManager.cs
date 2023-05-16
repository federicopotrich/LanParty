using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(transform.up*0.2f);
        Destroy(this.gameObject, 5);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(this.gameObject);
    }
}
