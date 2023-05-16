using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoriaGameManager : MonoBehaviour
{
    public GameObject [] quesiti;
    public int [] quesitiRisolti = {-1, -1, -1}; // -1 = quesito non svolto | 0 = quesito errato | 1 = quesito corretto
    // Start is called before the first frame update
    int index = 0;
    bool b = true;
    void Start()
    {
        b = true;
        index = 0;
        quesiti = GameObject.FindGameObjectsWithTag("DataStoria");
    }
    // Update is called once per frame
    void Update()
    {
        if(index <=2){
            GameObject.Find("TestoAnno").GetComponent<TMPro.TextMeshProUGUI>().text = GameObject.Find("Slider").GetComponent<UnityEngine.UI.Slider>().value+" DC";
            string replacedString = quesiti[index].GetComponent<storiaData>().immagine.Replace(".png", "");
            replacedString = replacedString.Replace(".jpg", "");
            GameObject.Find("ImageStoria").GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(replacedString);

            Debug.Log(quesiti[index].GetComponent<storiaData>().anno);
        }
        if(index == 3 && b){
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameSchoolScene");
            b=false;
        }
    }
    void tmp(){
        GameObject.Find("SceneManager").GetComponent<SceneManagerScript>().changeScene("GameSchoolScene");
        CancelInvoke();
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
}