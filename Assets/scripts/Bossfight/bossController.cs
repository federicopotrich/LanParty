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
        GameObject.Find("Player").GetComponent<PlayerGameManager>().Start();
        GameObject.Find("Player").transform.position = new Vector3(10f, 14f, 0);
        speed = 10f;
        spotIndex = (int) Random.Range(0,3);
        b = false;
        hp = maxHP = 7500;
        HPBar.maxValue = maxHP;
        HPBar.value = hp;
        StartCoroutine(ChangeBehaviour());
        GameObject.Find("Health").GetComponent<Slider>().maxValue = GameObject.Find("Player").GetComponent<PlayerGameManager.Stats>().MaxHP;
        GameObject.Find("Health").GetComponent<Slider>().value = GameObject.Find("Player").GetComponent<PlayerGameManager.Stats>().CurrentHP;
    }

    void Update()
    {
        HPBar.value = hp;
        GameObject.Find("Health").GetComponent<Slider>().value = GameObject.Find("Player").GetComponent<PlayerGameManager.Stats>().CurrentHP;
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
            GameObject.Find("Player").GetComponent<PlayerGameManager.Stats>().setDamageDealth(coll.gameObject.GetComponent<SlashScript>().damage);
            GameObject.Find("Player").GetComponent<PlayerGameManager.Stats>().score("slash");
        }
        //PlayerDataMinigame.Instance.dmg +=  coll.gameObject.GetComponent<SlashScript>().damage;
        /*if(coll.gameObject.GetComponent<attacksPlayer>().namePlayer==player.GetComponent<PlayerGameManager>().networkPlayerName.Value){
            player.GetComponent<PlayerGameManager.Stats>().score("slash");
        }*/
    }
}
