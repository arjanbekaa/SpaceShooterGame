using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircleCollider : MonoBehaviour
{
    private GameObject _player;
    private SpawnManager _spawnManager;
    private Vector3 _target;
    private bool _isNear = false;
    private Collider2D _collider;
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _player = GameObject.Find("Player");
        if (_player == null) Debug.LogError("Player is null");
        if (_spawnManager == null) Debug.LogError("Spawn Manager is null");
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null) _target = _player.transform.position;
        if (_collider != null) MoveTowerdsPlayer();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            _collider = other;
            _isNear = true;
        }
    }
    public void MoveTowerdsPlayer()
    {
        if (_isNear)
        {
            if (_collider.transform.position.y < _target.y) return;
            float step = _spawnManager.GetEnemySpeed() * Time.deltaTime;
            _collider.transform.position = Vector3.MoveTowards(_collider.transform.position, _target, step);
        }
    }
}
