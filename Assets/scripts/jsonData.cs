using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class jsonData : MonoBehaviour
{
    private string jsonDataString;
    public quest [] arrayData;

    [System.Serializable]
    public class dataClass{
        public quest[] data;
    }
    public class quest{
        public int id;
        public string immagine;
        public string nome;
        public int anno;
    }
    void Start()
    {

        jsonDataString = File.ReadAllText(Application.dataPath + "/json/storia.json");
        dataClass d = JsonConvert.DeserializeObject<dataClass>(jsonDataString);
        arrayData = d.data;
        /*foreach (var item in arrayData)
        {
            Debug.Log(item.id);
            Debug.Log(item.immagine);
            Debug.Log(item.nome);
            Debug.Log(item.anno);
        }*/
        
        
    }
    void Update(){
        
    }

}
