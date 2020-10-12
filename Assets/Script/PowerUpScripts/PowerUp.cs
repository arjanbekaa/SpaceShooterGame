using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private GameObject _player;
    [SerializeField]
    private AudioClip _powerUpClip;
    private float _speed = 0.2f;
    [SerializeField]
    private int _powerUpID;

    private Vector3 _target;
    void Start()
    {
        _player = GameObject.Find("Player");
        if (_player == null) Debug.LogError("Player is null");
    }
    void Update()
    {
        this.transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (this.transform.position.y <= -6) Destroy(this.gameObject);
        if (_player != null) _target = _player.transform.position;

        if (Input.GetKey(KeyCode.C))
        {
            MoveTowerdsPlayer();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Player"))
        {
            Player player = collision.GetComponent<Player>();
            Destroy(this.gameObject);
            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.TripleShot();
                        break;
                    case 1:
                        player.SpeedUp();
                        break;
                    case 2:
                        player.ShieldUp();
                        break;
                    case 3:
                        player.AmmoCollected();
                        break;
                    case 4:
                        player.LifeCollactable();
                        break;
                    case 5:
                        player.StartSuperBullet();
                        break;
                }
            }
            AudioSource.PlayClipAtPoint(_powerUpClip, this.transform.position);
        }
    }
    public void MoveTowerdsPlayer()
    {
        float step = _speed * Time.deltaTime;
        if (transform.position.y < _target.y) return;
        transform.position = Vector3.MoveTowards(transform.position, _target, step);
    }
}
