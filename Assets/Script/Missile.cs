using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Player _player;
    private bool _foundEnemy = false;
    GameObject _target;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) Debug.LogError("Player is null");

    }
    void Update()
    {
        if (!_foundEnemy)
        {
            _target = _player.FindClosestEnemy();
            if (_target != null)
            {
                _foundEnemy = true;
            }
        }
        if (_foundEnemy)
        {
            if (_target != null)transform.position = Vector3.MoveTowards(this.transform.position, _target.transform.position, 7 * Time.deltaTime);
        }
        
    }
    public void LunchMissile(Vector3 playerPos)
    {
        Instantiate(this, playerPos + new Vector3(0, 2, 0), Quaternion.identity);
    }
}
