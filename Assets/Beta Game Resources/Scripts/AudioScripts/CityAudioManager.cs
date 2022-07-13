using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityAudioManager : MonoBehaviour
{
    private bool _hasPlayed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_hasPlayed)
            {
                Narrator.instance.PlayEvent(Narrator.instance.City_Enter);
                _hasPlayed = true;
            }
        }
    }
}
