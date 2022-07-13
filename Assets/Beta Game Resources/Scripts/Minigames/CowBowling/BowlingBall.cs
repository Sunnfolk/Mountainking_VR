using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _startRot;
    private Vector3 _startScale;

   
    private Rigidbody _rigid;
    private ItemController _itemcont;

    public bool respawn = false;
    public int balls;

    public bool gameOver;

    public bool giveForce;
    public bool quickstart;

    void Start()
    {
        _startPos = transform.position;
        _startRot = transform.localEulerAngles;
        _startScale = transform.localScale;

        _rigid = GetComponent<Rigidbody>();
        _itemcont = GetComponent<ItemController>();
    }

    private void Update()
    {
        if (quickstart)
        {
            _rigid.constraints = RigidbodyConstraints.None;
            quickstart = false;
        }
        
        if (giveForce)
        {
            _rigid.AddForce(Vector3.forward*60000);
            giveForce = false;
        }

        if (!gameOver)
        {
            if (respawn)
            {
                transform.position = _startPos;
                transform.localEulerAngles = _startRot;
                transform.localScale = _startScale;
                _itemcont.interactedWith = false;
                _rigid.useGravity = false;
                _rigid.constraints = RigidbodyConstraints.FreezeAll;
                balls++;
                respawn = false;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BowlingPinContainer"))
        {
            respawn = true;
        }
    }
}
