using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{


    private float score;
    public Image scoreBar;
    private TextMeshProUGUI textMesh;
    private Board board;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        textMesh = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        //score+= Time.deltaTime;
        textMesh.text = score.ToString("0");

    }

    public void AddScore(float inputScore)
    {

        score += inputScore;
        textMesh.text = score.ToString("0");

        int Length = board.scoreGoals.Length;

        Debug.Log(score);
        Debug.Log(Length);
        //scoreBar.fillAmount = (float)0.59;
        // Debug.Log(scoreBar.fillAmount);

    }
}
