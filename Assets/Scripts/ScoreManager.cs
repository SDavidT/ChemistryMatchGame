using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{


    private float score;

    private TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh=GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //score+= Time.deltaTime;
        textMesh.text=score.ToString("0");
        
    }

    public void AddScore(float inputScore){

        score+=inputScore; 
        textMesh.text=score.ToString("0");

    }
}
