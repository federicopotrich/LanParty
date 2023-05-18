using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;


public class MusicSystem : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject redSquare;
    public GameObject blueSquare;
    public GameObject yellowSquare;
    public GameObject greenSquare;
    [Header("Spawners")]
    public GameObject redSpawner;
    public GameObject blueSpawner;
    public GameObject yellowSpawner;
    public GameObject greenSpawner;

    [Header("Note Prefab")]
    public GameObject note;

    [Header("Others")]
    private Song[] songs;
    float timer;
    public AudioSource music;
    public AudioClip [] clips;
    public TextMeshProUGUI ScoreText;
    private int index, score=0;
    public FailTriggerScript failTrigger;
    public TextMeshProUGUI[] AnswerButtons;
    public GameObject AnswerSheet; 
    private string SongName;
    string [] songNames=new string[9];
    private int difficulty;

    private bool spawning;
    
    // Start is called before the first frame update
    void Start()
    {
        string file = File.ReadAllText("./Assets/Script/canzoni.json");
        file = file.Replace("[{","");
        file = file.Replace("}]","");
        string[] files = file.Split("},");
        songs= new Song[files.Length];
        spawning = false;

        GameObject floor = GameObject.Find("Floor");
        difficulty=0;
        switch(int.Parse(floor.tag)){
            case 0: difficulty=0;break;
            case 1: difficulty=0;break;
            case 2: difficulty=1;break;
            case 3: difficulty=2;break;
        }

        for (int i = 0; i < files.Length; i++)
        {            
            files[i] = files[i].Replace("\",","\",:");
            files[i] = files[i].Replace("],","],:");
            string[] arrayFile = files[i].Split(":");
            songNames[i] = arrayFile[1].Replace("\"","");
            songNames[i] = songNames[i].Replace(",","");
            songs[i] = new Song(StringToArray(arrayFile[3]),StringToArray(arrayFile[5]),StringToArray(arrayFile[7]),StringToArray(arrayFile[9]));
        }
        Invoke("PlayMusic",2.549998f);
        index = (int) Random.Range(0,3)+3*difficulty;
        Notes(songs[index]);
        SongName=songNames[index];
        SongName=SongName.Trim();
        ScoreText.text = "Score: "+score;
    }

    public void PlayMusic(){
        music.clip = clips[index];
        music.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
        //Red Square
        if(Input.GetKey(KeyCode.A)){
            redSquare.GetComponent<Image>().color = Color.red;
            redSquare.GetComponent<MusicButton>().TurnOnOff(true);
        }
        else{
            redSquare.GetComponent<Image>().color = Color.white;
            redSquare.GetComponent<MusicButton>().TurnOnOff(false);
        }
        //Blue Square
        if(Input.GetKey(KeyCode.W)){
            blueSquare.GetComponent<Image>().color = Color.blue;
            blueSquare.GetComponent<MusicButton>().TurnOnOff(true);
        }
        else{
            blueSquare.GetComponent<Image>().color = Color.white;
            blueSquare.GetComponent<MusicButton>().TurnOnOff(false);
        }
        //Yellow Square
        if(Input.GetKey(KeyCode.S)){
            yellowSquare.GetComponent<Image>().color = Color.yellow;
            yellowSquare.GetComponent<MusicButton>().TurnOnOff(true);
        }
        else{
            yellowSquare.GetComponent<Image>().color = Color.white;
            yellowSquare.GetComponent<MusicButton>().TurnOnOff(false);
        }
        //Green Square
        if(Input.GetKey(KeyCode.D)){
            greenSquare.GetComponent<Image>().color = Color.green;
            greenSquare.GetComponent<MusicButton>().TurnOnOff(true);
        }
        else{
            greenSquare.GetComponent<Image>().color = Color.white;
            greenSquare.GetComponent<MusicButton>().TurnOnOff(false);
        }


        if(greenSquare.GetComponent<MusicButton>().hit || blueSquare.GetComponent<MusicButton>().hit || yellowSquare.GetComponent<MusicButton>().hit || redSquare.GetComponent<MusicButton>().hit){
            IncrementPoints();
        }

        if(failTrigger.Touch){
            DecrementPoints();
        }

        if(GameObject.Find("note(Clone)")==null&&spawning)
            FinalQuestionStarter();
    }

    private float[] StringToArray(string rekt){
        rekt = rekt.Replace("[","");
        rekt = rekt.Replace("],","");
        rekt = rekt.Replace("]","");
        rekt = rekt.Replace("}","");
        string[] cifre = rekt.Split(",");
        float[] result= new float[cifre.Length];
        for (int i = 0; i < cifre.Length; i++)
        {
            result[i]= float.Parse(cifre[i].Replace(".",","));
        }
        return result;
    }

    public void Notes(Song song){
        int noteNumber = 1000;
        for (int i = 0; i <= noteNumber; i++)
        {
            if(song.red.Length>i)
                Invoke("SpawnRedNote",song.red[i]);
            if(song.blue.Length>i)
                Invoke("SpawnBlueNote",song.blue[i]);
            if(song.yellow.Length>i)
                Invoke("SpawnYellowNote",song.yellow[i]);
            if(song.green.Length>i)
                Invoke("SpawnGreenNote",song.green[i]);
        }
    }

    public void SpawnRedNote(){
        spawning=true;
        GameObject noteTmp = Instantiate(note, redSpawner.transform.position, Quaternion.identity);
        noteTmp.GetComponent<Image>().color = Color.red;
        noteTmp.transform.SetParent(redSpawner.transform, false);
    }

    public void SpawnBlueNote(){
        spawning=true;
        GameObject noteTmp = Instantiate(note, blueSpawner.transform.position, Quaternion.identity);
        noteTmp.GetComponent<Image>().color = Color.blue;
        noteTmp.transform.SetParent(blueSpawner.transform, false);
    }

    public void SpawnYellowNote(){
        spawning=true;
        GameObject noteTmp = Instantiate(note, yellowSpawner.transform.position, Quaternion.identity);
        noteTmp.GetComponent<Image>().color = Color.yellow;
        noteTmp.transform.SetParent(yellowSpawner.transform, false);
    }

    public void SpawnGreenNote(){
        spawning=true;
        GameObject noteTmp = Instantiate(note, greenSpawner.transform.position, Quaternion.identity);
        noteTmp.GetComponent<Image>().color = Color.green;
        noteTmp.transform.SetParent(greenSpawner.transform, false);
    }

    void FixedUpdate(){
        timer+=1*Time.deltaTime;
        ScoreText.gameObject.GetComponent<Animator>().SetBool("Hit",false);
        ScoreText.gameObject.GetComponent<Animator>().SetBool("Failed",false);
    }

    void IncrementPoints(){
        ScoreText.gameObject.GetComponent<Animator>().SetBool("Hit",true);    
        score += 25;
        ScoreText.text = "Score: "+score;
    }

    void DecrementPoints(){
        ScoreText.gameObject.GetComponent<Animator>().SetBool("Failed",true);
        score-=100;
        if(score<=0)
            score=0;
        ScoreText.text = "Score: "+score;
    }

    void FinalQuestionStarter(){
        spawning=false;
        AnswerSheet.SetActive(true);
        switch(difficulty){
            case 0:
                for(int i=0;i<AnswerButtons.Length;i++){
                    AnswerButtons[i].text = songNames[i];
                }
                break;
            case 1: 
                Medium();
                break;
            case 2: 
                Hard();
                break;
        }
    }

    void Medium(){
        int index=(int) Random.Range(0,AnswerButtons.Length-1);
        AnswerButtons[index].text = SongName;
        string[] takensongs = new string[8];
        for(int i=0;i<AnswerButtons.Length;i++){
            if(i!=index){
                string tmp;
                bool canzonedisponibile;
                do{
                    canzonedisponibile=true;
                    tmp = songNames[(int) Random.Range(0,9)];
                    for (int j = 0; j < takensongs.Length; j++)
                    {
                        if(tmp==takensongs[j]){
                            canzonedisponibile=false;
                        }
                    }   
                }while(tmp==SongName||!canzonedisponibile);
                takensongs[i]=tmp;
                AnswerButtons[i].gameObject.GetComponent<TextMeshProUGUI>().text = tmp;
            }
        }
    }

    void Hard(){
        string[] answers = FakeNames();
        Debug.Log(SongName);
        answers = Reshuffle(answers);
        for(int i=0;i<AnswerButtons.Length;i++){
            if(i!=index){
               AnswerButtons[i].text = answers[i];
            }
        }
    }

    string[] FakeNames(){

        string[] sultans = {"Sultans Of Swing","Money for Nothing", "So far Away"};
        string[] diggy = {"Diggy Diggy Hole","I am a Dwarf", "I'm digging a hole"};
        string[] buggy = {"Dune Buggy","Race Off", "I feel like a king"};

        switch (SongName)
        {
            case string a when a.Contains("Sultans Of Swing"): 
                return sultans;
            case string a when a.Contains("Diggy Diggy Hole"): 
                return diggy;
            case string a when a.Contains("Dune Buggy"): 
                return buggy;
            default: return null;
        }
    }

    string[] Reshuffle(string[] texts)
    {
        for (int t = 0; t < texts.Length; t++ )
        {
            string tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
        return texts;
    }
    public void Answer(int index){
        string answer=AnswerButtons[index].text.Trim();
        Debug.Log(answer);
        if(answer==SongName)
            Debug.Log("Risposta Corretta");
        else
           Debug.Log("Risposta Errata"); 
    }
}

public class Song {
    public float[] red;
    public float[] blue;
    public float[] yellow;
    public float[] green;

    public Song(float[] red,float[] blue,float[] yellow,float[] green){
        this.red = red;
        this.blue = blue;
        this.yellow = yellow;
        this.green = green;
    }

}

