using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{

    private float _speed = 2f;
    private Boss _boss;
    private bool _stop = false;
    void Start()
    {
        _boss = GameObject.Find("Boss").GetComponent<Boss>();
        if (_boss == null) Debug.LogError("Boss is null");
    }
    void Update()
    {
        BossMovement();
    }
    public void BossMovement()
    {
        if (transform.position.y <= 2.3f)
        {
            _stop = true;
        }
        if (!_stop)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
        else
        {
            _boss.StartBoss();
        }
    }
    
}
