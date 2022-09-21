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

    
    // Start is called before the first frame update
    void Start()
    {
        SetupGame();
    }

    public void SetupGame(){
        currentCounter=requirements.counterValue;

        if(requirements.gameType==GameType.Moves){

            txtMoves.SetActive(true);
            txtTime.SetActive(false);
            Debug.Log("MOVES");
        } else{

            txtMoves.SetActive(false);
            txtTime.SetActive(true);
            Debug.Log("TIME");
        }

        counter.text="" + currentCounter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
