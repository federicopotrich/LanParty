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
using System.IO;

public class PlayerGameManager : NetworkBehaviour
{
    public NetworkVariable<Int32> networkTeam = new NetworkVariable<Int32>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<Int32> telecameraNumber = new NetworkVariable<Int32>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<FixedString128Bytes> networkPlayerName = new NetworkVariable<FixedString128Bytes>("Host", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public Image armorImage;
    public Image weaponImage;
    public ItemsScriptable armorSelected;
    public ItemsScriptable weaponSelected;

    public ItemsScriptable[] itemsArr;
    public Image[] arrSlotInventory;
    public GameObject attackPrefabs;
    public GameObject cameraPlayer;
    public GameObject[] arrPiani;
    public Stats myStats;
    // Start is called before the first frame update
    public string namePlayer;
    public string team = "";
    private GameObject[] players;
    public GameObject btnShop;
    public ShopManagerNet shopManager;
    public GameObject ViewArmature, ViewArmi;
    public float duration = 10; // Durata del timer in second

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (SceneManager.GetActiveScene().name == "GameSchoolScene")
        {

            //Debug.Log();
            players = GameObject.FindGameObjectsWithTag("Player");
            arrPiani = GameObject.FindGameObjectsWithTag("Piani");

            namePlayer = this.gameObject.name;
            this.gameObject.AddComponent<PlayerInventory>();

            myStats = this.gameObject.AddComponent<Stats>();
            myStats.MaxHP = myStats.CurrentHP = 150;
            myStats.coins = 0;
            myStats.floor = 0;
            myStats.armorValue = 0;
            myStats.DmgDealt = 0;

            Interactions interactionPlayer = this.gameObject.AddComponent<Interactions>();

            interactionPlayer.shopManager = shopManager;
            //interactionPlayer.btnShop = btnShop;
            interactionPlayer.s = myStats;
            interactionPlayer.piani = arrPiani;
            interactionPlayer._cameraPlayer = cameraPlayer;


            interactionPlayer.ViewArmature = ViewArmature;
            interactionPlayer.ViewArmi = ViewArmi;

            this.gameObject.AddComponent<attacksPlayer>();
            this.GetComponent<attacksPlayer>().Attacks = attackPrefabs;
            this.GetComponent<attacksPlayer>().cooldown = 2f;
            duration = 24*60f;
            StartCoroutine(DoSomethingDelayed(() =>
            {
                WriteCoinsDamageToFile();
                SceneManager.LoadScene("PalestraScene");
            }, duration));
        }
        if (SceneManager.GetActiveScene().name == "PalestraScene")
        {
            GameObject.Find("UI").transform.SetParent(this.gameObject.transform);
            GameObject.Find("UI").transform.localPosition = new Vector3(0,0,0);
            this.GetComponent<PlayerControllerNet>().speed = 10;
            duration = 10*60f;
            StartCoroutine(DoSomethingDelayed(() =>
            {
                WriteTotalDamageToFile();
                Application.Quit();
            }, duration));
        }


    }
    private IEnumerator DoSomethingDelayed(Action action, float t)
    {
        yield return new WaitForSeconds(t);
        action.Invoke();
    }
    public class Interactions : NetworkBehaviour
    {
        public ShopManagerNet shopManager;
        public GameObject _cameraPlayer;
        public GameObject[] piani;
        public Stats s;
        public GameObject ViewArmature, ViewArmi;

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
                    //Debug.Log(coll.gameObject.name);
                    this.gameObject.GetComponent<PlayerControllerNet>().speed = 0;
                    this.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 7, this.transform.position.z);
                    //Debug.Log(this.gameObject.GetComponent<PlayerControllerNet>().speed);
                    switch (coll.gameObject.transform.parent.gameObject.name)
                    {
                        /*case "Italiano":
                            SceneManager.LoadScene("ItalianoScene", LoadSceneMode.Additive);
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                GameObject.Find("CanvasItaliano").GetComponent<Canvas>().worldCamera = _cameraPlayer.GetComponent<Camera>();
                                GameObject.Find("CanvasItaliano").transform.localScale = new Vector3(0.047f, 0.047f, 0.047f);
                                GameObject.Find("CanvasItaliano").transform.position = this.transform.position;
                            }, 1));
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                this.gameObject.GetComponent<Stats>().coins += GameObject.Find("ItalianoGameManager").GetComponent<jsonDataItaliano>().points * 20;
                                SceneManager.UnloadSceneAsync("ItalianoScene");
                                this.gameObject.GetComponent<PlayerControllerNet>().speed = 10;
                            }, 120));
                            break;*/
                        case "Storia":
                            SceneManager.LoadScene("StoriaScene", LoadSceneMode.Additive);
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                GameObject.Find("CanvasStoria").GetComponent<Canvas>().worldCamera = _cameraPlayer.GetComponent<Camera>();
                                GameObject.Find("CanvasStoria").transform.localScale = new Vector3(0.047f, 0.047f, 0.047f);
                                GameObject.Find("CanvasStoria").transform.position = this.transform.position;
                            }, 1));
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                
                                this.gameObject.GetComponent<Stats>().coins += 5;
                                SceneManager.UnloadSceneAsync("StoriaScene");
                                this.gameObject.GetComponent<PlayerControllerNet>().speed = 10;
                            }, 120f));
                            break;

                        case "Musica":
                            SceneManager.LoadScene("MiniGame_Musica", LoadSceneMode.Additive);
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                GameObject.Find("MusicCanvas").GetComponent<Canvas>().worldCamera = _cameraPlayer.GetComponent<Camera>();
                                GameObject.Find("MusicCanvas").transform.localScale = new Vector3(0.055f, 0.055f, 0.055f);
                                GameObject.Find("MusicCanvas").transform.position = this.transform.position;
                            }, 0.5f));
                            break;

                        case "Matematica":
                            SceneManager.LoadScene("MateScene", LoadSceneMode.Additive);
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                GameObject.Find("Points").GetComponent<Canvas>().worldCamera = _cameraPlayer.GetComponent<Camera>();
                                GameObject.Find("Points").transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
                                GameObject.Find("Points").transform.position = this.transform.position;
                            }, 1));
                            StartCoroutine(DoSomethingDelayed(() =>
                            {
                                this.gameObject.GetComponent<Stats>().coins += GameObject.Find("MatematicaManager").GetComponent<MatematicaGameManager>().points * 20;
                                SceneManager.UnloadSceneAsync("MateScene");
                                this.gameObject.GetComponent<PlayerControllerNet>().speed = 10;

                            }, 120));
                            break;

                        case "Inglese":
                            break;
                    }

                    break;
            }
            if (coll.gameObject.transform.parent != null)
            {

                if (coll.gameObject.transform.parent.gameObject.name == "Bancone")
                {

                    this.transform.Find("CanvasShop").gameObject.SetActive(true);
                    this.transform.Find("CanvasShop").Find("background").Find("TextGold").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "" + this.gameObject.GetComponent<Stats>().coins;
                    //Debug.Log(coll.gameObject.transform.parent.parent.gameObject.name);
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
            }
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            //s.coins += PlayerDataMinigame.Instance.coins;
            //Debug.Log(s.coins);
        }
        void OnCollisionExit2D(Collision2D coll)
        {
            if (coll.gameObject.transform.parent != null)
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
        if (Input.GetMouseButtonUp(0) )
        {
            GetComponent<attacksPlayer>().atk();
        }
    }
    private void WriteTotalDamageToFile()
    {
        string fileName = GameObject.FindGameObjectWithTag("class") + "_Damage.txt";
        string filePath = Path.Combine(Application.dataPath, fileName);

        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/StreamingAssets/"+fileName, false))
        {
            writer.Write(this.gameObject.GetComponent<PlayerGameManager.Stats>().scoreTeam);
        }
    }
    private void WriteCoinsDamageToFile()
    {
        string fileName = GameObject.FindGameObjectWithTag("class") + "Coins.txt";
        string filePath = Path.Combine(Application.dataPath, fileName);

        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/StreamingAssets/"+fileName, false))
        {
            writer.Write(this.gameObject.GetComponent<PlayerGameManager.Stats>().coins);
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
}
public class PlayerInventory : MonoBehaviour
{
    public List<ItemsScriptable> inventoryItems = new List<ItemsScriptable>();


    private ItemsScriptable armorSelected;
    private ItemsScriptable weaponSelected;

    public void AddItem(ItemsScriptable item)
    {
        inventoryItems.Add(item);
    }

    public bool ContainsItem(ItemsScriptable item)
    {
        return inventoryItems.Contains(item);
    }
    public void RemoveItem(ItemsScriptable item)
    {
        inventoryItems.Remove(item);
    }

    public void SetArmor(ItemsScriptable armor)
    {
        if (inventoryItems.Contains(armor))
        {
            armorSelected = armor;
            this.gameObject.GetComponent<PlayerGameManager>().armorImage.sprite = armor.image_weapon_Armor;
        }
        else
        {
            Debug.Log("L'armatura selezionata non è presente nell'inventario.");
        }
    }

    public void SetWeapon(ItemsScriptable weapon)
    {
        if (inventoryItems.Contains(weapon))
        {
            weaponSelected = weapon;
            this.gameObject.GetComponent<PlayerGameManager>().weaponImage.sprite = weapon.image_weapon_Armor;
        }
        else
        {
            Debug.Log("L'arma selezionata non è presente nell'inventario.");
        }
    }

    public ItemsScriptable GetArmor()
    {
        return armorSelected;
    }

    public ItemsScriptable GetWeapon()
    {
        return weaponSelected;
    }

}
public class attacksPlayer : MonoBehaviour
{
    public float cooldown;
    float lastAttackTime = 0f;
    public GameObject Attacks;
    public void atk()
    {
        if (Time.time - lastAttackTime >= cooldown)
        {
            //Debug.Log("Attacking");
            GameObject attackInstance = Instantiate(Attacks, new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z), Quaternion.identity);
            Destroy(attackInstance, 1f);
            lastAttackTime = Time.time;
        }

    }
}
