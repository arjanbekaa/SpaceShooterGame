using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private GameObject _explositon;
    [SerializeField]
    private GameObject _explosionAudio;
    private SpawnManager _spawnManager;
    private float _rotateSpeed = 20f;
    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null) Debug.LogError("Spawn Manager is null ");
    }
    void Update()
    {
        this.transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime); 
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Bullet(Clone)")
        {
            _spawnManager.SpawnAmmo();
            var explosion = Instantiate(_explositon, this.transform.position, Quaternion.identity);
            _explosionAudio.GetComponent<AudioSource>().Play();
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.19f);
            Destroy(collision.gameObject);
            Destroy(explosion, 2.40f);
        }
    }
}
