using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.SceneManagement;



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
    public GameObject youWinGame;
    public GameObject tryAgainGame;
    public GameObject txtMoves;
    public GameObject txtTime;
    public TextMeshProUGUI counter;
    public int currentCounter;
    private float timerSeconds;
    private Board board;
    //public bool nextLevel= false;
    //public ConfirmPanel confirmPanel;


    
    // Start is called before the first frame update
    void Start()
    {
        board=FindObjectOfType<Board>();
        //confirmPanel=FindObjectOfType<ConfirmPanel>();
        SetGameType();
        SetupGame();
    }

    void SetGameType(){
        
        if(board !=null){
            if(board.world.levels[board.level]!=null){

                requirements=board.world.levels[board.level].endGameRequirements;
            }
        }
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

        if(board.currentState!=GameState.pause){

            currentCounter--;
            counter.text=counter.text="" + currentCounter;
            if(currentCounter<=0){
                LoseGame();
                
            } 
        }

    }

    public void WinGame(){

        youWinGame.SetActive(true);
        board.currentState=GameState.win;
        currentCounter=0;
        counter.text=counter.text="" + currentCounter;
        FadePanelController fade= FindObjectOfType<FadePanelController>();
        
        fade.GameOver();
        //nextLevel=true;
    }

    public void LoseGame(){
        tryAgainGame.SetActive(true);
        board.currentState = GameState.lose;
        Debug.Log("YOU LOSE");
        currentCounter=0;
        counter.text=counter.text="" + currentCounter;

        FadePanelController fade= FindObjectOfType<FadePanelController>();
        fade.GameOver();
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
