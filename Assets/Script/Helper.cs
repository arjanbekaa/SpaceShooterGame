using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    private SpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        if (transform.Find("NewEnemy (1)") != null) this.transform.Translate(Vector3.down * _spawnManager.GetEnemySpeed() * Time.deltaTime);
        else Destroy(this.gameObject);
        if (this.transform.position.y <= -5.39f)
        {
            this.transform.position = new Vector3(Random.Range(-9.46f, 9.43f), 6.94f, 0);
            _spawnManager.miss();
        }
    }
}
