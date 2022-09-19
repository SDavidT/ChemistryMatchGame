using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{


    public int width;
    public int height;
    public GameObject tilePrefab;
    private BackgroundTile[,] allTiles;
    public GameObject[] dots;
    public GameObject[,] allDots;
    public int offSet;
    // public FindMatches findMatches;
    // public GameObject destroyEffect;
    public Dot currentDot;

    public GameObject[] compuntDot;
    private SoundManager soundManager;



    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        allTiles = new BackgroundTile[width, height];
        allDots = new GameObject[width, height];
        SetUp();
    }


    private void SetUp()
    {

        for (int i = 0; i < width; i++)
        {

            for (int j = 0; j < height; j++)
            {
                // 01 creacion de mosaicos o matriz 
                Vector2 tempPosition = new Vector2(i, j);// representacion de vectores y posicion 2d con ejes X y Y
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject; // clona objetos moviendo la posicion y con rotacion 0
                backgroundTile.transform.parent = this.transform; // se asigna cada objeto al objeto padre
                backgroundTile.name = "( " + i + ", " + j + " )"; // se asigna el nombre a cada objeto
                // 01 --

                // 02 llenar matriz creando puntos aleatorios 
                int dotToUse = Random.Range(0, dots.Length);
                GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                // dot.GetComponent<Dot>().column = i;
                // dot.GetComponent<Dot>().row = j;
                dot.transform.parent = this.transform;
                dot.name = "( " + i + ", " + j + " )";
                //02--

                //03 Genero y alamaceno una matriz de GameObjects de puntos 
                allDots[i, j] = dot;
                //03--


                //int maxIterations=0;

                // // bucle para evitar que se repitan puntos al inciar el juego 
                // while(MatchesAt(i,j,dots[dotToUse])&& maxIterations<100){
                //     dotToUse=Random.Range(0,dots.Length);
                //     maxIterations++;
                // }
                // maxIterations=0;
                //GameObject dot = Instantiate(dots[dotToUse],tempPosition, Quaternion.identity);
                // dot.GetComponent<Dot>().column = i;
                // dot.GetComponent<Dot>().row = j;
                // dot.transform.parent= this.transform;
                // dot.name= "( " + i + ", " + j + " )";
                // allDots[i,j]=dot;


            }

        }

    }

    // 07 eliminar y combinar elementos 

    //Destruye el punto la bandera true
    private void DestroyMatchesAt(int column, int row)
    {
        if (allDots[column, row].GetComponent<Dot>().isMatched)
        {
            //GameObject particle=Instantiate(destroyEffect,allDots[column,row].transform.position, Quaternion.identity); // efecto para destruir puntos
            //Destroy(particle,.5f);
            Destroy(allDots[column, row]);
            allDots[column, row] = null;

            if (soundManager != null)
            { //Activar el sonido
                soundManager.PlayRandomDestroyNoice();
            }
        }
    }


    // recorre la matriz completa y llama a la función de destrucción
    public void DestroyMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allDots[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                }
            }
        }
    }

    // 07 ---


    // 08 Busqueda del componente en la lista de gameobjects de los compuestos
    public int SearchCompunt(string tag)
    {

        for (int i = 0; i < compuntDot.Length; i++)
        {

            if (compuntDot[i].tag == tag)
            {
                return i;
            }
        }
        return 100;
    }

    //09 Descenso de columnas

    public void DecreaseRow()
    {
        StartCoroutine(DecreaseRowCo());
    }

    public IEnumerator DecreaseRowCo()
    {
        int nullCount = 0;
        yield return new WaitForSeconds(.4f); // Tiempo para que aparezca la combinación de elemento 
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allDots[i, j] == null)
                {
                    nullCount++;
                    // if (i == currentDot.column && j == currentDot.row) //evitar que se llene el espacio del componente
                    // {
                    //     nullCount -= 1;
                    // }
                }
                else if (nullCount > 0)
                {
                    //nullCount -= 1;
                    allDots[i, j].GetComponent<Dot>().row -= nullCount;
                    allDots[i, j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
        //RefillBoard();
        StartCoroutine(RefillBoardCo());
    }

    //10 Rellenar el tablero
    private void RefillBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allDots[i, j] == null)
                {
                    Vector2 tempPosition = new Vector2(i, j);
                    int dotToUse = Random.Range(0, dots.Length);
                    GameObject piece = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                    allDots[i, j] = piece;
                    // piece.GetComponent<Dot>().column = i;
                    // piece.GetComponent<Dot>().row = j;
                }
            }
        }
    }

    private IEnumerator RefillBoardCo()
    {
        RefillBoard();
        yield return new WaitForSeconds(.4f);
    }




}
