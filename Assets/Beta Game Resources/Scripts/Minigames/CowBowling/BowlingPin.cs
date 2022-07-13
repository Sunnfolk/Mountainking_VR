using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPin : MonoBehaviour
{
    public float dinstanceToSee;

    private Vector3 _startPos;
    private Vector3 _startRot;
    private Vector3 _startScale;

    public bool hasFallen;
    public bool hasGivenScore;

    Ray ray;
    RaycastHit whatIHit;

    private CowBowlingManager _manager;
    private Rigidbody _rigid;

    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _manager = GetComponentInParent<CowBowlingManager>();

        _startPos = transform.position;
        _startRot = transform.localEulerAngles;
        _startScale = transform.localScale;
    }

    void Update()
    {
        if (!hasFallen)
        {
            hitRegistering();
        }  
    }

    void hitRegistering()
    {
        //Displays a ray with color red
        Debug.DrawRay(this.transform.position, this.transform.up * dinstanceToSee, Color.red);

        //Ray powers
        if (Physics.Raycast(this.transform.position, this.transform.up, out whatIHit, dinstanceToSee))
        {
            if (whatIHit.collider.tag == "NoHitRegister")
            {
                hasFallen = false;
            }
            else
            {
                hasFallen = true;
                //_manager._bowlingPins.Remove(this);
            }
        }
        else
        {
            hasFallen = true;
            //_manager._bowlingPins.Remove(this);
        }
    }

    public void CowReset()
    {
        _rigid.isKinematic = true;
        transform.position = _startPos;
        transform.localEulerAngles = _startRot;
        transform.localScale = _startScale;
        _rigid.isKinematic = false;
    
        hasFallen = false;
    }
}
