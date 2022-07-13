using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmAudioManager : MonoBehaviour
{
    [SerializeField]
    private float _delayTime;

    [SerializeField]
    [Range(0, 100)]
    private float probability;

    private float timer;

    private PlayerBehaviour _playerInstance;

    private bool detected;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        _playerInstance = PlayerBehaviour._instance;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        detected = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > _delayTime)
        {
            if (Random.Range(0, 100) > 100.0f - probability && detected)
            {
                Narrator.instance.PlayEvent(Narrator.instance.Farm_Delayed);
            }

            timer = 0.0f;
        }
    }
}
