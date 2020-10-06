using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private AudioClip _powerUpClip;
    private float _speed = 2;
    [SerializeField]
    private int _powerUpID;
    void Update()
    {
        this.transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (this.transform.position.y <= -6) Destroy(this.gameObject);
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
                }
            }
            AudioSource.PlayClipAtPoint(_powerUpClip, this.transform.position);
        }
       
    }
}
