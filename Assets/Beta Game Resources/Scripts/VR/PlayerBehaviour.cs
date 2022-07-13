using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
	This script should have player variables and stuff like that.
	Other stuff related to the player could also be done here.
	
	This script is required by the Billboard script. If you'd like
	to remove the usage of this, please contact Elias so he can change
	the Billboard script to instead rely on something else.
	 */

public class PlayerBehaviour : MonoBehaviour {


	public static PlayerBehaviour _instance = null;

	private PlayerSounds playerSounds;

	private bool _last;

	[SerializeField]
	private Score _score;

    [SerializeField]
    private float _delayTime;

    private bool _hasPlayedStage3, _hasPlayedStage4, _hasPlayedStage5;

	public enum TrollSize {
		LegSize = 1,
		HumanSize = 2,
		BigAnimalSize = 3,
		TreeSize = 4,
		MountainKing = 5,
	};

	private float[] _size =
	{
		0.3f,
		1f,
		4f,
		9f,
		12f
	};

	// the amount of score needed to grow
	private int[] _scoreAmount =
	{
	19,
	39,
	59,
	119,
    };

	private float _scale;
	private float _currentSizeAddition;

	public TrollSize stage;

	private int _state;

	// Start is called before the first frame update
	void Awake() {
		if (!_instance) {
			_instance = this;
		} else if (_instance != this) {
			Destroy(gameObject);
		}

		if (!playerSounds) playerSounds = gameObject.GetComponentInChildren<PlayerSounds>();
	}

    private void Start()
    {
        _score.score = _score._prevScore;

        if (_score.previousScene != null || _score.previousScene != "OpenWorld_Beta" || _score.previousScene != "Cave_Beta")
        {
            switch(_score.previousScene)
            {
                case "FoodFrenzy_Beta":
                    transform.position = _score.afterFrenzy;
                    break;
                case "TeaParty_Beta":
                    transform.position = _score.afterTea;
                    break;
                case "CowBowling_Beta":
                    transform.position = _score.afterBowling;
                    break;
            }
        }
    }
    // Update is called once per frame
    void Update() {

        _score.PlayerState = _state;

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			_score.score = 0;
		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			_score.score = 20;
		} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			_score.score = 40;
		} else if (Input.GetKeyDown(KeyCode.Alpha4)) {
			_score.score = 60;
		} else if (Input.GetKeyDown(KeyCode.Alpha5)) {
			_score.score = 120;
		}

		if (_score.score <= _scoreAmount[0]) {
			stage = TrollSize.LegSize;
		} else if (_score.score <= _scoreAmount[1]) {
			stage = TrollSize.HumanSize;
		} else if (_score.score <= _scoreAmount[2]) {
			stage = TrollSize.BigAnimalSize;
		} else if (_score.score <= _scoreAmount[3]) {
			stage = TrollSize.TreeSize;
		} else {
            Narrator.instance.PlayEvent(Narrator.instance.Stage_5);
			stage = TrollSize.MountainKing;
		}

		switch (stage) {
			case TrollSize.LegSize:
				_scale = _score.score / _scoreAmount[0] * 100;
				_currentSizeAddition = (0.2f * _scale) / 100;
				_state = 0;
				transform.localScale = new Vector3(_size[_state] + _currentSizeAddition, _size[_state] + _currentSizeAddition, _size[_state] + _currentSizeAddition);
				break;
			case TrollSize.HumanSize:
				_scale = (_score.score - 20) / _scoreAmount[0] * 100;
				_currentSizeAddition = (1 * _scale) / 100;
				_state = 1;
				transform.localScale = new Vector3(_size[_state] + _currentSizeAddition, _size[_state] + _currentSizeAddition, _size[_state] + _currentSizeAddition);
				break;
			case TrollSize.BigAnimalSize:
				_scale = (_score.score - 40) / _scoreAmount[0] * 100;
				_currentSizeAddition = (2 * _scale) / 100;
				_state = 2;
				transform.localScale = new Vector3(_size[_state] + _currentSizeAddition, _size[_state] + _currentSizeAddition, _size[_state] + _currentSizeAddition);
                if (!_hasPlayedStage3)
                {
                    Narrator.instance.PlayEvent(Narrator.instance.Stage_3);
                    _hasPlayedStage3 = true;
                    Debug.Log("Playing Stage 3");
                }
				break;
			case TrollSize.TreeSize:
				_scale = (_score.score - 60) / _scoreAmount[3] * 100;
				_currentSizeAddition = (4 * _scale) / 100;
				_state = 3;
				transform.localScale = new Vector3(_size[_state] + _currentSizeAddition, _size[_state] + _currentSizeAddition, _size[_state] + _currentSizeAddition);
                if (!_hasPlayedStage4)
                {
                    Narrator.instance.PlayEvent(Narrator.instance.Stage_4);
                    Debug.Log("Playing Stage 4");
                    _hasPlayedStage4 = true;
                }
				break;
			case TrollSize.MountainKing:
				_state = 4;
				transform.localScale = new Vector3(_size[_state], _size[_state], _size[_state]);
                if (!_hasPlayedStage5)
                {
                    Debug.Log("Playing Stage 5");
                    StartCoroutine(PlayStage5());
                }
				break;
		}

	}

    IEnumerator PlayStage5()
    {
        if (!_hasPlayedStage5)
        {
            Narrator.instance.PlayEvent(Narrator.instance.Stage_5);
            _hasPlayedStage5 = true;
        }
        yield return new WaitForSecondsRealtime(_delayTime);
        Narrator.instance.PlayEvent(Narrator.instance.Stage_5_Delayed);
    }

    public ref PlayerSounds GetSoundPlayer() {
		return ref playerSounds;
	}

	public void Eat(Edible.Edible other) {
		_score.AddScore(other.OriginalValue());
		//foodMinigameManager.score++;


		GetSoundPlayer().SetParam(
		    other.Tree(),
		    other.Glass(),
		    other.Plant(),
		    other.Metal(),
		    other.Rock(),
		    (float)stage
		    );

        GetSoundPlayer().PlayEatSound();

		other.Eat();
	}


}
