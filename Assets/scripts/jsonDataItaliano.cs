using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class jsonDataItaliano : MonoBehaviour
{
    private string jsonDataString;
    public quest[] arrayData;


    private TextMeshProUGUI titleText;
    private TextMeshProUGUI questText;

    private Button btn1;
    private Button btn2;
    private Button btn3;
    private Button btn4;

    private int points = 0;

    private int difficulty = 1;

    private dataClass d;

    private quest target;

    [System.Serializable]
    public class dataClass
    {
        public quest[] data;
        public string[] opzioni;
    }
    public class quest
    {
        public string poeta;
        public string[] opere;
        public string citazioni;
    }


    void Start()
    {
        // titleText = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        // questText = GameObject.Find("questText").GetComponent<TextMeshProUGUI>();
        // btn1 = GameObject.Find("Button1").GetComponent<Button>();
        // btn2 = GameObject.Find("Button2").GetComponent<Button>();
        // btn3 = GameObject.Find("Button3").GetComponent<Button>();
        // btn4 = GameObject.Find("Button4").GetComponent<Button>();

        jsonDataString = File.ReadAllText(Application.dataPath + "/json/italiano.json");
        d = JsonConvert.DeserializeObject<dataClass>(jsonDataString);
        arrayData = d.data;        

        //init();

    }
    void Update()
    {

    }

    public void init()
    {
        target = arrayData[Random.Range(0, d.data.Length)];
        int i = 0;
        for (int j = 0; j < d.opzioni.Length; j++)
        {
            if (d.opzioni[j] == target.poeta)
            {
                i = j;
                break;
            }
        }

        int[] numbers = new int[4];
        numbers[0] = i;
        numbers[1] = -1;
        numbers[2] = -1;
        numbers[3] = -1;
        for (int j = 1; j < 4;)
        {
            int n = Random.Range(0, d.opzioni.Length);
            if (n != numbers[0] && n != numbers[1] && n != numbers[2] && n != numbers[3])
            {
                numbers[j] = n;
                j++;
            }

        }


        Debug.Log("1*: "+numbers[0] + " 2*: " + numbers[1] + " 3*: " + numbers[2] + " 4*: " + numbers[3]);
        for (int j = 0; j < 50; j++)
        {
            int tmp1 = Random.Range(0, 4);
            int tmp2 = Random.Range(0, 4);
            int tmp = numbers[tmp1];
            numbers[tmp1] = numbers[tmp2];
            numbers[tmp2] = tmp;
        }

        if (difficulty == 1)
        {
            titleText.text = "Indovina il poeta dall'opera";
            questText.text = "" + target.opere[Random.Range(0, target.opere.Length)];
        }
        else
        {
            titleText.text = "Indovina il poeta dalla citazione";
            questText.text = "" + target.citazioni;
        }

        btn1.GetComponentInChildren<TextMeshProUGUI>().text = "" + d.opzioni[numbers[0]];
        btn2.GetComponentInChildren<TextMeshProUGUI>().text = "" + d.opzioni[numbers[1]];
        btn3.GetComponentInChildren<TextMeshProUGUI>().text = "" + d.opzioni[numbers[2]];
        btn4.GetComponentInChildren<TextMeshProUGUI>().text = "" + d.opzioni[numbers[3]];
        Debug.Log("score: "+ points);
    }

    public void bttn1()
    {
        if (btn1.GetComponentInChildren<TextMeshProUGUI>().text == target.poeta)
        {
            points++;
        }
        else{
            points--;
        }
        init();
    }
    public void bttn2()
    {
        if (btn2.GetComponentInChildren<TextMeshProUGUI>().text == target.poeta)
        {
            points++;
        }
        else{
            points--;
        }
        init();
    }
    public void bttn3()
    {
        if (btn3.GetComponentInChildren<TextMeshProUGUI>().text == target.poeta)
        {
            points++;
        }
        else{
            points--;
        }
        init();
    }
    public void bttn4()
    {
        if (btn4.GetComponentInChildren<TextMeshProUGUI>().text == target.poeta)
        {
            points++;
        }
        else{
            points--;
        }
        init();
    }
}
