using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoriaGameManager : MonoBehaviour
{
    public GameObject [] quesiti;
    public int [] quesitiRisolti = {-1, -1, -1}; // -1 = quesito non svolto | 0 = quesito errato | 1 = quesito corretto
    // Start is called before the first frame update
    int index = 0;
    bool b = true;
    void Awake()
    {
        List<int> l = GenerateRandom(3, 0, GameObject.Find("JsonStoria").GetComponent<jsonData>().arrayData.Length);
        for (int i = 0; i < 3; i++)
        {
            GameObject dataStoria = new GameObject("DataStoria" + (i+1));

            dataStoria.transform.SetParent(this.gameObject.transform);

            dataStoria.AddComponent<storiaData>();
            dataStoria.GetComponent<storiaData>().id = GameObject.Find("JsonStoria").GetComponent<jsonData>().arrayData[l[i]].id;
            dataStoria.GetComponent<storiaData>().nome = GameObject.Find("JsonStoria").GetComponent<jsonData>().arrayData[l[i]].nome;
            dataStoria.GetComponent<storiaData>().anno = GameObject.Find("JsonStoria").GetComponent<jsonData>().arrayData[l[i]].anno;
            dataStoria.GetComponent<storiaData>().immagine = GameObject.Find("JsonStoria").GetComponent<jsonData>().arrayData[l[i]].immagine;
        }
        GameObject[] quesitiNew = new GameObject[]
        {
            this.gameObject.transform.Find("DataStoria1").gameObject,
            this.gameObject.transform.Find("DataStoria2").gameObject,
            this.gameObject.transform.Find("DataStoria3").gameObject
        };
        quesiti = quesitiNew;
        b = true;
        index = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(quesiti != null){
            if(quesiti.Length > 0){

                if(index <=2){
                    GameObject.Find("TestoAnno").GetComponent<TMPro.TextMeshProUGUI>().text = GameObject.Find("Slider").GetComponent<UnityEngine.UI.Slider>().value+" DC";
                    string replacedString = quesiti[index].GetComponent<storiaData>().immagine.Replace(".png", "");
                    replacedString = replacedString.Replace(".jpg", "");
                    GameObject.Find("ImageStoria").GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(replacedString);

                    Debug.Log(quesiti[index].GetComponent<storiaData>().anno);
                }
                if(index == 3 && b){
                    SceneManager.UnloadSceneAsync("StoriaScene");
                    b=false;
                }

            }

            StartCoroutine(DoSomethingDelayed(() =>
            {
                SceneManager.UnloadSceneAsync("StoriaScene");
            }, 30));
        }
    }
    private IEnumerator DoSomethingDelayed(Action action, float t)
    {

        yield return new WaitForSeconds(t);
        action.Invoke();
    }
    public void checkAnswer(){
        if(index <= 2){
            if(quesiti[index].GetComponent<storiaData>().anno == GameObject.Find("Slider").GetComponent<UnityEngine.UI.Slider>().value){
                quesitiRisolti[index] = 1;
            }else{
                quesitiRisolti[index] = 0;
            }
            index++;
        }
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
}