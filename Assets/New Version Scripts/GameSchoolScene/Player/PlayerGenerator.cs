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
        this.gameObject.AddComponent<Stats>();
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
                switch(col.gameObject.transform.parent.gameObject.name){
                    case "Italiano":
                        UnityEngine.SceneManagement.SceneManager.LoadScene("ItalianoScene", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                        break;
                    
                    case "Storia":
                        UnityEngine.SceneManagement.SceneManager.LoadScene("StoriaScene", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                        break;
                    
                    case "Matematica":
                        UnityEngine.SceneManagement.SceneManager.LoadScene("MateScene", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                        break;
                    
                    case "Musica":
                        UnityEngine.SceneManagement.SceneManager.LoadScene("MiniGame_Musica", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                        break;

                    default:
                    break;
                }
                break;
            case "Bancone":
                break;
            default:
            break;
        }
    }
    
}

public class Stats : MonoBehaviour
{
    //Dichiarazione delle variabili
    public int MaxHP, CurrentHP, coins = 0, floor, DmgDealt, armorValue;

    //Funzione che aggiorna gli hp in base al danno
    public void updateDamage(int dmg)
    {
        CurrentHP -= (dmg - (armorValue / 5));

        if (CurrentHP <= 0)
        {
            score("death");
        }
    }

    //Setta il danno fatto dal player
    public void setDamageDealth(int dmg)
    {
        DmgDealt = dmg;
    }

    //Setta l'armor del player
    public void setArmor(int armor)
    {
        armorValue = armor;
        MaxHP = 100 + (3 * armorValue);
        CurrentHP = MaxHP;
    }

    public void heal(int cura)
    {
        CurrentHP += cura;
    }

    public float scoreTeam; /*dato per score finale per il boss*/
    public void score(string status)
    {
        switch (status)
        {
            case "slash":
                scoreTeam += DmgDealt;
                break;
            case "death":
                scoreTeam -= 30;
                CurrentHP = MaxHP;
                this.gameObject.transform.position = new Vector3(10f, 14f, 0);
                break;
            default:
                break;
        }

        if (scoreTeam < 0)
        {
            scoreTeam = 0;
        }
    }

}
