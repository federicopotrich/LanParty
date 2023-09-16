using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.AddComponent<InteractionPlayerClass>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class InteractionPlayerClass : MonoBehaviour
{
    void FixedUpdate(){
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        this.transform.Translate(new Vector3(x * 5 * Time.deltaTime, y * 5 * Time.deltaTime, this.transform.position.z));

    }

    void Update(){
        
    }
    void AnimationChecker(float x, float y){
        if(x != 0 || y != 0){
            this.gameObject.GetComponent<Animator>().Play("movement_player");
        }else{
            this.gameObject.GetComponent<Animator>().Play("idle_player");
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        switch (col.gameObject.name)
        {
            case "Porta":
                break;
            case "Bancone":
                break;
            default:
            break;
        }
    }
    
}