using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
        StartCoroutine(PowerUpSpawnRoutine());
    }
    public void StartSpawning()
    {
        _stopSpawn = false;
        StartCoroutine(EnemySpawnRoutine());
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
        while (true)
        {
            int ran = Random.Range(0, 110);
            yield return new WaitForSeconds(Random.Range(4,8));
            var instantiatedPowerUp = Instantiate(_powerups[rarity(ran)], new Vector3(Random.Range(-9.30f, 9.30f), 8, 0), Quaternion.identity);
            instantiatedPowerUp.transform.parent = _powerUpContainer.transform;
        }
    }

    public int rarity(int a)
    {
        int result = 0;
        if (a < 20) result = 0;
        else if (a < 40 && a > 20) result = 1;
        else if (a < 60 && a > 40) result = 2;
        else if (a < 80 && a > 60) result = 3;
        else if (a < 100 && a > 80) result = 4;
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
        _enemySpeed += 0.2f;
    }
    public float GetEnemySpeed() { return _enemySpeed; }
}
