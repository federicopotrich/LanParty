using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class bossController : MonoBehaviour
{
    private int maxHP;
    public int hp;
    public Transform[] BossSpots;
    float speed;
    int spotIndex;
    bool b;
    public Attacks att;

    public Slider HPBar;

    void Start()
    {
        speed = 10f;
        spotIndex = (int) Random.Range(0,3);
        b = false;
        hp = maxHP = 50;
        HPBar.maxValue = maxHP;
        HPBar.value = hp;
        StartCoroutine(ChangeBehaviour());
    }

    void Update()
    {
        HPBar.value = hp;
    }

    IEnumerator Move(){

        while(b==false){
            yield return new WaitForFixedUpdate();
            this.transform.position = Vector2.MoveTowards(this.transform.position, BossSpots[spotIndex].transform.position, speed*Time.deltaTime);
            if(this.transform.position==BossSpots[spotIndex].transform.position)
                b=true;
        }
        StartCoroutine(ChangeBehaviour());
    }

    IEnumerator ChangeDirection(){

        spotIndex = (int) Random.Range(0,3);
        b = false;
        yield return new WaitForSeconds(0);
        StartCoroutine(ChangeBehaviour());
    }

    IEnumerator ChangeBehaviour(){

        if(!b)
            yield return StartCoroutine(Move());   
        else{
            int index = (int) Random.Range(0,21);
            if(index%2==0){
                index = (int) Random.Range(0,6);
                yield return StartCoroutine(att.call(index));
                StartCoroutine(ChangeBehaviour());
            }
            else
                yield return ChangeDirection();
        }
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag=="PlayerAttack"){
            Debug.Log("AAAAAAAAAAAAAAAAAAAA");
            hp = hp - coll.gameObject.GetComponent<SlashScript>().damage;
        }
    }
}
