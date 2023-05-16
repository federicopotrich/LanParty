using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatematicaGameManager : MonoBehaviour
{
    public ArrayList matrix;
    public int[] row;
    public int currentPosI = 2;
    public int currentPosJ = 2;
    public int[,] bigTable = new int[5,5];
    public GameObject [] cells;
    public int target;
    public int currentValue;
    public int points = 0;
    public bool substraction = false;

    private TextMeshProUGUI textTarget;  
    private TextMeshProUGUI textCurrent;     

    public Sprite [] numbers;
    // Start is called before the first frame update
    void Start()
    {
        textTarget = GameObject.Find("Target").GetComponent<TextMeshProUGUI>();
        textCurrent = GameObject.Find("Current").GetComponent<TextMeshProUGUI>();
        int [,] matrix = nextBigTable(); 
        textTarget.text = "target: "+target;
        textCurrent.text = "total points "+ points;
    }

    private int[,] nextBigTable() {
        //preparo la tabella con i valori ordinati
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 5; j++) {
                bigTable[i,j] = i+1;
            }
        }
        
        //mescolo la tabella
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 5; j++) {
                int tmpI = Random.Range(0, 4);
                int tmpJ = Random.Range(0, 4);
                int tmp = bigTable[i,j];
                bigTable[i,j] = bigTable[tmpI,tmpJ];
                bigTable[tmpI,tmpJ] = tmp;
            }
        }
        
        int k = 0;
        for(int i = 0; i < 5; i++){
            for(int j = 0; j < 5; j++){       
                cells[k].AddComponent<cell>();
                cells[k].GetComponent<cell>().value = bigTable[i,j];
                cells[k].GetComponent<cell>().row = j;
                cells[k].GetComponent<cell>().col = i;
                cells[k].GetComponent<UnityEngine.UI.Image>().sprite = numbers[bigTable[i, j]-1];
                k++;
            }
        }
        cells[12].GetComponent<UnityEngine.UI.Image>().sprite = numbers[bigTable[2, 2]+9];
        target = Random.Range(0,25);
        textTarget.text = "target: "+ target;
        currentValue = bigTable[currentPosI,currentPosJ];
        return bigTable;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void eventMouseCellListener(GameObject cella){
        int currentCol = cella.GetComponent<cell>().col;
        int currentRow = cella.GetComponent<cell>().row;
        int value = cella.GetComponent<cell>().value;
        Debug.Log(currentCol+" "+currentRow+" "+ value);
        if(currentCol == currentPosI+1 || currentCol == currentPosI-1){
            if(currentRow == currentPosJ){
                currentPosI = currentCol;
                currentPosJ = currentRow;
                if(Input.GetMouseButtonDown(0)){
                    currentValue += value;
                    cella.GetComponent<UnityEngine.UI.Image>().sprite = numbers[value+9];
                }
                else{
                    currentValue -= value;
                    substraction = true;
                    cella.GetComponent<UnityEngine.UI.Image>().sprite = numbers[value+4];
                }
                
            }
        }
        if(currentCol == currentPosI){
            if(currentRow == currentPosJ+1 || currentRow == currentPosJ-1){
                currentPosI = currentCol;
                currentPosJ = currentRow;
                if(Input.GetMouseButtonDown(0)){
                    currentValue += value;
                    cella.GetComponent<UnityEngine.UI.Image>().sprite = numbers[value+9];
                }
                else{
                    currentValue -= value;
                    substraction = true;
                    cella.GetComponent<UnityEngine.UI.Image>().sprite = numbers[value+4];
                }
            }
        }
    }

    public void confirm(){
        Debug.Log(currentPosI+" "+ currentPosJ +" " +currentValue+" " +target);
        if(currentValue == target &&  (currentPosI == 0 || currentPosI == 4 || currentPosJ == 0 || currentPosJ == 4)){
            points++;
            resetTable();
        }
        else
            resetTable();
        textCurrent.text = "total points: "+points;
    }

    public void resetTable(){
        currentPosI = 2;
        currentPosJ = 2;
        nextBigTable();
    }
}


public class cell : MonoBehaviour
{
    public int col;
    public int row;
    public int value;
}