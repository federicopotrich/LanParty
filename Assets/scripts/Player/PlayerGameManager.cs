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
    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("JsonManager"));
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
        interactionPlayer._cameraPlayer = cameraPlayer;
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
        public int MaxHP, CurrentHP, coins = 0, floor, DmgDealt = 0;
        public float scoreTeam; /*dato per score finale per il boss*/
    }
    public class Interactions : MonoBehaviour
    {
        public GameObject _cameraPlayer;
        public GameObject[] piani;
        public Stats s;

        void OnCollisionEnter2D(Collision2D coll)
        {
            Debug.Log(coll.collider);
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
                    switch (coll.gameObject.transform.parent.gameObject.name)
                    {
                        case "Italiano":
                            SceneManager.LoadScene("ItalianoScene", LoadSceneMode.Additive);
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                GameObject.Find("CanvasItaliano").transform.position = this.gameObject.transform.position;
                                GameObject.Find("CanvasItaliano").transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
                                GameObject.Find("CanvasItaliano").GetComponent<Canvas>().worldCamera = _cameraPlayer.GetComponent<Camera>();
                                StartCoroutine(DoSomethingDelayed(() =>
                                {
                                    this.GetComponent<PlayerControllerNet>().speed = 5;
                                }, 120));
                            }, 1));
                            break;
                        case "Storia":

                            // Carica la scena adattiva solo per il player che ha colliso con la porta
                            SceneManager.LoadScene("StoriaScene", LoadSceneMode.Additive);
                            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                            gameObject.GetComponent<PlayerControllerNet>().speed = 0;
                            for (int i = 0; i < players.Length; i++)
                            {
                                Debug.Log(players[i]);
                                if(players[i] != this.gameObject){
                                    players[i].transform.Find("MainCameraPlayer").gameObject.SetActive(false);
                                }   
                            }
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                // Sposta il player nella scena adattiva alla sua posizione originale
                                GameObject.Find("CanvasStoria").transform.position = this.gameObject.transform.position;
                                GameObject.Find("CanvasStoria").transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
                                GameObject.Find("CanvasStoria").GetComponent<Canvas>().worldCamera = _cameraPlayer.GetComponent<Camera>();

                                // Ripristina il movimento del player dopo un ritardo di 1 secondo
                                StartCoroutine(DoSomethingDelayed(() =>
                                {
                                    gameObject.GetComponent<PlayerControllerNet>().speed = 5;
                                }, 1));

                            }, 0));

                            break;
                        case "Musica":
                            SceneManager.LoadScene("MiniGame_Musica", LoadSceneMode.Additive);
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                GameObject.Find("MusicCanvas").transform.position = this.gameObject.transform.position;
                                GameObject.Find("MusicCanvas").transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
                                GameObject.Find("MusicCanvas").GetComponent<Canvas>().worldCamera = _cameraPlayer.GetComponent<Camera>();
                                StartCoroutine(DoSomethingDelayed(() =>
                                {
                                    this.GetComponent<PlayerControllerNet>().speed = 5;
                                }, 120));
                            }, 1));
                            break;
                        case "Matematica":
                        /*
                            GameObject gTmp = new GameObject("GO_"+this.gameObject.name);

                            gTmp.AddComponent<PlayerDataMinigame>();
                            PlayerDataMinigame playerData = gTmp.GetComponent<PlayerDataMinigame>();
                            playerData.playerName = this.gameObject.name;
                            playerData.aula = coll.gameObject.transform.parent.gameObject.name;
                        */
                            SceneManager.LoadScene("MateScene", LoadSceneMode.Additive);
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                GameObject.Find("Points").transform.position = this.gameObject.transform.position;
                                GameObject.Find("Points").transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
                                GameObject.Find("Points").GetComponent<Canvas>().worldCamera = _cameraPlayer.GetComponent<Camera>();
                                StartCoroutine(DoSomethingDelayed(() =>
                                {
                                    this.GetComponent<PlayerControllerNet>().speed = 5;
                                    //Destroy(gTmp);
                                }, 120));
                            }, 1));
                            
                            break;
                        case "Inglese":
                            SceneManager.LoadScene("IngScene", LoadSceneMode.Additive);
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                GameObject.Find("IngCanvas").transform.position = this.gameObject.transform.position;
                                GameObject.Find("IngCanvas").transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
                                GameObject.Find("IngCanvas").GetComponent<Canvas>().worldCamera = _cameraPlayer.GetComponent<Camera>();
                                StartCoroutine(DoSomethingDelayed(() =>
                                {
                                    this.GetComponent<PlayerControllerNet>().speed = 5;
                                }, 120));
                            }, 1));
                            break;
                        default:
                            break;
                    }
                    this.gameObject.GetComponent<PlayerControllerNet>().speed = 0;
                    _cameraPlayer.GetComponent<Camera>().enabled = true;
                    _cameraPlayer.SetActive(false);
                    _cameraPlayer.SetActive(true);
                    break;
            }
            s.coins += PlayerDataMinigame.Instance.coins;
            Debug.Log(s.coins);
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
        public string _type;
    }
}

public class PlayerDataMinigame : MonoBehaviour
{
    public static PlayerDataMinigame Instance{get; private set;}

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    public string playerName;
    public string aula;
    public int coins = 0;
    public float dmg = 0;
}