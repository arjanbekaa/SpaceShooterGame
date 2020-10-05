using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private GameObject _bulletContainer;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _shootAudio;
    [SerializeField]
    private GameObject _explosionAudio;
    [SerializeField]
    private GameObject _shieldGO;
    private SpriteRenderer _shiedlSpriteRenderer;

    private float _speed = 4f;
    private float _speedMultiplayer = 2f;
    private int _life = 3;
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    private float _relodeWepon = 0.3f;
    private int _bulletAmount = 0;
    private int _maxBullet = 6;
    public bool _tripleShotActive { get; set; }
    public bool _speedBoostActive { get; set; }
    private int _shieldLifes = 3;
    private bool _shieldActive = false;
    private UIManager _uIManager;
    private int _ran;

    private SpawnManager _spawnManager;
    void Start()
    {
        _ran = Random.Range(1, 3);
        _tripleShotActive = false;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _shiedlSpriteRenderer = _shieldGO.GetComponent<SpriteRenderer>();
        if (_spawnManager == null) Debug.LogError("Spawn Manager is null");
        if(_uIManager == null) Debug.LogError("The UIManager is null");
    }

    void Update()
    {
        playerMovement();
        playerBounds();
        shoot();
    }

    public void playerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        this.transform.Translate(new Vector3(horizontal, vertical, 0) * _speed * Time.deltaTime);
    }
    public void playerBounds()
    {

        this.transform.position = new Vector3(this.transform.position.x, Mathf.Clamp(this.transform.position.y, -4, 5.8f), 0);

        if (this.transform.position.x >= 11.06101)
        {
            this.transform.position = new Vector3(-11.18091f, this.transform.position.y, 0);
        }
        else if (this.transform.position.x <= -11.18091f)
        {
            this.transform.position = new Vector3(11.06101f, this.transform.position.y, 0);
        }
    }
    public void shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > _canFire)
        {
            if (!_tripleShotActive) {
                if (_bulletAmount <= _maxBullet)
                {
                    _canFire = Time.time + _fireRate;
                    _bulletAmount++;
                    var instantiatedBullet = Instantiate(_bullet, this.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                    instantiatedBullet.transform.parent = (_bulletContainer.transform);
                    _shootAudio.GetComponent<AudioSource>().Play();
                }
                else
                {
                    _canFire = Time.time + _relodeWepon;
                    _bulletAmount = 0;
                }
            }
            else
            {
                if (_bulletAmount <= _maxBullet)
                {
                    _canFire = Time.time + _fireRate;
                    _bulletAmount += 3;
                    var instantiatedBullet = Instantiate(_tripleShot, this.transform.position + new Vector3(-8.46f, 0,0), Quaternion.identity);
                    instantiatedBullet.transform.parent = (_bulletContainer.transform);
                    _shootAudio.GetComponent<AudioSource>().Play();
                }
                else
                {
                    _canFire = Time.time + _relodeWepon;
                    _bulletAmount = 0;
                }
            }
        }
    }
    public void AddSpeed()
    {
        _speed += 0.2f;
    }
    public float getSpeed() { return _speed; }
    public void Damage()
    {
        if (_shieldActive)
        {
            _shieldLifes--;
            ShieldColor();
            return;
        }

        _life--;

        _uIManager.UpdateLives(_life);
        if (_life == 2)
        {
            if (_ran == 1) _leftEngine.SetActive(true);
            else _rightEngine.SetActive(true);
        }
        else if (_life == 1)
        {
            if (_leftEngine.active) _rightEngine.SetActive(true);
            else _leftEngine.SetActive(true);
        }
        else
        {
            var explosion = Instantiate(_explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.19f);
            Destroy(explosion.gameObject, 2.40f);
            _explosionAudio.GetComponent<AudioSource>().Play();
            _spawnManager.GameOver();
        }
    }
    public void TripleShot()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotRoutin());
    }
    IEnumerator TripleShotRoutin()
    {
        yield return new WaitForSeconds(5);
        _tripleShotActive = false;
    }
    public void SpeedUp()
    {
        _speed += _speedMultiplayer;
        StartCoroutine(SpeedUpRoutin());
    }

    IEnumerator SpeedUpRoutin()
    {
        yield return new WaitForSeconds(4);
        _speed -= _speedMultiplayer;
    }

    public void ShieldUp()
    {
        _shieldLifes = 3;
        _shieldActive = true;
        _shield.SetActive(_shieldActive);
        ShieldColor();
    }
    public void AddScore(int points)
    {
        _score += points;
        _uIManager.UpdateScore(_score);
    }
    public void ShieldColor()
    {
        switch (_shieldLifes)
        {
            case 3:
                _shiedlSpriteRenderer.color = Color.white;
                break;
            case 2:
                _shiedlSpriteRenderer.color = Color.blue;
                break;
            case 1:
                _shiedlSpriteRenderer.color = Color.red;
                break;
            case 0:
                _shieldActive = false;
                _shield.SetActive(_shieldActive);
                break;
        }
    }

}
