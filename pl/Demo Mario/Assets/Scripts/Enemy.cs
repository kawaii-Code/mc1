using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rigidbody2d;
    private Player player;
    private Vector2 direction = Vector2.left;
    private float xMax;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        xMax = Camera.main.orthographicSize * Camera.main.aspect;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        rigidbody2d.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool dead = collision.contacts.All(c => c.point.y > transform.position.y);
            if (dead || player.StarPower)
            {
                Destroy(gameObject);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            if (!collision.contacts.All(c => c.point.y < transform.position.y))
            {
                ChangeDirection();
            }
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.x < -xMax + 0.5f && direction.x <= 0 ||
            transform.position.x > xMax - 0.5f && direction.x >= 0)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        direction = -1 * direction;
        Vector2 velocity = rigidbody2d.velocity;
        velocity.x = direction.x * speed;
        rigidbody2d.velocity = velocity;
    }
}
