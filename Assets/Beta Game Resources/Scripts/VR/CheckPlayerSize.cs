using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerSize : MonoBehaviour
{

    public Score _score;
    public int _playerStateNon;

    public Transform[] _objects;
    // Start is called before the first frame update
    void Start()
    {
        _objects = GetComponentsInChildren<Transform>();
        _score.PlayerState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_score.PlayerState >= _playerStateNon)
        {
            foreach(Transform tran in _objects)
            {
                tran.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Transform tran in _objects)
            {
                tran.gameObject.SetActive(true);
            }
        }
            

    }
}
