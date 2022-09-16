using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Dot : MonoBehaviour
{

    [Header("variables")]
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public float swipeAngle=0;
    public int column;
    public int row;
    public GameObject otherDot;
    // private Board board;
    // public int targetX;
    // public int targetY;
    // private Vector2 tempPosition;
    // public bool isMatched=false;
    // public int previousColumn;
    // public int previousRow;
    public float swipeResiste=1f;
    // private FindMatches findMatches;
    // public bool isColumnBomb;
    // public bool isRowBomb;
    // public GameObject rowArrow;
    // public GameObject columnArrow;
    // public bool isColorBomb;
    // public GameObject colorBomb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // FUNCIONES PROPIAS 


    //04 Calculo de angulos para detectar movimientos 

     private void OnMouseDown(){
        
            firstTouchPosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // if(board.currentState==GameState.move){
        // }

    }

    private void OnMouseUp(){

            finalTouchPosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        // if (board.currentState==GameState.move){

        // }
    }

    void CalculateAngle(){
        if (Mathf.Abs(finalTouchPosition.y-firstTouchPosition.y)>swipeResiste || Mathf.Abs(finalTouchPosition.x-firstTouchPosition.x)>swipeResiste ){
            swipeAngle=Mathf.Atan2(finalTouchPosition.y-firstTouchPosition.y,finalTouchPosition.x-firstTouchPosition.x)*180/Mathf.PI;
            //MovePieces();
            //board.currentState=GameState.wait;
            //board.currentDot = this;
        } else {
            //board.currentState=GameState.move;
        }
    }







}
