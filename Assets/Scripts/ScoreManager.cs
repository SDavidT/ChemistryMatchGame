using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{


    private float score;
    private TextMeshProUGUI textMesh;
    private Board board;
    public Image scoreBar;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        textMesh = GetComponent<TextMeshProUGUI>();
        UpdateBar();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = score.ToString("0");
    }

    public void AddScore(float inputScore)
    {
        score += inputScore;
        textMesh.text = score.ToString("0");
        UpdateBar();
    }

    public void UpdateBar()
    {
        if (board != null && scoreBar != null)
        {
            int length = board.scoreGoals.Length;
            Debug.Log((float)score / (float)board.scoreGoals[length - 1]);
            scoreBar.fillAmount = (float)score / (float)board.scoreGoals[length - 1];
        }

    }
}
