using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackscreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _box;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Inside a wall");
        _box.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Went out of a wall");
        _box.SetActive(false);
    }
}
