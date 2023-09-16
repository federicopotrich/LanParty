using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BtnSceneManager : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Button").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{
            PlayerPrefs.SetString("playerName", GameObject.Find("InputField").GetComponent<TMPro.TMP_InputField>().text);
            PlayerPrefs.SetString("playerTeam", GameObject.Find("Dropdown").GetComponent<TMPro.TMP_Dropdown>().options[GameObject.Find("Dropdown").GetComponent<TMPro.TMP_Dropdown>().value].text);
            //Debug.Log($"{PlayerPrefs.GetString("playerName")} -> {PlayerPrefs.GetString("playerTeam")}");
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameSchoolScene");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
