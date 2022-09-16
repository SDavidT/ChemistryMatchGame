using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatches : MonoBehaviour
{

    private Board board;
    private Dot nextDot;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void EvaluateMatch()
    {
        StartCoroutine(EvaluateMatchCo());
    }

    private IEnumerator EvaluateMatchCo()
    {
        yield return new WaitForSeconds(.3f);
        nextDot = FindObjectOfType<Dot>();
        Debug.Log("Datos");
        Debug.Log(board.currentDot.column);
        Debug.Log(board.currentDot.row);
    }
}
