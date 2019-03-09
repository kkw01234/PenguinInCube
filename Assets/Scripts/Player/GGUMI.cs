using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGUMI : MonoBehaviour
{
    public static GGUMI instance;

    public Joystick joystick;
    public Joybutton joyJumpButton;
    public Joybutton joyActionButton;
    private Animation ani;
    private Rigidbody rigid;

    private bool isOver = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        joystick = FixedJoystick.instance;
        joyJumpButton = joystick.GetComponentInParent<Canvas>().GetComponentsInChildren<Joybutton>()[0];
        joyActionButton = joystick.GetComponentInParent<Canvas>().GetComponentsInChildren<Joybutton>()[1];
        ani = GetComponent<Animation>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!GameManager.instance.isGameStart && !isOver)
        {
            ani.Play("gameover");
            rigid.useGravity = false;
            isOver = true;
        }
        else if (isOver && !ani.isPlaying)
            Destroy(this.gameObject);
        else if (GameManager.instance.isGameStart)
        {
            PlayerButton();
            PlayerMoveController();
        }
    }

    public Foot foot;
    private bool isFeetGrounded = false;
    private Vector3 moveDir;
    public float moveSpeed;
    public float jumpPower;

    void PlayerMoveController()
    {
        //땅위에 있는지 판단
        if (foot.isGround) //양쪽 발 중 하나라도 닿으면 땅위에 있다고 판정
            isFeetGrounded = true;
        else if (!foot.isGround) //양쪽 발이 다 떨어졌을 경우 땅위에 없다고 판정
            isFeetGrounded = false;

        //이동 방향 계산
        if (joystick.isActiveAndEnabled)
            moveDir = Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical; //조이스틱
        else
            moveDir = Vector3.right * Input.GetAxisRaw("Horizontal") +
                      Vector3.forward * Input.GetAxisRaw("Vertical"); //키보드
        //수평 이동
        if (moveDir != Vector3.zero)
        {
            transform.eulerAngles =
                Vector3.up * Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg; //움직이는 방향을 바라보기
            transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
        }

        //애니메이션 및 점프 판정
        if (isFeetGrounded && !ani.IsPlaying("jump")) //땅위에 있으면서 점프중이 아닐경우 (Case)
        {
            if (ani.IsPlaying("falling")) //떨어지는 중일때 땅에 닿으면 착지 모션
                ani.CrossFade("landing");
            else if (moveDir == Vector3.zero) //가만히 서있을 경우 정지 모션
                ani.CrossFade("idle");
            else //가만히 있지 않을 경우 걷기 모션
                ani.CrossFade("walk");
            if (joyJumpButton.isPressed) //"점프"버튼을 누른상태일 경우 점프 모션
            {
                ani.CrossFade("jump");
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); //위로 힘을 가해서 점프
            }
        }
        else if (ani.IsPlaying("jump")) //땅에 닿아있지 않으면서 점프중일 경우
        {
            //속력이 양수에서 음수로 바뀌는 부분이 최고점
            if (rigid.velocity.y > 0 && !ani.IsPlaying("jump")) //상승 중 점프 모션
                ani.CrossFade("jump");
            else if (rigid.velocity.y < 0 && !ani.IsPlaying("falling")) //하강 중 낙하 모션
                ani.CrossFade("falling");
        }
        else if (!isFeetGrounded && rigid.velocity.y < 0 && !ani.IsPlaying("falling")) //그냥 떨어지는 중일 경우
        {
            ani.CrossFade("falling");
        }
    }

    void PlayerButton()
    {
        //점프 버튼
        if (Input.GetButton("Jump"))
            joyJumpButton.isPressed = true;
        else if(Input.GetButtonUp("Jump"))
            joyJumpButton.isPressed = false;
        //액션 버튼
        if (Input.GetButtonDown("Action"))
            joyActionButton.isPressed = true;
        else if (Input.GetButtonUp("Action"))
            joyActionButton.isPressed = false;
    }
}
