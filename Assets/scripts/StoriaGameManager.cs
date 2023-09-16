using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.EventSystems;
public class StoriaGameManager : MonoBehaviour
{

    public GameObject[] quesiti;
    public Sprite s;
    public int score = 0;
    // Start is called before the first frame update
    public quest[] arrayData;
    public GameObject btn;
    public int random;
    public quest q;
    [System.Serializable]
    public class dataClass
    {
        public quest[] data;
    }
    public class quest
    {
        public int id;
        public string immagine;
        public string nome;
        public int anno;
    }
    void Start()
    {
        GameObject.Find("Player").GetComponent<PlayerControllerNet>().speed = 0;
        string filePath = Application.dataPath + "/StreamingAssets/storia.json";
        dataClass d = JsonConvert.DeserializeObject<dataClass>(File.ReadAllText(filePath));
        arrayData = d.data;
    
        random = UnityEngine.Random.Range(1, 16);
        q = new quest();
        q.id = d.data[random].id;
        q.nome = d.data[random].nome;
        q.anno = d.data[random].anno;
        q.immagine = d.data[random].immagine;

        GameObject.Find("TestoAnno").GetComponent<TMPro.TextMeshProUGUI>().text = GameObject.Find("Slider").GetComponent<UnityEngine.UI.Slider>().value + " DC";
        string replacedString = q.immagine.Replace(".png", "");
        replacedString = replacedString.Replace(".jpg", "");
        GameObject.Find("ImageStoria").GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(replacedString);

    }
    public void checkAnswer()
    {
        if(Math.Abs(q.anno - GameObject.Find("Slider").GetComponent<UnityEngine.UI.Slider>().value) <= 5){
            score++;
        }
        Start();
    }
    public static List<int> GenerateRandom(int Lenght, int min, int max)
    {
        int Rand;

        List<int> list = new List<int>();

        list = new List<int>(new int[Lenght]);

        for (int j = 1; j < Lenght; j++)
        {
            Rand = UnityEngine.Random.Range(min, max);

            while (list.Contains(Rand))
            {
                Rand = UnityEngine.Random.Range(min, max);
            }

            list[j] = Rand;
        }
        return list;
    }
    public void updateValue()
    {
        GameObject.Find("TestoAnno").GetComponent<TMPro.TextMeshProUGUI>().text = GameObject.Find("Slider").GetComponent<UnityEngine.UI.Slider>().value + " DC";
    }
}
public class StoriaDataElement: MonoBehaviour{

}