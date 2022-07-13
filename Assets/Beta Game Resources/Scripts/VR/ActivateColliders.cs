using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateColliders : MonoBehaviour
{
    private BoxCollider[] _boxChildren;
    private CapsuleCollider[] _capsuleChildren;

    private void Start()
    {
        _boxChildren = GetComponentsInChildren<BoxCollider>();
        _capsuleChildren = GetComponentsInChildren<CapsuleCollider>();

        for (var i = 1; i < _boxChildren.Length; i++)
        {
            _boxChildren[i].enabled = false;
        }

        for (var j = 0; j < _capsuleChildren.Length; j++)
        {
            _capsuleChildren[j].enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for(var i = 1; i < _boxChildren.Length; i++)
            {
                _boxChildren[i].enabled = true;
            }

            for (var j = 0;j < _capsuleChildren.Length; j++)
            {
                _capsuleChildren[j].enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            for (var i = 1; i < _boxChildren.Length; i++)
            {
                _boxChildren[i].enabled = false;
            }

            for (var j = 0; j < _capsuleChildren.Length; j++)
            {
                _capsuleChildren[j].enabled = false;
            }
        }
    }
}
