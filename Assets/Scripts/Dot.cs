using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public enum MoveTo
{
    right,
    left,
    up,
    down,
    empty
}


public class Dot : MonoBehaviour
{

    [Header("Board Variables")]
    public MoveTo moveTo = MoveTo.empty;
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;


    private EndGameManager endGameManager;
    private Board board;
    public GameObject otherDot;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;

    [Header("Swipe Stuff")]
    public float swipeAngle = 0;
    public float swipeResiste = 1f;
    private bool movePiece = false;


    private bool powerDot = false;
    public List<GameObject> currentMatches = new List<GameObject>();
    public float inputScore;
    private ScoreManager score;


    //private FindMatches findMatches;
    // public bool isColumnBomb;
    // public bool isRowBomb;
    // public GameObject rowArrow;
    // public GameObject columnArrow;
    // public bool isColorBomb;
    // public GameObject colorBomb;
    // Start is called before the first frame update
    void Start()
    {
        endGameManager=FindObjectOfType<EndGameManager>();
        board = FindObjectOfType<Board>();
        score = FindObjectOfType<ScoreManager>();
        // targetX = (int)transform.position.x;
        // targetY = (int)transform.position.y;
        // row = targetY;
        // column = targetX;
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
        if(board.currentState == GameState.move){

            if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResiste || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResiste)
            {
                swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
                MovePieces();
                board.currentDot = this;
                powerDot = false;
            }
            else
            {
                powerDot = true;
                board.currentDot = this;
                PowersDots();
            }
        }
    }

    // 04 ---

    //05 Generar movimientos 
    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        { // ir a la derecha
            moveTo = MoveTo.right;
            otherDot = board.allDots[column + 1, row];

            if (otherDot != null)
            {
                previousRow = row;
                previousColumn = column;
                otherDot.GetComponent<Dot>().column -= 1;//desplazamiento del punto intercambiado - vecino
                column = column + 1;//desplazamiento del punto seleccionado
                movePiece = true;
            }

        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        { // ir para arriba
            moveTo = MoveTo.up;
            otherDot = board.allDots[column, row + 1];
            if (otherDot != null)
            {
                previousRow = row;
                previousColumn = column;
                otherDot.GetComponent<Dot>().row -= 1;
                row += 1;
                movePiece = true;
            }

        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        { // ir a la izquierda
            moveTo = MoveTo.left;
            otherDot = board.allDots[column - 1, row];

            if (otherDot != null)
            {
                previousRow = row;
                previousColumn = column;
                otherDot.GetComponent<Dot>().column += 1;
                column -= 1;
                movePiece = true;
            }

        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {// ir para abajo
            moveTo = MoveTo.down;
            otherDot = board.allDots[column, row - 1];

            if (otherDot != null)
            {
                previousRow = row;
                previousColumn = column;
                otherDot.GetComponent<Dot>().row += 1;
                row -= 1;
                movePiece = true;
            }

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
                        board.DestroyMatches();
                        int numberDot = board.SearchCompunt("O2");
                        GameObject powerDot = Instantiate(board.compuntDot[numberDot], board.currentDot.transform.position, Quaternion.identity);
                        board.allDots[(int)board.currentDot.transform.position.x, (int)board.currentDot.transform.position.y] = powerDot;
                        powerDot.GetComponent<Dot>().row = row;
                        powerDot.GetComponent<Dot>().column = column;
                        board.DecreaseRow();
                    }
                    else if (board.currentDot.tag == "H")
                    {
                        otherDot.GetComponent<Dot>().isMatched = true;
                        board.currentDot.GetComponent<Dot>().isMatched = true;
                        board.DestroyMatches();
                        int numberDot = board.SearchCompunt("H2");
                        GameObject powerDot = Instantiate(board.compuntDot[numberDot], board.currentDot.transform.position, Quaternion.identity);
                        board.allDots[(int)board.currentDot.transform.position.x, (int)board.currentDot.transform.position.y] = powerDot;
                        powerDot.GetComponent<Dot>().row = row;
                        powerDot.GetComponent<Dot>().column = column;
                        board.DecreaseRow();
                    }

                }
                else if ((board.currentDot.tag == "H2" && otherDot.tag == "O") || (board.currentDot.tag == "O" && otherDot.tag == "H2"))
                {
                    otherDot.GetComponent<Dot>().isMatched = true;
                    board.currentDot.GetComponent<Dot>().isMatched = true;
                    board.DestroyMatches();
                    int numberDot = board.SearchCompunt("H2O");
                    GameObject powerDot = Instantiate(board.compuntDot[numberDot], board.currentDot.transform.position, Quaternion.identity);
                    board.allDots[(int)board.currentDot.transform.position.x, (int)board.currentDot.transform.position.y] = powerDot;
                    powerDot.GetComponent<Dot>().row = row;
                    powerDot.GetComponent<Dot>().column = column;
                    board.DecreaseRow();

                }
                else if (board.currentDot.tag == "S" && otherDot.tag == "O2")
                {
                    otherDot.GetComponent<Dot>().isMatched = true;
                    board.currentDot.GetComponent<Dot>().isMatched = true;
                    board.DestroyMatches();
                    int numberDot = board.SearchCompunt("SO2");
                    GameObject powerDot = Instantiate(board.compuntDot[numberDot], board.currentDot.transform.position, Quaternion.identity);
                    board.allDots[(int)board.currentDot.transform.position.x, (int)board.currentDot.transform.position.y] = powerDot;
                    powerDot.GetComponent<Dot>().row = row;
                    powerDot.GetComponent<Dot>().column = column;
                    board.DecreaseRow();

                }
                else if (board.currentDot.tag == "H2O" && otherDot.tag == "SO2")
                {
                    otherDot.GetComponent<Dot>().isMatched = true;
                    board.currentDot.GetComponent<Dot>().isMatched = true;
                    board.DestroyMatches();
                    int numberDot = board.SearchCompunt("H2SO3");
                    GameObject powerDot = Instantiate(board.compuntDot[numberDot], board.currentDot.transform.position, Quaternion.identity);
                    board.allDots[(int)board.currentDot.transform.position.x, (int)board.currentDot.transform.position.y] = powerDot;
                    powerDot.GetComponent<Dot>().row = row;
                    powerDot.GetComponent<Dot>().column = column;
                    board.DecreaseRow();

                }
                // else if (board.currentDot.tag == "H2O")
                // {
                //         Debug.Log(board.currentDot.transform.position);
                //         DestroyRow((int)board.currentDot.transform.position.y);
                //         board.DecreaseRow();
                //     // if (moveTo == MoveTo.left || moveTo == MoveTo.right)
                //     // {
                //     //     Debug.Log(board.currentDot.transform.position);
                //     //     DestroyRow((int)board.currentDot.transform.position.y);
                //     //     board.DecreaseRow();
                //     // }
                //     // else if (moveTo == MoveTo.up || moveTo == MoveTo.down)
                //     // {
                //     //     Debug.Log(board.currentDot.transform.position);
                //     //     DestroyColum((int)board.currentDot.transform.position.x);
                //     //     board.DecreaseRow();
                //     // }
                //     //currentMatches.Union(GetColumnPiece((int)transform.position.x));
                // }
                movePiece = false;

                if(endGameManager!=null){

                    if(endGameManager.requirements.gameType==GameType.Moves){
                        endGameManager.DecreaseCounter();
                    }
                }

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
            // }else if (moveTo==MoveTo.left){
            //     Debug.Log("Izquierda");
            // }else if (moveTo==MoveTo.down){
            //     Debug.Log("Abajo");
            // }

        }
    }


    // 07 Revisión de poderes
    public void PowersDots()
    {
        StartCoroutine(PowersDotsCo());
    }


    public IEnumerator PowersDotsCo()
    {
        if (powerDot)
        {
            if (board.currentDot.tag == "O2")
            {
                DestroyRow((int)board.currentDot.transform.position.y);
                board.DecreaseRow();
                powerDot = false;
            }
            else if (board.currentDot.tag == "H2O")
            {
                DestroyColum((int)board.currentDot.transform.position.x);
                board.DecreaseRow();
                powerDot = false;
            }
            else if (board.currentDot.tag == "SO2")
            {
                DestroySquare((int)board.currentDot.transform.position.x, (int)board.currentDot.transform.position.y);
                board.DecreaseRow();
                powerDot = false;
            }
            else if (board.currentDot.tag == "H2SO3")
            {
                DestroyAll();
                board.DecreaseRow();
                powerDot = false;
            }
        }
        yield return new WaitForSeconds(.3f);
    }

    // Destrucción según su poder
    public void DestroyColum(int column)
    {
        for (int i = 0; i < board.height; i++)
        {
            if (board.allDots[column, i] != null)
            {
                board.allDots[column, i].GetComponent<Dot>().isMatched = true;
            }
        }
        board.DestroyMatches();
    }

    public void DestroyRow(int row)
    {
        for (int i = 0; i < board.width; i++)
        {
            if (board.allDots[i, row] != null)
            {
                board.allDots[i, row].GetComponent<Dot>().isMatched = true;
            }
        }
        board.DestroyMatches();
    }

    public void DestroySquare(int column, int row)
    {
        if (column < 1)
        {
            if (row < 1) //Esquina inferor izquierda
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (board.allDots[i, j] != null)
                        {
                            board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                        }
                    }
                }
            }
            else if (row == board.height - 1) //Esquina superior izquierda
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = board.height - 1; j >= (board.height - 2); j--)
                    {
                        if (board.allDots[i, j] != null)
                        {
                            board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                        }
                    }
                }
            }
            else if (row >= 1 && row < board.height - 1)//Borde de columna izquierdo
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = row - 1; j <= row + 1; j++)
                    {
                        if (board.allDots[i, j] != null)
                        {
                            board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                        }
                    }
                }
            }
        }
        else if (column == board.width - 1)
        {
            if (row < 1) //Esquina infero derecha
            {
                for (int i = board.width - 1; i >= board.width - 2; i--)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (board.allDots[i, j] != null)
                        {
                            board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                        }
                    }
                }
            }
            else if (row == board.height - 1) //Esquina superior derecha
            {
                for (int i = board.width - 1; i >= board.width - 2; i--)
                {
                    for (int j = board.height - 1; j >= board.height - 2; j--)
                    {
                        if (board.allDots[i, j] != null)
                        {
                            board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                        }
                    }
                }
            }
            else if (row >= 1 && row < board.height - 1)//Borde de columna derecho
            {
                for (int i = board.width - 1; i >= board.width - 2; i--)
                {
                    for (int j = row - 1; j <= row + 1; j++)
                    {
                        if (board.allDots[i, j] != null)
                        {
                            board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                        }
                    }
                }
            }
        }
        else if (column >= 1 && column < board.width - 1)
        {
            if (row < 1) //Fila del borde inferior
            {
                for (int i = column - 1; i <= column + 1; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (board.allDots[i, j] != null)
                        {
                            board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                        }
                    }
                }
            }
            else if (row == board.height - 1) //Fila del borde superior
            {
                for (int i = column - 1; i <= column + 1; i++)
                {
                    for (int j = board.height - 1; j >= board.height - 2; j--)
                    {
                        if (board.allDots[i, j] != null)
                        {
                            board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                        }
                    }
                }
            }
            else
            { // Filas y columnas alejadas de los bordes
                for (int i = column - 1; i <= column + 1; i++)
                {
                    for (int j = row - 1; j <= row + 1; j++)
                    {
                        if (board.allDots[i, j] != null)
                        {
                            board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                        }
                    }
                }
            }
        }

        board.DestroyMatches();
    }

    public void DestroyAll()
    {
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                if (board.allDots[i, j] != null)
                {
                    board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                }
            }
        }
        board.DestroyMatches();
    }

}
