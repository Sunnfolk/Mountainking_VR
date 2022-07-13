using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurchAudioManager : MonoBehaviour
{
    private bool _hasPlayedIntro;
    private bool _hasPlayedDestroy;

    private ItemController[] _items;

    // Start is called before the first frame update
    void Start()
    {
        _items = GetComponentsInChildren<ItemController>();
    }

    private void Update()
    {
        if (_hasPlayedDestroy) Destroy(this);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_hasPlayedIntro)
            {
                Narrator.instance.PlayEvent(Narrator.instance.Church_Spotted);
                _hasPlayedIntro = true;
            }
            if (_hasPlayedIntro)
            {
                for(var i = 0; i < _items.Length; i++)
                {
                    if(_items[i].interactedWith || _items[i].shouldDomino)
                        if (!_hasPlayedDestroy)
                        {
                            Narrator.instance.PlayEvent(Narrator.instance.Church_Destroyed);
                            _hasPlayedDestroy = true;
                        } 
                }
            }
        }
    }
}
