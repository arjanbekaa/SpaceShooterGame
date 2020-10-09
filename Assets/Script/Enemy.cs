using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private GameObject _explositon;
    [SerializeField]
    private GameObject _superBullet;
    [SerializeField]
    private AudioClip _explosionClip;
    [SerializeField]
    private GameObject _enemyBullet;
    private AudioSource _audioSource;
    private SpawnManager _spawnManager;
    private Player _player;
    private bool _isDestroyed = false;
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    private bool _isShielded;
    private Vector3 _pos;

    void Start()
    {
        _pos = this.transform.position;
        EnemyCheck(); 
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = this.GetComponent<AudioSource>();
        if (_audioSource == null) Debug.LogError("Audio Source is null");
        if (_shield == null) Debug.LogError("Not an Enemy with a Shield");
        if (_spawnManager == null) Debug.LogError("Spawn Manager is null");
        if (_player == null) Debug.LogError("Player is null");
        _audioSource.clip = _explosionClip;
    }
    void Update()
    {
        CalculateMovement();
       
        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            Instantiate(_enemyBullet, this.transform.position, Quaternion.identity);
        }
    }
    public void CalculateMovement()
    {
        if (id != 2)
        {
            if (!_isDestroyed) this.transform.Translate(Vector3.down * _spawnManager.GetEnemySpeed() * Time.deltaTime);
            if (this.transform.position.y <= -5.39f)
            {
                this.transform.position = new Vector3(Random.Range(-9.46f, 9.43f), 6.94f, 0);
                _spawnManager.miss();
            }
        }
        else
        {
            if (!_isDestroyed)
            {
                Vector3 final = new Vector3(0, -1, 0);
                if (_pos.x < 0)
                {
                    final.x += 0.5f;
                }
                else
                {
                    final.x += -0.5f;
                }
                this.transform.Translate(final * _spawnManager.GetEnemySpeed() * Time.deltaTime);
            }

            if (this.transform.position.y <= -5.39f || this.transform.position.x >= 11 || this.transform.position.x <= -11)
            {
                // i left it so when it spawns for the second time it will have the same direction so when others apper will be more randome;
                this.transform.position = new Vector3(Random.Range(-9.46f, 9.43f), 6.94f, 0);
                _spawnManager.miss();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If the player tuches the enemy even with a shield the enemy will die imidiatly;
        if (other.tag == "Player")
        {
            _player.WaveFinished();
            var explosion = Instantiate(_explositon, this.transform.position, Quaternion.identity);
            Player player = other.GetComponent<Player>();
            if (player != null) player.Damage();
            _isDestroyed = true;
            Destroy(this.gameObject, 0.19f);
            Destroy(explosion, 2.40f);
        }
        if (other.tag == "Bullet")
        {
            if (_isShielded)
            {
                _isShielded = false;
                _shield.SetActive(_isShielded);
                Destroy(other.gameObject);
                return;
            }

            _player.WaveFinished();
            var explosion = Instantiate(_explositon, this.transform.position, Quaternion.identity);
            _isDestroyed = true;
            _player.AddSpeed();
            _player.AddScore(Random.Range(5, 10));
            _spawnManager.AddEnemySpeed();
            _audioSource.Play();

            if (_player.GetSuperBulletOn())
            {
                StartCoroutine(SpawnSuperBullet());
            }

            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.19f);
            Destroy(explosion, 2.40f);
        }
    }

    public void EnemyCheck()
    {
        if (id == 1)
        {
            _isShielded = true;
            _shield.SetActive(_isShielded);
        }
        else 
        {
            _isShielded = false;
            _shield.SetActive(_isShielded);
        }
    }

    IEnumerator SpawnSuperBullet()
    {
        yield return new WaitForSeconds(0.18f);
        Instantiate(_superBullet, this.transform.position, Quaternion.identity);
    }
}
