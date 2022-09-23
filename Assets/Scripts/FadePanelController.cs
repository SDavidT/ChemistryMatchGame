using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[System.Serializable]
public class numberLevel
{
    public int intlevel;

}
public class FadePanelController : MonoBehaviour
{
    // Start is called before the first frame update
    public numberLevel numberLevel;
    public Animator panelAnim;
    public Animator gameInfoAnim;
    public TextMeshProUGUI level;
    public Board board;
    int prueba;

    void Start()
    {
        board = FindObjectOfType<Board>();
        prueba = board.level + 1;
        level.text = "LEVEL" + " " + prueba;
    }
    public void OK()
    {

        if (panelAnim != null && gameInfoAnim != null)
        {

            panelAnim.SetBool("Out", true);
            gameInfoAnim.SetBool("Out", true);
            StartCoroutine(GameStartCo());

        }
    }

    public void GameOver()
    {

        panelAnim.SetBool("Out", true);
        panelAnim.SetBool("Game Over", true);


    }

    IEnumerator GameStartCo()
    {
        yield return new WaitForSeconds(1f);

        Board board = FindObjectOfType<Board>();
        board.currentState = GameState.move;
    }
}
