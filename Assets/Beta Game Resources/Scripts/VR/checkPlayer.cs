using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPlayer : MonoBehaviour
{
    private MeshCollider[] _allPieces;

    private ItemController[] _allItems;

    private bool _hasItemController;

    private void Start()
    {
        _allPieces = GetComponentsInChildren<MeshCollider>();
        _allItems = GetComponentsInChildren<ItemController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || (other.GetComponent<ItemController>() != null && (other.GetComponent<ItemController>().interactedWith || other.GetComponent<ItemController>().shouldDomino)))
        {
            Debug.Log("Entered");
            int i = 0;

            for (i = 0; i < _allPieces.Length; ++i)
            {
                if (_allPieces[i])
                    _allPieces[i].enabled = true;
            }           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Left");

            for(var i = 0; i < _allItems.Length; i++)
            {
                if (_allItems[i])
                {
                    if (_allItems[i].hasDominoed)
                    {
                        if(Random.Range(0,10) < 4)
                        {
                            Destroy(_allItems[i].gameObject);
                        }
                    }
                    if (!_allItems[i].interactedWith || !_allItems[i].shouldDomino)
                    {
                        if (!_allItems[i].hasDominoed)
                            _allPieces[i].enabled = false;
                    }
                }
            }
        }
    }
}
