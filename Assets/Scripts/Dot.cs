using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// public enum MoveTo{
//     right,
//     left,
//     up,
//     down,
//     empty

// }


public class Dot : MonoBehaviour
{

    [Header("variables")]
    //public MoveTo moveTo =MoveTo.empty;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public float swipeAngle = 0;
    public int column;
    public int row;
    public GameObject otherDot;
    private Board board;
    public int targetX;
    public int targetY;
    private Vector2 tempPosition;
    public bool isMatched = false;
    // public int previousColumn;
    // public int previousRow;
    public float swipeResiste = 1f;
    //private FindMatches findMatches;
    private bool movePiece = false;


    // public bool isColumnBomb;
    // public bool isRowBomb;
    // public GameObject rowArrow;
    // public GameObject columnArrow;
    // public bool isColorBomb;
    // public GameObject colorBomb;

    // Start is called before the first frame update
    void Start()
    {

        board = FindObjectOfType<Board>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
    }

    // Update is called once per frame
    void Update()
    {
        targetX = column;
        targetY = row;

        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);

            EvaluateMatch();


        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            board.allDots[column, row] = this.gameObject;

        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);

            EvaluateMatch();

        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            board.allDots[column, row] = this.gameObject;

        }
    }


    // FUNCIONES PROPIAS 


    //04 Calculo de angulos para detectar movimientos 

    private void OnMouseDown()
    {

        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {

        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResiste || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResiste)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
            board.currentDot = this;
        }
    }

    // 04 ---

    //05 Generar movimientos 
    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        { // ir a la derecha
            otherDot = board.allDots[column + 1, row];
            otherDot.GetComponent<Dot>().column -= 1;//desplazamiento del punto intercambiado - vecino
            column = column + 1;//desplazamiento del punto seleccionado
            movePiece = true;

        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        { // ir para arriba
            otherDot = board.allDots[column, row + 1];
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
            movePiece = true;

        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        { // ir a la izquierda
            otherDot = board.allDots[column - 1, row];
            otherDot.GetComponent<Dot>().column += 1;
            column -= 1;
            movePiece = true;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {// ir para abajo
            otherDot = board.allDots[column, row - 1];
            otherDot.GetComponent<Dot>().row += 1;
            row -= 1;
            movePiece = true;
        }


    }
    // 05 ---


    // 06 Evaluar las combinaciones posibles de los componentes 
    public void EvaluateMatch()
    {
        StartCoroutine(EvaluateMatchCo());
    }

    private IEnumerator EvaluateMatchCo()
    {


        yield return new WaitForSeconds(.3f);


        if (board.currentDot != null)
        {

            if (movePiece)
            {

                if (otherDot.tag == board.currentDot.tag)
                {

                    if (board.currentDot.tag == "O")
                    {

                        otherDot.GetComponent<Dot>().isMatched = true;
                        board.currentDot.GetComponent<Dot>().isMatched = true;
                        Debug.Log("OO");
                        board.DestroyMatches();
                        int numberDot = board.SearchCompunt("O2");
                        Instantiate(board.compuntDot[numberDot], board.currentDot.transform.position, Quaternion.identity);
                        board.DecreaseRow();


                    }
                    else if (board.currentDot.tag == "H")
                    {

                        otherDot.GetComponent<Dot>().isMatched = true;
                        board.currentDot.GetComponent<Dot>().isMatched = true;

                        Debug.Log("HH");
                        board.DestroyMatches();
                        int numberDot = board.SearchCompunt("H2");
                        Instantiate(board.compuntDot[numberDot], board.currentDot.transform.position, Quaternion.identity);
                        board.DecreaseRow();
                    }

                }
                else if ((board.currentDot.tag == "H2" && otherDot.tag == "O") || (board.currentDot.tag == "O" && otherDot.tag == "H2"))
                {

                    otherDot.GetComponent<Dot>().isMatched = true;
                    board.currentDot.GetComponent<Dot>().isMatched = true;
                    Debug.Log("H2O");
                    board.DestroyMatches();
                    int numberDot = board.SearchCompunt("H2O");
                    Instantiate(board.compuntDot[numberDot], board.currentDot.transform.position, Quaternion.identity);
                    board.DecreaseRow();

                }
                else if (board.currentDot.tag == "S" && otherDot.tag == "O2")
                {

                    otherDot.GetComponent<Dot>().isMatched = true;
                    board.currentDot.GetComponent<Dot>().isMatched = true;
                    Debug.Log("SO2");
                    board.DestroyMatches();
                    int numberDot = board.SearchCompunt("SO2");
                    Instantiate(board.compuntDot[numberDot], board.currentDot.transform.position, Quaternion.identity);
                    board.DecreaseRow();

                }
                else if (board.currentDot.tag == "H2O" && otherDot.tag == "SO2")
                {

                    otherDot.GetComponent<Dot>().isMatched = true;
                    board.currentDot.GetComponent<Dot>().isMatched = true;
                    Debug.Log("H2SO3");
                    board.DestroyMatches();
                    int numberDot = board.SearchCompunt("H2SO3");
                    Instantiate(board.compuntDot[numberDot], board.currentDot.transform.position, Quaternion.identity);
                    board.DecreaseRow();

                }

                movePiece = false;

            }


            // private void RefillBoard()
            // {
            //     for (int i = 0; i < width; i++)
            //     {

            //         for (int j = 0; j < height; j++)
            //         {

            //             if (allDots[i, j] == null)
            //             {
            //                 Vector2 tempPosition = new Vector2(i, j + offSet);
            //                 int dotToUse = Random.Range(0, dots.Length);
            //                 GameObject piece = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
            //                 allDots[i, j] = piece;
            //                 piece.GetComponent<Dot>().column = i;
            //                 piece.GetComponent<Dot>().row = j;
            //             }
            //         }
            //     }
            // }
            //else if(moveTo==MoveTo.up){
            //     Debug.Log("Arriba");
            //         Debug.Log(board.currentDot.column);
            //         Debug.Log(board.currentDot.row);
            //         Debug.Log(board.currentDot.tag);

            // }else if (moveTo==MoveTo.left){
            //     Debug.Log("Izquierda");
            //         Debug.Log(board.currentDot.column);
            //         Debug.Log(board.currentDot.row);
            //         Debug.Log(board.currentDot.tag);


            // }else if (moveTo==MoveTo.down){
            //     Debug.Log("Abajo");
            //         Debug.Log(board.currentDot.column);
            //         Debug.Log(board.currentDot.row);
            //         Debug.Log(board.currentDot.tag);

            // }

        }
    }
}
