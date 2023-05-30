using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.Netcode;
public class PlayerGameManager : NetworkBehaviour
{
    public GameObject cameraPlayer;
    public GameObject[] arrPiani;
    public Stats myStats;
    // Start is called before the first frame update
    public string namePlayer;
    public string team = "";
    public GameObject canvasMate, canvasStoria, canvasInglese, canvasItaliano, canvasMusica;
    private GameObject [] players;
    public GameObject btnShop;
    public ShopManagerNet shopManager;
    void Start()
    {

        players = GameObject.FindGameObjectsWithTag("Player");
        DontDestroyOnLoad(GameObject.Find("JsonManager"));
        arrPiani = GameObject.FindGameObjectsWithTag("Piani");

        namePlayer = this.gameObject.name;
        this.gameObject.AddComponent<PlayerInventory>();

        myStats = this.gameObject.AddComponent<Stats>();
        myStats.MaxHP = myStats.CurrentHP = 150;
        myStats.coins = 0;
        myStats.floor = 0;
        myStats.armorValue=0;
        myStats.DmgDealt = 0;

        Interactions interactionPlayer = this.gameObject.AddComponent<Interactions>();

        interactionPlayer.shopManager = shopManager;
        //interactionPlayer.btnShop = btnShop;
        interactionPlayer.s = myStats;
        interactionPlayer.piani = arrPiani;
        interactionPlayer._cameraPlayer = cameraPlayer;
        interactionPlayer.canvasMate = canvasMate;
        interactionPlayer.canvasItaliano = canvasItaliano;
        interactionPlayer.canvasStoria = canvasStoria;
        interactionPlayer.canvasMate = canvasMate;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            this.gameObject.transform.Find("CanvasInventory").gameObject.SetActive(true);
        }

        if (this.gameObject.transform.Find("CanvasInventory").gameObject.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
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

        public float scoreTeam; /*dato per score finale per il boss*/
        public void score(string status){
            switch(status){
                case "slash":
                    scoreTeam += DmgDealt;
                    break;
                case "death":
                    scoreTeam -= 30;
                    break;
                default:
                    break;
            }

            if(scoreTeam < 0){
                scoreTeam = 0;
            }
        }

    }
    public class Interactions : MonoBehaviour
    {
        public ShopManager shopManager;
        //public GameObject btnShop;
        public GameObject canvasMate, canvasStoria, canvasInglese, canvasItaliano, canvasMusica;
        public GameObject _cameraPlayer;
        public GameObject[] piani;
        public Stats s;

        void OnCollisionEnter2D(Collision2D coll)
        {
            switch (coll.gameObject.name)
            {
                case "up":
                    if (s.floor <= 2)
                    {
                        this.gameObject.transform.position = piani[s.floor + 1].transform.Find("------Scale------").Find(coll.gameObject.transform.parent.name).Find("down").Find("SpawnPoint").position;
                        s.floor++;
                    }
                    break;
                case "down":
                    if (s.floor >= 1)
                    {
                        this.gameObject.transform.position = piani[s.floor - 1].transform.Find("------Scale------").Find(coll.gameObject.transform.parent.name).Find("up").Find("SpawnPoint").position;
                        s.floor--;
                    }
                    break;
                case "Porta":

                    this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                    this.gameObject.GetComponent<PlayerControllerNet>().speed = 0;

                    switch (coll.gameObject.transform.parent.gameObject.name)
                    {
                        case "Italiano":
                            canvasItaliano.SetActive(true);
                            // SceneManager.LoadScene("ItalianoScene", LoadSceneMode.Additive);
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                canvasItaliano.transform.localScale = new Vector3(0.0075f, 0.0075f, 0.0075f);
                                canvasItaliano.GetComponent<Canvas>().worldCamera = _cameraPlayer.GetComponent<Camera>();
                                StartCoroutine(DoSomethingDelayed(() =>
                                {
                                    canvasItaliano.SetActive(false);
                                }, 120));
                            }, 1));
                            break;
                        case "Storia":
                            canvasStoria.SetActive(true);
                            
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                canvasStoria.transform.localScale = new Vector3(0.0075f, 0.0075f, 0.0075f);
                                canvasStoria.GetComponent<Canvas>().worldCamera = _cameraPlayer.GetComponent<Camera>();
                                StartCoroutine(DoSomethingDelayed(() =>
                                {
                                    canvasItaliano.SetActive(false);
                                }, 120));
                            }, 1));
                            break;
                        case "Musica":
                        
                            break;
                        case "Matematica":
                            canvasMate.SetActive(true);
                            
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                //this.gameObject.transform.Find("MainCameraPlayer").gameObject.GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("Matematica");
                                canvasMate.transform.localScale = new Vector3(0.0075f, 0.0075f, 0.0075f);
                                canvasMate.GetComponent<Canvas>().worldCamera = _cameraPlayer.GetComponent<Camera>();
                                
                                StartCoroutine(DoSomethingDelayed(() =>
                                {
                                    canvasMate.SetActive(false);
                                }, 120));
                            }, 1));
                            break;

                        case "Inglese":

                            break;
                        default:
                            break;
                    }
                    
                    break;
                    
                case "Bancone":
                        //btnShop.SetActive(true);
                    switch (coll.gameObject.transform.parent.gameObject.name)
                    {
                        case "Pozioni":
                            shopManager.idShopSeller = 0;
                            break;
                        case "Armi":
                            shopManager.idShopSeller = 1;
                            break;
                        case "Armature":
                            shopManager.idShopSeller = 2;
                            break;
                        default:
                            break;
                    }
                    break;
            }
            this.GetComponent<PlayerControllerNet>().speed = 5;
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            //s.coins += PlayerDataMinigame.Instance.coins;
            //Debug.Log(s.coins);
        }
        private IEnumerator DoSomethingDelayed(Action action, float t)
        {

            yield return new WaitForSeconds(t);
            action.Invoke();
        }

    }
}
class PlayerInventory : MonoBehaviour
{
    public Item[,] inventoryItems = new Item[3, 4];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                inventoryItems[i, j] = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public class Item
    {
        public string _name;
    }
}

public class PlayerDataMinigame : MonoBehaviour
{
    public static PlayerDataMinigame Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public string playerName;
    public string aula;
    public int coins = 0;
    public float dmg = 0;
}