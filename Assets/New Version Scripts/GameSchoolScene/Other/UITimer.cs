using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TimerText;
    float time_elapsed;
    float initial_value = 60*20;
    float countdown;
    void Start(){
        
        countdown = initial_value;
    }
    void Update()
    {
        if(countdown>0)
        {
            countdown-=Time.deltaTime;
            time_elapsed=initial_value-countdown;
        }
        float min=Mathf.FloorToInt(countdown/60);
        float sec=Mathf.FloorToInt(countdown%60);

        string time = string.Format("{0,00}:{1,00}",min,sec);
        string [] min_sec = time.Split(":");

        for (int i = 0; i < min_sec.Length; i++)
        {
            if(int.Parse(min_sec[i])<10){
                min_sec[i] = $"0{min_sec[i]}";
            }
        }
        TimerText.text=$"{min_sec[0]}:{min_sec[1]}";
    }

}