using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WeponsManager[] armi;
    public DistanceWeapon[] armiDistanti;
    public ArmorsManager[] armature;

    public GameObject container;
    public GameObject slotShop;
    public GameObject Shop;

    public GameObject inventory;

    public WeponsManager weaponSelected;
    public ArmorsManager armorSelected;

    public GameObject placeForWeapon;

    public string[] nomiAnimazioniArmature;

    public GameObject primaryGear;
    public GameObject secondaryGear;

    public TMPro.TextMeshProUGUI textFloor;
    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("DataStoria").Length > 0){
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("DataStoria").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("DataStoria")[i]);
            }
        }
        if(GameObject.FindGameObjectsWithTag("DataItaliano").Length > 0){
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("DataItaliano").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("DataItaliano")[i]);
            }
        }

        for (int i = 0; i < armi.Length; i++)
        {
            GameObject gInstantiated = GameObject.Instantiate(slotShop, new Vector3(), Quaternion.identity);
            gInstantiated.transform.SetParent(container.transform);
            gInstantiated.transform.localScale = new Vector3(1, 1, 1);

            gInstantiated.transform.Find("Image").GetComponent<UnityEngine.UI.Image>().sprite = armi[i].imageWeapon;
            gInstantiated.transform.Find("TextNome").GetComponent<TMPro.TextMeshProUGUI>().text = armi[i].nome;
            gInstantiated.transform.Find("TextDmg").GetComponent<TMPro.TextMeshProUGUI>().text = "" + armi[i].dmg;
            gInstantiated.transform.Find("TextCost").GetComponent<TMPro.TextMeshProUGUI>().text = "" + armi[i].cost;
            gInstantiated.transform.Find("TextRarita").GetComponent<TMPro.TextMeshProUGUI>().text = armi[i].rarita;
            Destroy(gInstantiated.GetComponent<armorSelected>());
            gInstantiated.GetComponent<WeaponSelection>().id = armi[i];
        }
        for (int i = 0; i < armiDistanti.Length; i++)
        {
            GameObject gInstantiated = GameObject.Instantiate(slotShop, new Vector3(), Quaternion.identity);
            gInstantiated.transform.SetParent(container.transform);
            gInstantiated.transform.localScale = new Vector3(1, 1, 1);

            gInstantiated.transform.Find("Image").GetComponent<UnityEngine.UI.Image>().sprite = armiDistanti[i].imageWeapon;
            gInstantiated.transform.Find("TextNome").GetComponent<TMPro.TextMeshProUGUI>().text = armiDistanti[i].nome;
            gInstantiated.transform.Find("TextDmg").GetComponent<TMPro.TextMeshProUGUI>().text = "" + armiDistanti[i].dmg;
            gInstantiated.transform.Find("TextCost").GetComponent<TMPro.TextMeshProUGUI>().text = "" + armiDistanti[i].cost;
            gInstantiated.transform.Find("TextRarita").GetComponent<TMPro.TextMeshProUGUI>().text = armiDistanti[i].rarita;
            Destroy(gInstantiated.GetComponent<armorSelected>());
            gInstantiated.GetComponent<WeaponSelection>().id = armi[i];
        }

        for (int i = 0; i < armature.Length; i++)
        {
            GameObject gInstantiated = GameObject.Instantiate(slotShop, new Vector3(), Quaternion.identity);
            gInstantiated.transform.SetParent(container.transform);
            gInstantiated.transform.localScale = new Vector3(1, 1, 1);

            gInstantiated.transform.Find("Image").GetComponent<UnityEngine.UI.Image>().sprite = armature[i].imageArmor;
            gInstantiated.transform.Find("TextNome").GetComponent<TMPro.TextMeshProUGUI>().text = armature[i].nome;
            gInstantiated.transform.Find("TextDmg").GetComponent<TMPro.TextMeshProUGUI>().text = "" + armature[i].defence;
            gInstantiated.transform.Find("TextCost").GetComponent<TMPro.TextMeshProUGUI>().text = "" + armature[i].cost;
            gInstantiated.transform.Find("TextRarita").GetComponent<TMPro.TextMeshProUGUI>().text = armature[i].rarita;

            Destroy(gInstantiated.GetComponent<WeaponSelection>());

            //Destroy(gInstantiated.GetComponent<UnityEngine.UI.Button>());

            //gInstantiated.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();

            gInstantiated.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => gInstantiated.GetComponent<armorSelected>().select());
            gInstantiated.GetComponent<armorSelected>().id = armature[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        primaryGear.GetComponent<UnityEngine.UI.Image>().sprite = weaponSelected.imageWeapon;
        if (Input.GetKeyUp(KeyCode.I))
        {
            inventory.SetActive(!inventory.activeSelf);
        }

        if (inventory.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        placeForWeapon.GetComponent<SpriteRenderer>().sprite = weaponSelected.imageWeapon;



        switch (GameObject.Find("Player").GetComponent<PlayerController>().piano)
        {
            case 0:
                textFloor.text = "Piano Terra";
                break;
            case 1:
                textFloor.text = "Primo Piano";
                break;
            case 2:
                textFloor.text = "Secondo Piano";
                break;
            case 3:
                textFloor.text = "Terzo Piano";
                break;
            case 4:
                textFloor.text = "Quarto Piano";
                break;
        }

    }

    public void generateQuestion(string typeQuestion)
    {
        switch (typeQuestion)
        {
            case "Italiano":
                List <int> listaItaliano = GenerateRandom(4, 0, GameObject.Find("JsonItaliano").GetComponent<jsonDataItaliano>().arrayData.Length);
                for (int i = 0; i < listaItaliano.ToArray().Length; i++)
                {
                    GameObject g = new GameObject("DataItaliano"+i);

                    g.tag = "DataItaliano";

                    g.AddComponent<italianoData>();
                    g.GetComponent<italianoData>().poeta = GameObject.Find("JsonItaliano").GetComponent<jsonDataItaliano>().arrayData[listaItaliano[i]].poeta;
                    g.GetComponent<italianoData>().opere = GameObject.Find("JsonItaliano").GetComponent<jsonDataItaliano>().arrayData[listaItaliano[i]].opere;
                    g.GetComponent<italianoData>().citazioni = GameObject.Find("JsonItaliano").GetComponent<jsonDataItaliano>().arrayData[listaItaliano[i]].citazioni;
                    DontDestroyOnLoad(g);
                }
                
                UnityEngine.SceneManagement.SceneManager.LoadScene("ItalianoScene");
                break;
            case "Storia":
                //Debug.Log("sei entrato nella classe di: ST");

                List <int> l = GenerateRandom(3, 0, GameObject.Find("JsonStoria").GetComponent<jsonData>().arrayData.Length);
                for (int i = 0; i < l.ToArray().Length; i++)
                {
                    GameObject g = new GameObject("DataStoria"+i);

                    g.tag = "DataStoria";

                    g.AddComponent<storiaData>();
                    g.GetComponent<storiaData>().id = GameObject.Find("JsonStoria").GetComponent<jsonData>().arrayData[l[i]].id;
                    g.GetComponent<storiaData>().nome = GameObject.Find("JsonStoria").GetComponent<jsonData>().arrayData[l[i]].nome;
                    g.GetComponent<storiaData>().anno = GameObject.Find("JsonStoria").GetComponent<jsonData>().arrayData[l[i]].anno;
                    g.GetComponent<storiaData>().immagine = GameObject.Find("JsonStoria").GetComponent<jsonData>().arrayData[l[i]].immagine;

                    DontDestroyOnLoad(g);
                }
                
                UnityEngine.SceneManagement.SceneManager.LoadScene("StoriaScene");

                break;
            case "Geografia":
                Debug.Log("sei entrato nella classe di: GE");
                break;
            case "Inglese":
                Debug.Log("sei entrato nella classe di: IN");
                break;
            case "Matematica":
                Debug.Log("sei entrato nella classe di: MA");
                break;
            case "Scienze":
                Debug.Log("sei entrato nella classe di: SC");
                break;
            case "Arte":
                Debug.Log("sei entrato nella classe di: AR");
                break;
            case "Musica":
                Debug.Log("sei entrato nella classe di: MU");
                break;
            case "Tecnologia":
                Debug.Log("sei entrato nella classe di: TE");
                break;
        }
    }
    public static List<int> GenerateRandom(int Lenght, int min, int max)
    {
        int Rand;

        List<int> list = new List<int>();

        list = new List<int>(new int[Lenght]);

        for (int j = 1; j < Lenght; j++)
        {
            Rand = Random.Range(min, max);

            while (list.Contains(Rand))
            {
                Rand = Random.Range(min, max);
            }

            list[j] = Rand;
        }
        return list;
    }
}
public class storiaData : MonoBehaviour
{
    public int id;
    public string immagine;
    public string nome;
    public int anno;
}

public class italianoData : MonoBehaviour
{
    public string poeta;
    public string[] opere;
    public string citazioni;
}