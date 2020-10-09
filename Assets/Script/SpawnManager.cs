using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _astroid;
    [SerializeField]
    private GameObject [] _enemy;
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
  /*  public void StopSpawning()
    {
        _stopSpawn = true;
        var InstantiatedEnemy = Instantiate(_astroid, new Vector3(0, 3, 0), Quaternion.identity);
    }*/
    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (!_stopSpawn) {
            int ran = Random.Range(0, 30);
            var InstantiatedEnemy = Instantiate(_enemy[EnemyRarity(ran)], new Vector3(Random.Range(-9.30f, 9.30f), 8, 0), Quaternion.identity);
            InstantiatedEnemy.transform.parent = (_enemyContainer.transform);
        yield return new WaitForSeconds(4);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (!_stopSpawn)
        {
            int ran = Random.Range(0, 110);
            yield return new WaitForSeconds(Random.Range(6,9));
            var instantiatedPowerUp = Instantiate(_powerups[PUrarity(ran)], new Vector3(Random.Range(-9.30f, 9.30f), 8, 0), Quaternion.identity);
            instantiatedPowerUp.transform.parent = _powerUpContainer.transform;
        }
    }
    public int EnemyRarity(int a)
    {
        int result = 0;
        if (a < 15) result = 0;
        else if (a > 15 && a < 20) result = 1;
        else result = 2;
        return result;
    }
    public int PUrarity(int a)
    {
        int result = 0;
        if (a < 10) result = 0;
        else if (a < 40 && a > 10) result = 1;
        else if (a < 60 && a > 40) result = 2;
        else if (a < 90 && a > 60) result = 3;
        else if (a < 100 && a > 90) result = 4;
        else result = 5;
        return result;
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
        if(_enemySpeed <= 5)_enemySpeed += 0.2f;
    }
    public float GetEnemySpeed() { return _enemySpeed; }
}
