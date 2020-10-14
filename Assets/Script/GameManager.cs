using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    private UIManager _uIManager;
    private SpawnManager _spawnManager;
    [SerializeField]
    private BossHelper _bossHelper;

    private int _enemyDistroyed = 0;
    private int _waveEnemies = 4;
    private int _numberOfWaves = 2;
    private int _wavesFinished = 0;
    private bool _isBossComing = false;
    private bool _isBossDead = false;

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _bossHelper = GameObject.Find("BossHelper").GetComponent<BossHelper>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_bossHelper == null) Debug.LogError("Boss Helper is null");
        if (_spawnManager == null) Debug.LogError("Spawn Manager is null");
        if (_uIManager == null) Debug.LogError("The UIManager is null");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void WaveFinished()
    {
        if (_wavesFinished <= _numberOfWaves)
        {
            _enemyDistroyed++;
            
            if (_wavesFinished == _numberOfWaves)
            {
                _isBossComing = true;
            }
            else
            {
                if (_enemyDistroyed == _waveEnemies)
                {
                    _spawnManager.StopSpawning();
                    _waveEnemies += _waveEnemies;
                    _wavesFinished++;
                    _enemyDistroyed = 0;
                    _uIManager.UpdateWaveText(_wavesFinished);
                }
            }
        }
        if (_isBossComing)
        {
            _spawnManager.SpawnAmmo();
            _spawnManager.stopPu();
            _uIManager.UpdateWaveText(_wavesFinished);
        }
    }

    public void IsBossDead()
    {
        if (_bossHelper.getBossHealth() == 0)
        {
            _isBossDead = true;
            WaveFinished();
        }
    }

    public int getWave() { return _numberOfWaves; }
    public bool getIsBosDead() { return _isBossDead; }
}
