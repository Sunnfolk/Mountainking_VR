using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAudioManager : MonoBehaviour
{
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
        if (!_hasPlayed && (int)_playerInstance.stage == 5)
        {
            Debug.Log("Ending music");
            Narrator.instance.PlayEvent(Narrator.instance.Ending);
            _hasPlayed = true;
        }
    }
}
