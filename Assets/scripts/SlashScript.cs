using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashScript : MonoBehaviour
{
    public float timeout;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeOut()); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator TimeOut(){
        yield return new WaitForSeconds(timeout);
        Destroy(this.gameObject);
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.name == "boss"){
            GameObject.Find("Player").GetComponent<PlayerGameManager.Stats>().score("slash");            
        }
    }
}
