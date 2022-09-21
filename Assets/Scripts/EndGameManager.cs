using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public enum GameType{
    Moves,
    Time
}

[System.Serializable]
public class EndGameRequirements{
    public GameType gameType;
    public int counterValue;
    
}

public class EndGameManager : MonoBehaviour
{
    public EndGameRequirements requirements;
    public GameObject txtMoves;
    public GameObject txtTime;
    public TextMeshProUGUI counter;
    public int currentCounter;
    private float timerSeconds;
    private Board board;

    
    // Start is called before the first frame update
    void Start()
    {
        board=FindObjectOfType<Board>();
        SetupGame();
    }

    void SetupGame(){
        currentCounter=requirements.counterValue;

        if(requirements.gameType==GameType.Moves){

            txtMoves.SetActive(true);
            txtTime.SetActive(false);
            Debug.Log("MOVES");
        } else{
            timerSeconds=1;
            txtMoves.SetActive(false);
            txtTime.SetActive(true);
            Debug.Log("TIME");
        }

        counter.text="" + currentCounter;
    }

    public void DecreaseCounter(){

        currentCounter--;
        counter.text=counter.text="" + currentCounter;
        if(currentCounter<=0){
            Debug.Log("YOU LOSE");
            currentCounter=0;
            counter.text=counter.text="" + currentCounter;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(requirements.gameType==GameType.Time && currentCounter>0){

            timerSeconds-=Time.deltaTime;

            if(timerSeconds<=0){
                DecreaseCounter();
                timerSeconds=1;
            }

        }
        
    }
}
