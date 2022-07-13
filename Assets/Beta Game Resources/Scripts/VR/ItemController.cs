using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ItemController : MonoBehaviour
{
    //[HideInInspector]
    public bool interactedWith;

    private float _timer = 0.2f;
    public bool shouldDomino;
    public bool hasDominoed;

    private Rigidbody _rb;

    private MeshCollider _mc;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (transform.parent!= null)
        {
            if (transform.parent.CompareTag("Destruct"))
                _mc = GetComponent<MeshCollider>();
        }
        
       
        _rb.useGravity = false;
        
    }
    private void Start()
    {
        _rb.constraints = RigidbodyConstraints.FreezeAll;

        if (_mc != null)
        {
            _mc.enabled = false;
        }
       
    }

    private void Update()
    {
        
        if (shouldDomino)
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0)
            {
                hasDominoed = true;
                shouldDomino = false;
                _timer = 0.2f;
            }
        }

        if(shouldDomino || interactedWith)
        {
            _rb.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interractable"))
        {
            if ((interactedWith || shouldDomino) && collision.gameObject.GetComponent<Edible.Edible>()._edibleIndex <= gameObject.GetComponent<Edible.Edible>()._edibleIndex)
            {
                collision.gameObject.GetComponent<ItemController>().shouldDomino = true;
                
                collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }
}
