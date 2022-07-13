using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestAudioManager : MonoBehaviour
{
    [SerializeField]
    private float _delayTimer;

    private bool _hasPlayedIntro, _hasExited;
    

    private PlayerBehaviour _playerInstance;

    private void Start()
    {
        _playerInstance = PlayerBehaviour._instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!_hasPlayedIntro)
            if (other.CompareTag("Player")) StartCoroutine(PlayAudio());
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_hasExited)
            {
                StopAllCoroutines();
                Narrator.instance.PlayEvent(Narrator.instance.Forest_Exit);
                _hasExited = true;
            }
        }
    }

    IEnumerator PlayAudio()
    {
        if (!_hasPlayedIntro)
        {
            Narrator.instance.PlayEvent(Narrator.instance.Forest_Enter);
            _hasPlayedIntro = true;
        }
        while(_playerInstance != null)
        {
            yield return new WaitForSeconds(_delayTimer);
            Narrator.instance.PlayEvent(Narrator.instance.Forest_Delayed);
        }
    }

}
