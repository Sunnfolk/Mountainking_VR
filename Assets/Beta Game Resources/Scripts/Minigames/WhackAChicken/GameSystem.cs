using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSystem : MonoBehaviour
{

    public Mole mole;
    public GameObject moleContainer;
    public TextMesh infoText;
    public TextMesh startGameText;
    public VRWhackController whackController;

    public float spawnDuration = 1.5f;
    public float spawnDecrement = 0.1f;
    public float minimumSpawnDuration = 0.5f;
    public float spawnDurationSpeedMode = 0.2f;

    float speedReset;
    public float SpeedUpTime;
    public float SpeedUpSpeed;

    public float gameTimer = 16f;
    public float gamerTimerReset;

    public float resetTimer = 5f;
    float resetTimerReset;

    private Mole[] moles;
    private float spawnTimer = 0f;


    public bool isInGame;
    public bool isGameOver;
    public bool resetTime;
    public bool isStartingUp;


    void Start()
    {
        speedReset = mole.speed;

        infoText = GameObject.Find("InfoText").GetComponent<TextMesh>();
        startGameText = GameObject.Find("StartGameText").GetComponent<TextMesh>();

       

        


    }

    void Update()
    {
        GameOver();

        if (isInGame)
        InGame();

        if (isStartingUp)
            StartUp();








    }

    public void StartGame()
    {
        isInGame = true;
        Debug.Log("In game TIME");



    }

    void InGame()
    {
        
        

            gameTimer -= Time.deltaTime;

            //Mole System
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {

                moles[Random.Range(0, moles.Length)].Rise();


                spawnDuration -= spawnDecrement;

                if (spawnDuration <= minimumSpawnDuration)
                {
                    spawnDuration = minimumSpawnDuration;
                }

                spawnTimer = spawnDuration;
            }

            infoText.text = "Hit all the moles\nTime: " + Mathf.Floor(gameTimer) + "\nScore: " + whackController.score;

            //Speed up
            if (gameTimer <= SpeedUpTime)
            {
                minimumSpawnDuration = spawnDurationSpeedMode;
                mole.speed = SpeedUpSpeed;
            }

            if (gameTimer <= 0f)
            {
                isGameOver = true;
                
            }



        



    }

    void GameOver()
    {
        if (isGameOver)
        {
            isInGame = false;

            //Instantiate Effects

            infoText.text = "Game over! \nYour score: " + Mathf.Floor(whackController.score);


            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0f)
            {
                EndReset();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void StartUp()
    {
        //GetComponents
        moles = moleContainer.GetComponentsInChildren<Mole>();
       
        //Gameobject



        //Timers
        gamerTimerReset = gameTimer;
        resetTimerReset = resetTimer;

        
        

        //Score
        whackController.score = 0;

        //Bools
        isInGame = true;
        resetTime = false;
        isGameOver = false;


        //Stop
        Debug.Log("Starting up NOW");


        isStartingUp = false;

    }

    void EndReset()
    {
        //Reset Before Startup
        resetTime = true;
        infoText.gameObject.SetActive(false);
        startGameText.gameObject.SetActive(true);
        isGameOver = false;

        //Timers Reset
        gameTimer = gamerTimerReset;
        resetTimer = resetTimerReset;
        mole.speed = speedReset;
    }

    

}
//What is needed in VR
/*
 Mole (Collision Trigger)
 Buttons (Start, Restart, Exist_Minigame)
 
    
    
    
 Score Saving (Scoreboard...)
 


 */




