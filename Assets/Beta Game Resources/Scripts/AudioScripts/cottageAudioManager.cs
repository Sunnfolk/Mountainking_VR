using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cottageAudioManager : MonoBehaviour
{
    private bool _hasPlayed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_hasPlayed)
            {
                Narrator.instance.PlayEvent(Narrator.instance.Cabin_Spotted);
                _hasPlayed = true;
            }
        }
    }
}
