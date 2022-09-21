using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
