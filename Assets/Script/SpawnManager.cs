using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject _powerUpContainer;
    [SerializeField]
    public int score;
    private UIManager _uIManager;
    private bool _stopSpawn;
    private float _enemySpeed = 3f;
    private int _missedEnemies = 0;

    private void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    public void StartSpawning()
    {
        _stopSpawn = false;
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawn == false) { 
        var InstantiatedEnemy = Instantiate(_enemy, new Vector3(Random.Range(-9.30f, 9.30f), 8, 0), Quaternion.identity);
            InstantiatedEnemy.transform.parent = (_enemyContainer.transform);
        yield return new WaitForSeconds(4);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        while(_stopSpawn == false)
        {
            yield return new WaitForSeconds(Random.Range(5,10));
            var instantiatedPowerUp = Instantiate(_powerups[Random.Range(0,5)], new Vector3(Random.Range(-9.30f, 9.30f), 8, 0), Quaternion.identity);
           instantiatedPowerUp.transform.parent = _powerUpContainer.transform;
        }
    }
    public void GameOver()
    {
        _stopSpawn = true;
        var clones = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var clone in clones) { Destroy(clone); }
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }

    public void miss() {
        _missedEnemies++;
        if (_missedEnemies >= 5)
        {
            _uIManager.GameOverAction();
            GameOver();
        }
        _uIManager.UpdateMissedEnemies(_missedEnemies);
        
    }
    public void AddEnemySpeed()
    {
        _enemySpeed += 0.2f;
    }
    public float GetEnemySpeed() { return _enemySpeed; }
}
