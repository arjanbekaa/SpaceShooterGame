using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHelper : MonoBehaviour
{

    private GameManager _gameManager;
    private BossHelper _bossHelper;
    private UIManager _uIManager;
    [SerializeField]
    public int bossHealth = 100;
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _bossHelper = GameObject.Find("BossHelper").GetComponent<BossHelper>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_bossHelper == null) Debug.LogError("Boss Helper is null");
        if (_uIManager == null) Debug.LogError("The UIManager is null");
        if (_gameManager == null) Debug.LogError("Game Manager is null");
    }
    public void BossDamage()
    {
        if (bossHealth - 10 < 0) return;
        bossHealth -= 10;
        _uIManager.UpdateBossHealth();
        _gameManager.IsBossDead();
    }
    public int getBossHealth()
    {
        return bossHealth;
    }
}
