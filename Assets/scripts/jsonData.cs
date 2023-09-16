using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class jsonData : MonoBehaviour
{
    public TextAsset TheJSONFile;
    private string jsonDataString;
    public quest[] arrayData;

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
        // Utilizza la variabile "jsonString" come desideri

        string filePath = Application.dataPath + "/StreamingAssets/storia.json";
        //Debug.Log(Application.dataPath);
        if (!File.Exists(filePath))
        {
            Debug.Log("ciao file json non trovato");
        }
        dataClass d = JsonConvert.DeserializeObject<dataClass>(File.ReadAllText(filePath));
        Debug.Log(d.data.Length);
        arrayData = d.data;
        /*foreach (var item in arrayData)
        {
            Debug.Log(item.id);
            Debug.Log(item.immagine);
            Debug.Log(item.nome);
            Debug.Log(item.anno);
        }*/
    }

}