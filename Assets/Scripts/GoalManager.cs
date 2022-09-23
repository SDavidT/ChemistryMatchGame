using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BlankGoal
{

    public int numberNeeded;
    public int numberCollected;
    public Sprite goalSprite;
    public string matchValue;

}
public class GoalManager : MonoBehaviour
{
    public BlankGoal[] levelGoals;
    public GameObject goalPrefab;
    public GameObject goalIntroParent;
    public GameObject goalGameParent;
    public List<GoalPanel> currentGoals = new List<GoalPanel>();
    private EndGameManager endGame;
    private Board board;


    // Start is called before the first frame update
    void Start()
    {

        board = FindObjectOfType<Board>();
        endGame = FindObjectOfType<EndGameManager>();
        GetGoals();
        ResetNumberCollected();
        SetupGoals();
    }

    void GetGoals()
    {
        if (board != null)
        {
            if (board.world != null)
            {
                if (board.world.levels[board.level] != null)
                {
                    levelGoals = board.world.levels[board.level].levelGoals;
                }
            }
        }
    }

    void SetupGoals()
    {
        for (int i = 0; i < levelGoals.Length; i++)
        {

            GameObject goal = Instantiate(goalPrefab, goalIntroParent.transform.position, Quaternion.identity);
            goal.transform.SetParent(goalIntroParent.transform);
            GoalPanel panel = goal.GetComponent<GoalPanel>();
            panel.thisSprite = levelGoals[i].goalSprite;
            panel.thisString = levelGoals[i].numberNeeded.ToString();


            GameObject gameGoal = Instantiate(goalPrefab, goalGameParent.transform.position, Quaternion.identity);
            gameGoal.transform.SetParent(goalGameParent.transform);
            panel = gameGoal.GetComponent<GoalPanel>();
            currentGoals.Add(panel);
            panel.thisSprite = levelGoals[i].goalSprite;
            panel.thisString = "0/" + levelGoals[i].numberNeeded;

        }
    }

    public void UpdateGoals()
    {

        int goalsCompleted = 0;

        for (int i = 0; i < levelGoals.Length; i++)
        {
            currentGoals[i].thisText.text = "" + levelGoals[i].numberCollected + "/" + levelGoals[i].numberNeeded;


            if (levelGoals[i].numberCollected >= levelGoals[i].numberNeeded)
            {
                goalsCompleted++;
                currentGoals[i].thisText.text = "" + levelGoals[i].numberNeeded + "/" + levelGoals[i].numberNeeded;
            }
        }

        if (goalsCompleted >= levelGoals.Length)
        {
            Debug.Log(goalsCompleted);
            Debug.Log(levelGoals.Length);
            if (endGame != null)
            {
                endGame.WinGame();
                Debug.Log("YOU WIN");
                ResetNumberCollected();
            }
        }
    }

    public void CompareGoal(string goalToCompare)
    {
        for (int i = 0; i < levelGoals.Length; i++)
        {
            if (goalToCompare == levelGoals[i].matchValue)
            {

                levelGoals[i].numberCollected++;
            }
        }
    }

    public void ResetNumberCollected()
    {
        for (int i = 0; i < levelGoals.Length; i++)
        {
            levelGoals[i].numberCollected = 0;
        }
    }
}
