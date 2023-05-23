using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
public class PlayerGameManager : MonoBehaviour
{
    public GameObject [] arrPiani;
    public Stats myStats;
    // Start is called before the first frame update
    public string namePlayer;
    public string team;
    void Start()
    {
        arrPiani = GameObject.FindGameObjectsWithTag("Piani");

        namePlayer = this.gameObject.name;
        this.gameObject.AddComponent<PlayerInventory>();

        myStats = this.gameObject.AddComponent<Stats>();
        myStats.MaxHP = myStats.CurrentHP = 150;
        myStats.coins = 0;
        myStats.floor = 0;

        Interactions interactionPlayer = this.gameObject.AddComponent<Interactions>();

        interactionPlayer.s = myStats;
        interactionPlayer.piani = arrPiani;
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P)){
            this.gameObject.transform.Find("CanvasInventory").gameObject.SetActive(true);
        }

        if(this.gameObject.transform.Find("CanvasInventory").gameObject.activeSelf){
            Time.timeScale = 0;
        }else{
            Time.timeScale = 1;
        }
    }
    
    public class Stats : MonoBehaviour
    {
        public int MaxHP, CurrentHP, coins, floor;
    }
    public class Interactions : MonoBehaviour
    {
        public GameObject [] piani;
        public Stats s;
        void OnCollisionEnter2D(Collision2D coll){
            switch (coll.gameObject.name)
            {
                case "up":
                    if (s.floor <= 2)
                    {
                        this.gameObject.transform.position = piani[s.floor+1].transform.Find("------Scale------").Find(coll.gameObject.transform.parent.name).Find("down").Find("SpawnPoint").position;
                        s.floor++;
                    }
                    break;
                case "down":
                    if (s.floor >= 1)
                    {
                        this.gameObject.transform.position = piani[s.floor-1].transform.Find("------Scale------").Find(coll.gameObject.transform.parent.name).Find("up").Find("SpawnPoint").position;
                        s.floor--;
                    }
                    break;
                case "Porta":
                    this.gameObject.tag = coll.gameObject.transform.parent.gameObject.name;
                    switch (coll.gameObject.transform.parent.gameObject.name)
                    {
                        case "Italiano":
                            SceneManager.LoadScene("ItalianoScene", LoadSceneMode.Additive);
                            break;
                        case "Storia":
                            SceneManager.LoadScene("StoriaScene", LoadSceneMode.Additive);
                            break;
                        case "Musica":
                            SceneManager.LoadScene("MiniGame_Musica", LoadSceneMode.Additive);
                            break;
                        case "Matematica":
                            SceneManager.LoadScene("MateScene", LoadSceneMode.Additive);
                            GameObject.Find("Points").transform.position = this.gameObject.transform.position;
                            break;
                        case "Inglese":
                            SceneManager.LoadScene("IngScene", LoadSceneMode.Additive);
                            break;
                        default:
                            break;
                    }
                    this.gameObject.GetComponent<PlayerControllerNet>().speed = 0;
                    break;
            }
        }
    }
}
class PlayerInventory : MonoBehaviour
{
    public Item [,] inventoryItems = new Item[3,4];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                inventoryItems[i,j] = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public class Item{
        public string _name;
        public string _type; 
    }
}

public class PlayerDataMinigame
{
    public string playerName;
    public string aula;
    public int coins;
}