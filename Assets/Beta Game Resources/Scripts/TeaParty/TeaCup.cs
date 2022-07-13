using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaCup : MonoBehaviour
{

    private bool Tea;
    [SerializeField]
    private Sprite FullCup;
    [SerializeField]
    private Sprite EmptyCup;

    private SpriteRenderer Rend;

    // Start is called before the first frame update
    void Start()
    {
        Rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Tea)
            Rend.sprite = EmptyCup;
        else if (Tea)
            Rend.sprite = FullCup;
            


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KettleFull")
        {
            Debug.Log("HIT");
            gameObject.tag = "TeaCupFull";
            Tea = true;
        }
    }

    /*3private IEnumerator TeaTime()
    {


        gameObject.tag = "TeaCupFull";
        yield return new WaitForSeconds(Timer);
        gameObject.tag = "TeaCupEmpty";
        //Tag = cup full
        //Text Show THANK YOU FOR TEA
        //Wait 2 sec, 
        //text hide
        //Wait Time
        //tag = cup empty
        //Text Show NEED MORE TEA
        //2 sec
        //text hide
        

        yield return new WaitForSeconds(3);

    }*/
}
