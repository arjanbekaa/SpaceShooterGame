using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _speedCoolDown;
    [SerializeField]
    private Slider _thrusterBar;
    [SerializeField]
    private Text _ammoTxt;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _missedEnemies;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Button _mainMenuBtn;
    [SerializeField]
    private Sprite[] _livesImage;
    [SerializeField]
    private Image _lifeDisplay;
    private GameManager _gameManager;
    private Player _player;

    private bool _barwait = false;

    private float currentValue = 0f;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = value;
            _thrusterBar.value = currentValue;
        }
    }

    void Start()
    {
        CurrentValue = 100f;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_gameManager == null) Debug.LogError("Game Manager is null");
        _gameOver.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _mainMenuBtn.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
        _missedEnemies.text = "Enemies Missed : 0";
    }
    private void Update()
    {
        if (_barwait)
        {
            StartCoroutine(ThrusetBar());
        }
    }
    public void UpdateMissedEnemies(int missedEnemies)
    {
        _missedEnemies.text = "Enemies Missed : " + missedEnemies;
    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }
    public void UpdateLives(int currentLives)
    {
        _lifeDisplay.sprite = _livesImage[currentLives];
        if (currentLives < 0) currentLives = 0;
        if (currentLives == 0)
        {
            GameOverAction();
        }
    }
    public void UpdateSpeedCDtxt(float a)
    {
        _speedCoolDown.text = "   " + (int)a;
        if((int)a == 5)
        {
            _barwait = true;
            CurrentValue = 0f;
            _speedCoolDown.text = "Wait..";
            StartCoroutine(waitTxt());
        }
    }
    public void UpdateAmmoTxt(int ammo)
    {
        if(ammo < 6)
        {
            _ammoTxt.color = Color.red;
            _ammoTxt.text = "Current/Max: " + ammo +" / " + _player.getMaxBullet();
        }
        else
        {
            _ammoTxt.color = Color.white;
            _ammoTxt.text = "Current/Max: " + ammo + " / " + _player.getMaxBullet();
        }
    }

    IEnumerator waitTxt()
    {
        yield return new WaitForSeconds(5);
        _speedCoolDown.text = "GO";
        _barwait = false;
    }

    IEnumerator ThrusetBar()
    {
        while (_barwait)
        {
            CurrentValue += 0.000038f;

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void GameOverAction()
    {
        _gameManager.GameOver();
        _mainMenuBtn.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gameOver.gameObject.SetActive(true);
        StartCoroutine(FlickerRoutin());
    }

    public void LodeMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator FlickerRoutin()
    {
        while(true)
        {
            _gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
