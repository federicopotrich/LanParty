using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public int dmg;
    public bool active;
    public bool spin;

    // Start is called before the first frame update
    void Start()
    {
        int spinrot = 0;
        if(spin)
            this.transform.Rotate(0,0,spinrot, Space.World);
        active=false;
        StartCoroutine(activate());
    }

    // Update is called once per frame
    void Update()
    {
        if(spin)
            this.transform.Rotate(0,0,1, Space.World);
    }

    IEnumerator activate(){
        yield return new WaitForSeconds(1f);
        active = true;
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag == "Player"){
            if(coll.gameObject.GetComponent<PlayerGameManager.Stats>().CurrentHP - (dmg/5) >= 0){
                coll.gameObject.GetComponent<PlayerGameManager.Stats>().updateDamage(dmg);
            }else{
                coll.gameObject.GetComponent<PlayerGameManager.Stats>().score("death");
            }
        }
    }
}
