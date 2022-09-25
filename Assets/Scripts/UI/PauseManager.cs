using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public bool paused = false;
    private Board board;
    public Animator panelPause;
    public Image soundButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    // Start is called before the first frame update
    void Start()
    {
        //pausePanel.SetActive(false);
        //board = GameObject.FindWithTag("Board").GetComponent<Board>();
        board = FindObjectOfType<Board>();
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                soundButton.sprite = musicOffSprite;
            }
            else
            {
                soundButton.sprite = musicOnSprite;
            }
        }
        else
        {
            soundButton.sprite = musicOnSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            //pausePanel.SetActive(true);
            board.currentState = GameState.pause;
        }
        else
        {
            board.currentState = GameState.move;
        }
        // if (!paused)
        // {
        //     //pausePanel.SetActive(false);
        //     board.currentState = GameState.move;
        // }
    }

    public void PauseGame()
    {
        paused = !paused;
    }

    ///PAUSE MENU

    public void PauseIn()
    {
        if (panelPause != null)
        {
            panelPause.SetBool("PauseIn", true);
            panelPause.SetBool("PauseOut", false);
            PauseGame();
        }
    }

    public void PauseOut()
    {
        if (panelPause != null)
        {
            panelPause.SetBool("PauseIn", false);
            panelPause.SetBool("PauseOut", true);
            PauseGame();
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void SoundButton()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                soundButton.sprite = musicOnSprite;
                PlayerPrefs.SetInt("Sound", 1);
            }
            else
            {
                soundButton.sprite = musicOffSprite;
                PlayerPrefs.SetInt("Sound", 0);
            }
        }
        else
        {
            soundButton.sprite = musicOffSprite;
            PlayerPrefs.SetInt("Sound", 1);
        }
    }
}
