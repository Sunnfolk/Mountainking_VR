using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRWhackController : MonoBehaviour
{
    

    public GameSystem gameController;
    public TextSystem textSystem;
       
    public int score = 0;
    

    void Start()
    {
        gameController = FindObjectOfType<GameSystem>();
    }

    void FixedUpdate()
    {
        Shortcut();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Mole")
        {

            //Get POINT HERE
            if (collision.gameObject.GetComponent<Mole>() != null)
            {
                Debug.Log("Mole Hit");
                Mole mole = collision.transform.GetComponent<Mole>();

                if (gameController.isGameOver == false)
                {
                    Debug.Log("GIVE ME POINTS!");
                    mole.OnHit();
                    score++;
                }
            }
        }

        //Text Activate
        if (collision.gameObject.tag == "Interractable")
        {



            if (collision.gameObject.GetComponent<TextSystem>() != null)
            {
                Debug.Log("Text Hit");
                TextSystem textTarget = collision.transform.GetComponent<TextSystem>();

                textTarget.TriggerTime();

            }
        }

        Debug.Log("You hit " + collision.gameObject.name);

        
            
    }

    void Shortcut()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            textSystem.TriggerTime();   
            Debug.Log("Start game");

        }



    }

    /*void ResetTime()
    {
        if (gameController.resetTime)
            score = 0;
    }*/

    

    





}



