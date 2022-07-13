using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodMinigameManager : MonoBehaviour
{
    #region Game Timer
    [SerializeField]
    private float gameTimer = 5;
    private float gamerTimerReset;

    //Bools
    public bool GametimerStart;
    public bool GameIsOver;

    [SerializeField]
    private GameObject[] _prize;

    #endregion

    #region Components 
    //TextMesh
    private TextMesh timerText;
    private Transform prizePoint;

    public List<FoodItem> foodItems;
    #endregion

   
 
    #region Score System

    //ints
    
    public int Score;
    private int nr;
    private bool hasSpawnedPrize;


    [SerializeField]
    private Transform _spawn;

    //Float
    [SerializeField]
    private float sceneChangeDelay = 3;

    [SerializeField]
    private float textDelay = 3;

    //Bools
    [SerializeField]
    private bool finish;




    #endregion

    // Start is called before the first frame update
    void Start()
    {
 
        GetStarted();

    }

    void GetStarted()
    {
        //GetComponents
        timerText = GameObject.Find("FoodTimer").GetComponent<TextMesh>();
        prizePoint = GameObject.Find("PrizePoint").GetComponent<Transform>();
        //Timers
        gamerTimerReset = gameTimer;

        //Bools
        GametimerStart = true;

        //Soundtrack
        

    }

    // Update is called once per frame
    void Update()
    {
        #region Game Timer Stuff
        if (GametimerStart && !GameIsOver)
        {
            gameTimer -= Time.deltaTime;
        }

        if(gameTimer <= 0)
        {
            GameIsOver = true;
            gameTimer = 0;
        }
        #endregion

        TextSystem();

        if (gameTimer <= 0 && !finish)
        {
            EndGame();
            if (!hasSpawnedPrize)
            {
                Instantiate(_prize[nr], _spawn.position, Quaternion.identity);
                hasSpawnedPrize = true;
            }
        }   
    }


    void TextSystem()
    {
        if(!finish)
        timerText.text = "Time: " + Mathf.Floor(gameTimer) + "\nscore: " + Score;
    }

        

    IEnumerator WinnerPrize()
    {
        if (finish)
        {
            timerText.text = "FINISH\nScore: " + Score;

            yield return new WaitForSeconds(textDelay);

            timerText.text = "Grab your prize";

            yield return new WaitForSeconds(sceneChangeDelay);

            //Debug.Log("SceneManager.LoadScene();");
        }
        else
        {
            timerText.text = "You haven't finished\nthe game";

            yield return new WaitForSeconds(sceneChangeDelay);

            //Debug.Log("SceneManager.LoadScene();");
        }
        
    }

    public void EndGame()
    {
        if (Score >= 30)
        { nr = 0; }
        else if (Score >= 15)
        { nr = 1; }
        else if (Score >= 4)
        { nr = 2; }

        StartCoroutine("WinnerPrize");
        Debug.Log("Has Reached Win");
    }
}