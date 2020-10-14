using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private BossHelper _bossHelper;
    private UIManager _uIManager;
    private Animator _anim;
    private AudioSource _audioSource;

    private int _count = 1;
    [SerializeField]
    private GameObject _bullets;
    [SerializeField]
    private GameObject _explositon;
    [SerializeField]
    private GameObject _playerExcplosion;

    private void Start()
    {
        _bossHelper = GameObject.Find("BossHelper").GetComponent<BossHelper>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = this.GetComponent<AudioSource>();
        if (_audioSource == null) Debug.LogError("Audio Source is null");
        if (_bossHelper == null) Debug.LogError("Boss Helper is null");
        if(_uIManager == null) Debug.LogError("The UIManager is null");
        _anim = gameObject.GetComponent<Animator>();
        _explositon.SetActive(false);
    }
    void Update()
    {
    }

    public void StartBoss()
    {
        if (this != null) { 
            transform.transform.parent = null;
            if(transform.parent == null)
            {
                _anim.SetTrigger("BossStart");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            _count++;
            if(_count == 2)
            {
                Instantiate(_bullets,this.transform.position + new Vector3(0, -1, 0), Quaternion.identity);
                _count = 0;
            }
            _bossHelper.BossDamage();

            Destroy(other.gameObject);
            int health = _bossHelper.getBossHealth();
            if (health == 0)
            {
                _explositon.SetActive(true);
                _explositon.transform.transform.parent = null;
                StartCoroutine(BossDead());
                _audioSource.Play();
                Destroy(_explositon, 2.40f);
            }
        }
        if(other.tag == "Player")
        {

            var explosion = Instantiate(_playerExcplosion, other.transform.position, Quaternion.identity);
            Player player = other.GetComponent<Player>();
            Destroy(other.gameObject, 0.19f);
            Destroy(explosion, 2.40f);
        }
    }
    IEnumerator BossDead()
    {
        yield return new WaitForSeconds(0.19f);
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.40f);
        Destroy(this.gameObject);
    }
}
