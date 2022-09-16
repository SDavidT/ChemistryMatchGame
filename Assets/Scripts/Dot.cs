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
    private Board board;
    public int targetX;
    public int targetY;
    private Vector2 tempPosition;
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
        
        board=FindObjectOfType<Board>();
        targetX=(int)transform.position.x;
        targetY=(int)transform.position.y;
        row=targetY;
        column=targetX;
    }

    // Update is called once per frame
    void Update()
    {
        targetX=column;
        targetY=row; 

        if(Mathf.Abs(targetX-transform.position.x)>.1){
            tempPosition=new Vector2 (targetX, transform.position.y);
            transform.position=Vector2.Lerp(transform.position, tempPosition, .4f);
        }else{
            tempPosition=new Vector2 (targetX,transform.position.y);
            transform.position= tempPosition;

            board.allDots[column,row]=this.gameObject;

        }
        if(Mathf.Abs(targetY-transform.position.y)>.1){
            tempPosition=new Vector2 (transform.position.x,targetY);
            transform.position=Vector2.Lerp(transform.position, tempPosition, .4f);

        }else{
            tempPosition=new Vector2 (transform.position.x,targetY);
            transform.position= tempPosition;
            board.allDots[column,row]=this.gameObject;
            
        } 
    }


    // FUNCIONES PROPIAS 


    //04 Calculo de angulos para detectar movimientos 

     private void OnMouseDown(){
        
            firstTouchPosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp(){

            finalTouchPosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
    }

    void CalculateAngle(){
        if (Mathf.Abs(finalTouchPosition.y-firstTouchPosition.y)>swipeResiste || Mathf.Abs(finalTouchPosition.x-firstTouchPosition.x)>swipeResiste ){
            swipeAngle=Mathf.Atan2(finalTouchPosition.y-firstTouchPosition.y,finalTouchPosition.x-firstTouchPosition.x)*180/Mathf.PI;
            MovePieces();
        } 
    }

    // 04 ---

    //05 Generar movimientos 
    void MovePieces(){
        if(swipeAngle>-45 && swipeAngle<=45 && column<board.width-1){ // ir a la derecha
            otherDot=board.allDots[column+1,row];
            otherDot.GetComponent<Dot>().column=otherDot.GetComponent<Dot>().column-1;//desplazamiento del punto intercambiado - vecino
            column=column+1;//desplazamiento del punto seleccionado

            // if(otherDot.tag==board.allDots[column-1,row].tag){

            //     if(board.allDots[column-1,row].tag=="O" || board.allDots[column-1,row].tag=="H"){

            //         otherDot.GetComponent<Dot>().isMatched=true;
            //         board.allDots[column-1,row].GetComponent<Dot>().isMatched=true;
            //     } 

            // } else if(otherDot.tag=="O" && board.allDots[column-1,row].tag=="H2"){

            //         otherDot.GetComponent<Dot>().isMatched=true;
            //         board.allDots[column-1,row].GetComponent<Dot>().isMatched=true;
            //     } 

            
        }else if(swipeAngle>45 && swipeAngle<=135 && row< board.height-1){ // ir para arriba
            otherDot=board.allDots[column,row+1];
            otherDot.GetComponent<Dot>().row-=1;
            row+=1;

        } else if ((swipeAngle>135 || swipeAngle<=-135)&&column>0){ // ir a la izquierda
            otherDot=board.allDots[column-1,row];
            otherDot.GetComponent<Dot>().column+=1;
            column-=1;
        }else if(swipeAngle<-45 && swipeAngle>=-135 && row>0){// ir para abajo
            otherDot=board.allDots[column,row-1];
            otherDot.GetComponent<Dot>().row+=1;
            row-=1;
        }

    // 05 ---
    
    }
}
