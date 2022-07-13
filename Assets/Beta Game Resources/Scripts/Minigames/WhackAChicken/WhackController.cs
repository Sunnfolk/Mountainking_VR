using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackController : MonoBehaviour
{
    public GameSystem gamecontroller;

    public int score = 0;

    public Camera playerCam;
    RaycastHit hit;


    private void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) /*VR Action*/)
        {
            Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    //WHAT HAPPENS ON HIT

                    MoleTrigger();
                    TextTrigger();


                }
            }
        }
    }

    //Is NEEDED for Mole Game to work
    void MoleTrigger()
    {
        if (hit.transform.GetComponent<Mole>() != null)
        {
            Mole mole = hit.transform.GetComponent<Mole>();

            //Stop the mole interaction
            if (gamecontroller.isGameOver == false)
            {
                mole.OnHit();
                score++;
            }



        }
    }

    //Can be delete or Change
    void TextTrigger()
    {
        if (hit.transform.GetComponent<TextSystem>() != null)
        {
            TextSystem textTarget = hit.transform.GetComponent<TextSystem>();

            textTarget.TriggerTime();

        }




    }

}


