using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded = false;

    private ParticleSystem deathParticles;

    static public bool isDeath;

    static public bool l2 = false;
    static public bool l3 = false;

    private string currScene;

    private AudioSource death;
    private AudioSource jump;

    private float touchDuration;
    private float touchStartTime;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        deathParticles = GameObject.Find("DieAnimation").GetComponent<ParticleSystem>();

        isDeath = false;

        death = GameObject.Find("Death").GetComponent<AudioSource>();
        jump = GameObject.Find("Jump").GetComponent<AudioSource>();
    }

    //Check if player fell in a hole
    void FelDown()
    {
        if (transform.position.y < -12.0f)
        {
            death.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    //Check collisions. Able to jump? Touching any death triggers? Any filters active?
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<SpriteRenderer>().color != Color.black && col.gameObject.GetComponent<SpriteRenderer>().color != Color.white)
        {
            if (!Filters.isDark)
            {
                Die();
            }
        }

        if (col.gameObject.GetComponent<SpriteRenderer>().color == Color.white)
        {
            Finished();
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

    //Trigger death animation and level restart
    void Die()
    {
        death.Play();
        isDeath = true;
        this.gameObject.SetActive(false);
        deathParticles.transform.position = transform.position;
        deathParticles.Play();

        Invoke("Restart", 1.0f);

    }

    //Restart level
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Level finished
    void Finished()
    {

        //Get current level
        currScene = SceneManager.GetActiveScene().name;


        //Check which level is completed and update booleans
        if (currScene == "Level2")
        {
            l2 = true;
        }
        else if (currScene == "Level3")
        {
            l3 = true;
        }

        SceneManager.LoadScene("Levels");
    }



#if !UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_WEBPLAYER

    void Update()
    {

        //Move player according to camera speed
        float moveAmount = CameraMovement.speed * Time.deltaTime;
        transform.position += new Vector3(moveAmount, 0, 0);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jump.Play();
        }

        FelDown();
    }



#elif UNITY_IOS || UNITY_ANDROID

    void Update()
    {

        //Move player according to camera speed
        float moveAmount = CameraMovement.speed * Time.deltaTime;
        transform.position += new Vector3(moveAmount, 0, 0);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartTime = Time.time;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                touchDuration = Time.time - touchStartTime;
                 
                if (touchDuration < 0.2f && Filters.swipeDelta.x < 50.0f && Filters.swipeDelta.x > -50.0f && Filters.swipeDelta.y < 50.0f && Filters.swipeDelta.y > -50.0f)
                {
                    // Checks if its a short touch or a long touch, short touch -> jump, otherwise -> check filter.cs
                    if (isGrounded)
                    {
                        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                        jump.Play();
                    }
                }
            }

            FelDown();
        }

    }

#endif


}