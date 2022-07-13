using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractUI : MonoBehaviour
{
    private ItemController _itemController;

    public UnityEvent _SceneChange;

    private void Start()
    {
        _itemController = GetComponent<ItemController>();
    }

    void Update()
    {
        if (_itemController.interactedWith)
        {
            _SceneChange.Invoke();
        }
    }
}
