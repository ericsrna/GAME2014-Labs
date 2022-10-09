using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScreenBounds
{
    public Boundary horizontal;
    public Boundary vertical;
}

public class BulletBehaviour : MonoBehaviour
{
    [Header("Bullet Properties")]
    public BulletDirection bulletDirection;
    public float speed;
    public ScreenBounds bounds;    
    private Vector3 velocity;
    public BulletManager bulletManager;

    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
        SetDirection(bulletDirection);
    }

    void Update()
    {
        Move();
        CheckBounds();
    }

    void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }

    void CheckBounds()
    {
        if ((transform.position.x > bounds.horizontal.max) || 
            (transform.position.x < bounds.horizontal.min) ||
            (transform.position.y > bounds.vertical.max) ||
            (transform.position.y < bounds.vertical.min)
            )
        {
            // return the bullet to the pool
            bulletManager.ReturnBullet(this.gameObject);
        }
    }

    public void SetDirection(BulletDirection direction)
    {
        switch (direction)
        {
            case BulletDirection.UP:
                velocity = Vector3.up * speed;
                break;
            case BulletDirection.DOWN:
                velocity = Vector3.down * speed;
                break;
            case BulletDirection.LEFT:
                velocity = Vector3.left * speed;
                break;
            case BulletDirection.RIGHT:
                velocity = Vector3.right * speed;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bulletManager.ReturnBullet(this.gameObject);
    }
}
