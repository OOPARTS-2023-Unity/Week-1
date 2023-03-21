using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rgbody;

    // Tooltip은 에디터에서 플로팅 도움말을 띄워줍니다.
    [Tooltip("Jump power")]
    public float JUMP_POWER;

    [Tooltip("Maximum count of jump")]
    public int MAX_JUMP_COUNT;

    [Tooltip("Accelation of x-axis")]
    public float ACCELATION;

    [Tooltip("Maximum speed of x-axis")]
    public float MAX_SPEED;

    [Tooltip("Initial Position of the Character")]
    public Vector3 INIT_POSITION;

    private int currentJumpCount;

    private enum KeyInput
    {
        UP,
        LEFT,
        RIGHT
    }

    private bool[] keyInputCheck;

    // 생명주기 함수들
    // Awake는 항상 Start 이전에 한 번 호출됩니다. (언제 호출될지는 모름)
    void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    // Awake 이후에 단 한 번 호출됩니다.
    void Start()
    {
        
    }

    // 오브젝트가 활성화될 때마다 호출됩니다.
    // pub-sub 패턴에서 구독할 때 자주 사용됩니다.
    void OnEnable()
    {
        
    }

    // 오브젝트가 비활성화될 때마다 호출됩니다.
    // pub-sub 패턴에서 구독 해지할 때 자주 사용됩니다.
    void OnDisable()
    {
        
    }

    // Update is called once per frame
    // 매 프레임 호출됩니다. 주로 input 이벤트 처리와 그래픽 작업을 합니다.
    void Update()
    {
        GetPlayerKeyInput();
    }

    // 고정 프레임 시간마다 호출됩니다. 물리 연산은 여기서 해야합니다.
    void FixedUpdate()
    {
        PlayerJump();
        PlayerMove();
    }

    private void Init()
    {
        animator = GetComponent<Animator>();
        rgbody = GetComponent<Rigidbody2D>();

        rgbody.position = INIT_POSITION;
        rgbody.velocity = Vector3.zero;

        currentJumpCount = 0;

        keyInputCheck = new bool[Enum.GetNames(typeof(KeyInput)).Length];

        for (int i = 0; i < keyInputCheck.Length; i++)
        {
            keyInputCheck[i] = false;
        }
    }

    private void GetPlayerKeyInput()
    {
        // 사용자의 입력을 받아서 처리합니다.
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
        }
        else
        {

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            
        }
        else
        {

        }
    }

    // 저장된 입력값을 확인해서 플레이어를 움직입니다.
    private void PlayerJump()
    {
        if (keyInputCheck[(int)KeyInput.UP])
        {

        }
    }

    private void PlayerMove()
    {
        if (keyInputCheck[(int)KeyInput.LEFT])
        {

        }
        if (keyInputCheck[(int)KeyInput.RIGHT])
        {

        }
    }


    // 타일맵에 캐릭터가 닿을 때 이벤트를 처리합니다.
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            
        }
    }

    // 타일맵에 캐릭터가 떨어질 때 이벤트를 처리합니다.
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            
        }
    }

    // 타일맵의 지형 외에 다른 오브젝트와 부딪혔을 때 이벤트를 처리합니다.
    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Flag"))
        {
            
        }
        if (trigger.gameObject.CompareTag("Obstacle"))
        {
            
        }
    }
}

