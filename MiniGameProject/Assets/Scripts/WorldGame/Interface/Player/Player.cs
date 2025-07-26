using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour, IMove, IClothes
{
    public enum PlayerState
    {
        Idle,
        Walking,
        Running,
        Jumping
    }
    public enum PlayerGameMode
    {
        Map,
        StackBlock,
        Run,
        None
    }
    public Animator animator;

    public static Player Instance { get; private set; }

    private bool isGrounded = true;
    public PlayerState playerState = PlayerState.Idle;
    public PlayerGameMode playerGameMode = PlayerGameMode.None;

    private float _moveSpeed = 5f;
    public float jumpPower = 30;
    private Vector2 _movementDirection;
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public bool isLoad = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public Vector2 movementDirection
    {
        get { return _movementDirection; }
        private set { _movementDirection = value; }
    }
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        private set { _moveSpeed = value; }
    }
    void Start()
    {

    }
    void Update()
    {
        isMoveAni();
    }
    public void OnMove()
    {
        switch (playerGameMode)
        {
            case PlayerGameMode.StackBlock:

                break;
            case PlayerGameMode.Run:
                RunMove();
                break;
            case PlayerGameMode.Map:
                MapMove();
                break;
            default:
                MapMove();
                break;
        }
    }
    public void OnRunGame()
    {   
        playerGameMode = PlayerGameMode.Run;
        rb.gravityScale = 10.0f;
    }
    public void OnBlockStackGame()
    {
        playerGameMode = PlayerGameMode.StackBlock;
    }

    public void MapMove()
    { 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
        transform.position += new Vector3(movementDirection.x, movementDirection.y, 0) * MoveSpeed * Time.deltaTime;
    }
    

    public void isMoveAni()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            playerState = PlayerState.Walking;
            animator.SetBool("isMove", true);
        }
        else
        {
            playerState = PlayerState.Idle;
            animator.SetBool("isMove", false);
        }

        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void RunMove()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        movementDirection = new Vector2(horizontal, 0).normalized;
        rb.velocity = new Vector2(movementDirection.x * _moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.X) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        switch(playerGameMode)
            {
            case PlayerGameMode.StackBlock:
                break;
            case PlayerGameMode.Run:
                if (other.gameObject.CompareTag("Ground"))
                {
                    isGrounded = true;
                }
                break;
            default:
                break;
        }


    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        switch (playerGameMode)
        {
            case PlayerGameMode.StackBlock:

            case PlayerGameMode.Run:
                if (other.gameObject.CompareTag("Obstacle"))
                {
                    rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    if (spriteRenderer != null)
                        StartCoroutine(BlinkSprite(1f, 0.1f));
                }
                break;
            case PlayerGameMode.Map:
                switch (other.gameObject.tag)
                {
                    case "Building1":
                        UIManager_World.Instance.OnStartGameUI("Building1");
                        break;
                    case "Building2":
                        UIManager_World.Instance.OnStartGameUI("Building2");
                        break;
                    case "ReaderBoard":
                        UIManager_World.Instance.OnReaderBoard();
                        break;
                    case "NPC":
                        UIManager_World.Instance.OnOpenShop();
                        break;
                }
                break;
            default:
                break;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Building1":
                UIManager_World.Instance.OnStartGameUI("Building1");
                break;
            case "Building2":
                UIManager_World.Instance.OnStartGameUI("Building2");
                break;
            case "ReaderBoard":
                UIManager_World.Instance.OnReaderBoard();
                break;
            case "NPC":
                UIManager_World.Instance.OnOpenShop();
                break;
        }
    }


    IEnumerator BlinkSprite(float duration, float blinkInterval)
    {
        float timer = 0f;
        bool visible = true;
        while (timer < duration)
        {
            visible = !visible;
            spriteRenderer.enabled = visible;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }
        spriteRenderer.enabled = true; 
    }

    public void OnClothesChange(Dictionary<string, string> clothesDict)
    {
        foreach (var kvp in clothesDict)
        {
            string partTag = kvp.Key;      // 부위 ("Object", "sprite", "Effect")
            string spriteName = kvp.Value; // 변경할 스프라이트 이름
            string resourcePath = "";

            // 부위별 경로 지정
            switch (partTag)
            {
                case "Object":
                    resourcePath = $"Image/Character/Object/{spriteName}";
                    break;
                case "Sprite":
                    resourcePath = $"Image/Character/Sprite/{spriteName}";
                    partTag = "Player";
                    break;
                case "Effect":
                    resourcePath = $"Image/Character/Effect/{spriteName}";
                    break;
                default:
                    resourcePath = spriteName;
                    break;
            }
            if (partTag == "Player")
            {
                var newController = Resources.Load<RuntimeAnimatorController>($"Ani/{spriteName}");
                animator.runtimeAnimatorController = newController;
            }
            else
            {
                foreach (Transform child in transform)
                {
                    if (child.CompareTag(partTag))
                    {
                        SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                        if (sr != null)
                        {
                            Sprite newSprite = Resources.Load<Sprite>(resourcePath);
                            if (newSprite != null)
                            {
                                sr.sprite = newSprite;
                            }
                            else
                            {
                                Debug.LogWarning($"스프라이트를 찾을 수 없습니다: {resourcePath}");
                            }
                        }
                    }
                }
            }
        }
    }
}
