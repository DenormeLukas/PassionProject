using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded = false;

    private ParticleSystem deathParticles;

    static public bool isDeath;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        deathParticles = GameObject.Find("DieAnimation").GetComponent<ParticleSystem>();

        isDeath = false;
    }

    void Update()
    {

        Debug.Log(isGrounded);

        float moveAmount = CameraMovement.speed * Time.deltaTime;
        transform.position += new Vector3(moveAmount, 0, 0);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began && isGrounded)
                {
                    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                }
            }
        }

        FelDown();
    }

    void FelDown()
    {
        if (transform.position.y < -12.0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<SpriteRenderer>().color != Color.black)
        {
            if (!Filters.isDark)
            {
                Die();
            }
        }

        if (col.gameObject.CompareTag("Spike"))
        {
            if (!Filters.isBlurred)
            {
                Die();
            }
        }

        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        isGrounded = false;
    }

    void Die()
    {
        isDeath = true;
        this.gameObject.SetActive(false);
        deathParticles.transform.position = transform.position;
        deathParticles.Play();

        Invoke("Restart", 1.0f);

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

