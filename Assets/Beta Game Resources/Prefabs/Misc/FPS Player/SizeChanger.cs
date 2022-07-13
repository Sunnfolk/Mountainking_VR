using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChanger : MonoBehaviour
{
    [SerializeField]
    private float[] _size =
    {
        0.3f,
        1f,
        4f,
        9f,
        12f
    };

    [SerializeField]
    private int _state;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _state = 0;
            transform.localScale = new Vector3(_size[_state],_size[_state],_size[_state]);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            _state = 1;
            transform.localScale = new Vector3(_size[_state], _size[_state], _size[_state]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _state = 2;
            transform.localScale = new Vector3(_size[_state], _size[_state], _size[_state]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _state = 3;
            transform.localScale = new Vector3(_size[_state], _size[_state], _size[_state]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _state = 4;
            transform.localScale = new Vector3(_size[_state], _size[_state], _size[_state]);
        }
    }
}
