using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _bulletSpeed = 8;
    void Update()
    {
        Movement();
        Cleaning();
    }

    public void Movement()
    {
        if (this.transform.parent.name == "EnemyBullet(Clone)")
        {
            this.transform.Translate(Vector3.down * _bulletSpeed * Time.deltaTime);
        }
        else
        {
            if (this.name == "BulletRight")
            {
                this.transform.Translate(Vector3.down * _bulletSpeed * Time.deltaTime);
            }
            else
            {
                this.transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);
            }
        }
    }
    public void Cleaning()
    {
        if (this.transform.position.y >= 8f)
        {
            if (this.transform.parent != null && transform.parent.name == "TripleShot(Clone)") Destroy(this.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
        if (this.transform.position.y <= -7f)
        {
            if (this.transform.parent != null && transform.parent.name == "EnemyBullet(Clone)") Destroy(this.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
        if (this.transform.position.x <= -11f || this.transform.position.x >= 11f)
        {
            if (this.transform.parent != null && transform.parent.name == "SuperBullet(Clone)") Destroy(this.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && this.transform.parent.name == "EnemyBullet(Clone)")
        {
            Destroy(this.transform.parent.gameObject);
            Player player = other.GetComponent<Player>();
            if (player != null) player.Damage();
        }
    }
}
