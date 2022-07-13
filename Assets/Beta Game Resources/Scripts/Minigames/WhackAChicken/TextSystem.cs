using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSystem : MonoBehaviour
{
    public GameObject infoText;
    public UnityEngine.Events.UnityEvent triggerOnHit;

    void Awake()
    {
        infoText = GameObject.Find("InfoText");
    }
    public void TriggerTime()
    {
        triggerOnHit.Invoke();

        gameObject.SetActive(false);
        infoText.SetActive(true);
    }

}
