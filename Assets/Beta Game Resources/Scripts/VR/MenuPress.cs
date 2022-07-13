using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MenuPress : MonoBehaviour
{
    [SerializeField]
    private Canvas _menu;

    private Hand _thisHand;

    private bool _activeMenu;

    private void Awake()
    {
        _thisHand = GetComponent<Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_thisHand.menuPressAction.GetStateDown(_thisHand.handType))
        {
            if (!_activeMenu)
            {
                _activeMenu = true;
                _menu.gameObject.SetActive(true);
                Debug.Log("Turn on menu");
            }
            else
            {
                _activeMenu = false;
                _menu.gameObject.SetActive(false);
                Debug.Log("Turn off menu");
            }
        }
    }
}
