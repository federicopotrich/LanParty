using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using Unity.Netcode;
using Unity.Networking.Transport;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerGameManager : NetworkBehaviour
{
    public NetworkVariable<FixedString128Bytes> networkPlayerName = new NetworkVariable<FixedString128Bytes>("Player: 0", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<FixedString128Bytes> networkTeam = new NetworkVariable<FixedString128Bytes>("Team: no", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> telecameraNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public string ip;
    public GameObject cameraPlayer;
    public GameObject[] arrPiani;
    public Stats myStats;
    // Start is called before the first frame update
    public string namePlayer;
    public string team = "";
    public GameObject canvasMate, canvasStoria, canvasInglese, canvasItaliano, canvasMusica;
    private GameObject[] players;
    public GameObject btnShop;
    public ShopManagerNet shopManager;
    string[] visibleLayers;
    public GameObject ViewArmature, ViewArmi;
    public TextMeshProUGUI timerText;
    public float duration = 10; // Durata del timer in secondi
    private int timer = 0; // Tempo trascorso
    void Start()
    {
        if (IsHost && SceneManager.GetActiveScene().name!="PalestraScene")
        {
            StartCoroutine(DoSomethingDelayed(() =>
            {
                
                NetworkManager.Singleton.SceneManager.LoadScene("PalestraScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
            }, duration/*1500*/));
        }
        visibleLayers = new string[2] { "Default", "" };
        this.gameObject.name = networkPlayerName.Value.ToString();

        this.transform.Find("CanvasName").Find("Name").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = this.gameObject.name;

        this.transform.Find("CanvasShop").gameObject.layer = telecameraNumber.Value + 5;
        this.transform.Find("CanvasStoria").gameObject.layer = telecameraNumber.Value + 5;
        this.transform.Find("CanvasItaliano").gameObject.layer = telecameraNumber.Value + 5;
        this.transform.Find("Points").gameObject.layer = telecameraNumber.Value + 5;

        visibleLayers[1] = "Player" + telecameraNumber.Value;

        int cullingMask = 0; // Variabile per memorizzare la Culling Mask

        // Itera attraverso gli array dei nomi dei layer visibili
        for (int i = 0; i < visibleLayers.Length; i++)
        {
            int layer = LayerMask.NameToLayer(visibleLayers[i]); // Ottieni l'indice del layer dal suo nome
            cullingMask |= 1 << layer; // Imposta il bit corrispondente all'indice del layer
        }
        this.transform.Find("MainCameraPlayer").gameObject.GetComponent<Camera>().cullingMask = cullingMask;

        //Debug.Log();
        players = GameObject.FindGameObjectsWithTag("Player");
        DontDestroyOnLoad(GameObject.Find("JsonManager"));
        arrPiani = GameObject.FindGameObjectsWithTag("Piani");

        namePlayer = this.gameObject.name;
        this.gameObject.AddComponent<PlayerInventory>();

        myStats = this.gameObject.AddComponent<Stats>();
        myStats.MaxHP = myStats.CurrentHP = 150;
        myStats.coins = 0;
        myStats.floor = 0;
        myStats.armorValue = 0;
        myStats.DmgDealt = 0;
        team = networkPlayerName.Value.ToString();

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

        interactionPlayer.ViewArmature = ViewArmature;
        interactionPlayer.ViewArmi = ViewArmi;
    }
    private IEnumerator DoSomethingDelayed(Action action, float t)
    {

        yield return new WaitForSeconds(t);
        action.Invoke();
    }
    public class Interactions : MonoBehaviour
    {
        public ShopManagerNet shopManager;
        //public GameObject btnShop;
        public GameObject canvasMate, canvasStoria, canvasInglese, canvasItaliano, canvasMusica;
        public GameObject _cameraPlayer;
        public GameObject[] piani;
        public Stats s;
        public GameObject ViewArmature, ViewArmi;

        void OnCollisionEnter2D(Collision2D coll)
        {
            Debug.Log(coll.gameObject.name);
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
                    Debug.Log(coll.gameObject.name);
                    this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                    this.gameObject.GetComponent<PlayerControllerNet>().speed = 0;
                    Debug.Log(this.gameObject.GetComponent<PlayerControllerNet>().speed);

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
                                canvasMate.transform.localScale = new Vector3(0.0075f, 0.0075f, 0.0075f);
                                canvasMate.GetComponent<Canvas>().worldCamera = _cameraPlayer.GetComponent<Camera>();

                                StartCoroutine(DoSomethingDelayed(() =>
                                {
                                    Debug.Log("Points"+canvasMate.transform.Find("MatematicaManager").gameObject.GetComponent<MatematicaGameManager>().points);
                                    this.gameObject.GetComponent<Stats>().coins += canvasMate.transform.Find("MatematicaManager").gameObject.GetComponent<MatematicaGameManager>().points;
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
            }
            if (coll.gameObject.transform.parent.gameObject.name == "Bancone")
            {

                this.transform.Find("CanvasShop").gameObject.SetActive(true);
                this.transform.Find("CanvasShop").Find("background").Find("TextGold").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = ""+this.gameObject.GetComponent<Stats>().coins;
                Debug.Log(coll.gameObject.transform.parent.parent.gameObject.name);
                //btnShop.SetActive(true);
                switch (coll.gameObject.transform.parent.parent.gameObject.name)
                {
                    case "Pozioni":
                        //shopManager.idShopSeller = 0;
                        break;
                    case "Armi":
                        //shopManager.idShopSeller = 1;
                        ViewArmature.gameObject.SetActive(false);
                        ViewArmi.gameObject.SetActive(true);
                        break;
                    case "Armature":
                        ViewArmature.gameObject.SetActive(true);
                        ViewArmi.gameObject.SetActive(false);
                        //shopManager.idShopSeller = 2;
                        break;
                    default:
                        break;
                }
            }
            this.GetComponent<PlayerControllerNet>().speed = 5;
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            //s.coins += PlayerDataMinigame.Instance.coins;
            //Debug.Log(s.coins);
        }
        void OnCollisionExit2D(Collision2D coll)
        {
            if (coll.gameObject.transform.parent.gameObject.name == "Bancone")
            {
                this.transform.Find("CanvasShop").gameObject.SetActive(false);
            }
        }
        private IEnumerator DoSomethingDelayed(Action action, float t)
        {

            yield return new WaitForSeconds(t);
            action.Invoke();
        }

    }
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.P))
        {
            this.gameObject.transform.Find("CanvasInventory").gameObject.SetActive(true);
        }
        timer += (int)(Time.deltaTime%60);
        Debug.Log(timer);
        // Verifica se il timer ha raggiunto la durata desiderata
        if (timer >= duration)
        {
            // Il tempo è scaduto
            Debug.Log("Timer scaduto!");

            // Puoi eseguire altre azioni qui

            // Resetta il timer
            timer = 0;
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