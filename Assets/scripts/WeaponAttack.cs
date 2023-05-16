using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag == "enemy")
            if(Input.GetKeyUp(KeyCode.Space)){
                Debug.Log("Enter "+gm.weaponSelected.dmg);
                //Debug.Log("attaccato");
                //StartCoroutine("attack", gm.weaponSelected);
            }
    }
    void OnTriggerStay2D(Collider2D coll){
        if(coll.gameObject.tag == "enemy")
            if(Input.GetKey(KeyCode.Space)){
                Debug.Log("Stay "+gm.weaponSelected.dmg);
                //StartCoroutine("attack", gm.weaponSelected);
            }
    }
}
