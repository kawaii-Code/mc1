using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Text scoreText;
    [SerializeField] private float speedCoefficient;

    private Vector2 velocity;
    private bool isGrounded;
    private float xMax;
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private int score;

    public bool StarPower { get; private set; }

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        xMax = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        float inputAxis = Input.GetAxis("Horizontal");
        velocity = rigidbody2d.velocity;

        if (transform.position.x < -xMax + 0.5f && inputAxis <= 0 ||
            transform.position.x > xMax - 0.5f && inputAxis >= 0)
        {
            velocity.x = 0.0f;
        }
        else
        {
            velocity.x = inputAxis * speed;
        }

        rigidbody2d.velocity = velocity;

        if (isGrounded)
        {
            if (inputAxis != 0)
            {
                animator.SetInteger("State", 1);
            }
            else
            {
                animator.SetInteger("State", 0);
            }
        }

        if (inputAxis < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputAxis > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private IEnumerator StarPowerAnimation(float duration)
    {
        StarPower = true;
        speed *= speedCoefficient;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            if (Time.frameCount % 4 == 0)
            {
                spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }
            yield return null;
            elapsed += Time.deltaTime;
        }

        speed /= speedCoefficient;
        spriteRenderer.color = Color.white;
        StarPower = false;
    }

    public void StarPowerActive(float duration = 5.0f)
    {
        StartCoroutine(StarPowerAnimation(duration));
    }

    public void AddCoin(int count)
    {
        score += count;
        scoreText.text = $"{score}";
    }

    private void Jump()
    {
        animator.SetInteger("State", 2);
        rigidbody2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGrounded && collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = collision.contacts.All(c => c.point.y < transform.position.y);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = !collision.contacts.All(c => c.point.y > transform.position.y);
        }
    }
}
