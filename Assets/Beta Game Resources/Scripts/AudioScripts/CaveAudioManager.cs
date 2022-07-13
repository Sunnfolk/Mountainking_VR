using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveAudioManager : MonoBehaviour
{
    [SerializeField]
    private float _beginningWaitTime,  _delayedTime;


    private PlayerBehaviour _playerInstance;
    private bool _hasPlayed;

    // Start is called before the first frame update
    void Start()
    {
        _playerInstance = PlayerBehaviour._instance;
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)_playerInstance.stage == 1 && !_hasPlayed) StartCoroutine(PlaySounds());
    }

    IEnumerator PlaySounds()
    {
        _hasPlayed = true;
        yield return new WaitForSecondsRealtime(_beginningWaitTime);
        Narrator.instance.PlayEvent(Narrator.instance.Cave_Start);
        while (_playerInstance != null)
        {
            yield return new WaitForSecondsRealtime(_delayedTime);
            Narrator.instance.PlayEvent(Narrator.instance.Cave_Delayed);
        }
    }
}
