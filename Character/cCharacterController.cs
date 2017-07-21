using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCharacterController : MonoBehaviour {
    #region private 필드

    public GameObject m_objSword;

    private cBuffDebuffProgress m_cBuffDebuffProgress;      //캐릭터 버프,디버프 스크립트
    private CharacterController m_chatacterController;      //캐릭터 컨트롤러


    private Vector3 m_vecMoveDir;                           //캐릭터가 이동할 방향

    private float m_fHorizontal;                            // W,S 키가 눌렸을때 -1 ~ 1 까지의 값을 받아올 변수
    private float m_fVertical;                              // A,D 키가 눌렸을때 -1 ~ 1 까지의 값을 받아올 변수
    private float m_fRotX;                                  //카메라 x축 회전 누적값

    private bool m_isJump = false;                          //점프중인지를 체크하는 변수
    private bool m_isLand = true;                           //지상에 있는지 체크하는 변수
    private bool m_isCrouch = false;                        //앉았는지를 체크하는 변수
    private bool m_isRolling = false;                       //구르는 중인지를 체크하는 변수

    #endregion

    #region public 필드

    public Animator m_anim;                                 //애니메이터
    public Transform m_TrCamFollowPoint;                    //카메라 팔로우 포인트

    public float m_fMoveSpeed = 10.0f;                      //캐릭터의 속도 
    public float m_fJumpHeight = 5.0f;                      //캐릭터의 점프 높이
    public float m_fRotYSpeed = 50.0f;                      //캐릭터의 회전 속도
    public float m_fRotXSpeed = 100.0f;                     //카메라 Y축 회전속도
    public float m_fRollSpeed = 0.2f;                       //캐럭턱의 구르기 속도
    public float m_fGravity = -9.81f;                       //캐릭터가 받게될 중력변수
    public float m_fMinRotX = 20.0f;                        //카메라 X축 MIN값
    public float m_fMaxRotX = -35.0f;                       //카메라 x축 MAX값

    #endregion

    #region 콜백 메소드

    // Use this for initialization
    void Start () {
        m_cBuffDebuffProgress = GetComponent<cBuffDebuffProgress>();
        m_chatacterController = GetComponent<CharacterController>();    //스크립트 시작시 자신의 캐릭터 컴포넌트를 받아온다.
        m_anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            m_anim.SetBool("MoveToIdle", true);
            m_anim.SetTrigger("MeleeBasic");
            m_anim.SetBool("MeleeBasicCombo", true);
        }
        else
        {
            m_anim.SetBool("MeleeBasicCombo", false);
        }


        Move();
        Rotate();
        Jump();
        Crouch();
        Rolling();
    }

    #endregion

    #region 메소드
    /// <summary>
    /// 캐릭터를 이동 시키는 메소드
    /// </summary>
    public void Move()
	{ 
		if (!m_isLand || m_isCrouch || m_isRolling || cCharacterInformation.Instance.m_isDontMove)
        {
            m_vecMoveDir = (transform.up * m_fGravity);
            m_chatacterController.Move(m_vecMoveDir * Time.deltaTime);
        }
       else 
        {
			m_fHorizontal = Input.GetAxis("Horizontal");                         //W,S 키의 -1 ~ 1 까지의 값을 받아온다.
            m_fVertical = Input.GetAxis("Vertical");                             //A,D 키의 -1 ~ 1 까지의 값을 받아온다.

            if (m_fHorizontal < 0)                                              //
            {                                                                   //캐릭터의 이동 애니메이션을 실행 시킨다
                m_anim.SetInteger("HRun", -1);                                  //HRun은 좌우 이동
                m_anim.SetBool("MoveToIdle", false);                            //VRun은 앞뒤 이동
            }
            else if (m_fHorizontal > 0)
            {
                m_anim.SetInteger("HRun", 1);
                m_anim.SetBool("MoveToIdle", false);
            }
            else m_anim.SetInteger("HRun", 0);

            if (m_fVertical < 0)
            {
                m_anim.SetInteger("VRun", -1);
                m_anim.SetBool("MoveToIdle", false);
            }
            else if (m_fVertical > 0)
            {
                m_anim.SetInteger("VRun", 1);
                m_anim.SetBool("MoveToIdle", false);
            }
            else m_anim.SetInteger("VRun", 0);

            if (m_fVertical == 0 && m_fHorizontal == 0)
            {
                m_anim.SetBool("MoveToIdle", true);
            }


            m_vecMoveDir = ((transform.forward * m_fVertical) +
                (transform.right * m_fHorizontal));

            m_vecMoveDir.Normalize();
            m_vecMoveDir *= m_fMoveSpeed;
            m_vecMoveDir.y = m_fGravity;


            m_chatacterController.Move(m_vecMoveDir * Time.deltaTime);            //캐릭터의 이동방향으로 이동시킨다
        }
    }

    /// <summary>
    /// 캐리턱를 회전 시키는 메소드
    /// </summary>
    public void Rotate()
    {
        if (m_isRolling) return;

        transform.Rotate(transform.up * Input.GetAxis("Mouse X") * m_fRotXSpeed * Time.deltaTime);

        float fRotY = -Input.GetAxis("Mouse Y");
        m_fRotX = Mathf.Clamp(m_fRotX + fRotY * Time.deltaTime * m_fRotXSpeed,
            m_fMaxRotX, m_fMinRotX);

        if (m_fRotX > m_fMaxRotX &&
            m_fRotX < m_fMinRotX)
        {
            m_TrCamFollowPoint.Rotate(Vector3.right,fRotY * Time.deltaTime * m_fRotXSpeed);
        }

    }

    /// <summary>
    /// 점프 메소드
    /// </summary>
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!m_isJump && m_isLand && !m_isRolling && !cCharacterInformation.Instance.m_isDontJump)
            {
                m_isJump = true;
                m_anim.SetTrigger("Jump");
                StartCoroutine(JumpUp());
            }
        }

    }

    /// <summary>
    /// 캐릭터의 앉기를 수행할 메소드
    /// </summary>
    public void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl)
            && !m_isJump 
            && m_isLand
            && m_fHorizontal == 0
            && m_fVertical == 0
            && !m_isRolling
            && !cCharacterInformation.Instance.m_isDontMove)
        {
            if (!m_isCrouch)
            {
                m_anim.SetBool("Crouch", true);
                m_isCrouch = true;
            }
            else
            {
                m_anim.SetBool("Crouch", false);
                m_isCrouch = false;
            }
        }
    }

    /// <summary>
    /// 캐릭터 구르기를 실행할 메소드
    /// </summary>
    public void Rolling()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)
            && !m_isRolling 
            && !m_isJump 
            && m_isLand
            && !cCharacterInformation.Instance.m_isDontMove)
        {
            if (m_fVertical == 0 && m_fHorizontal == 0)
            {
                m_anim.SetTrigger("RollForward");
                Vector3 Dir = (transform.forward) + (transform.up * m_fGravity);

                StartCoroutine(TranslateRolling(Dir));
            }
            else if (m_fVertical > 0 && m_fHorizontal == 0)
            {
                m_anim.SetTrigger("RollForward");
                Vector3 Dir = (transform.forward) + (transform.up * m_fGravity);

                StartCoroutine(TranslateRolling(Dir));
            }
            else if (m_fVertical < 0 && m_fHorizontal == 0)
            {
                m_anim.SetTrigger("RollBackward");
                Vector3 Dir = (transform.forward * -1.0f) + (transform.up * m_fGravity);

                StartCoroutine(TranslateRolling(Dir));
            }
            else if (m_fHorizontal > 0 && m_fVertical == 0)
            {
                m_anim.SetTrigger("RollRight");
                Vector3 Dir = (transform.right) + (transform.up * m_fGravity);

                StartCoroutine(TranslateRolling(Dir));
            }
            else if (m_fHorizontal < 0 && m_fVertical == 0)
            {
                m_anim.SetTrigger("RollLeft");
                Vector3 Dir = (transform.right * -1.0f) + (transform.up * m_fGravity);

                StartCoroutine(TranslateRolling(Dir));
            }
        }
    }

    #endregion

    #region 코루틴

    /// <summary>
    /// 0.1초를 주기로 중력가속도를 적용시켜 점프시키는 코루틴 메소드
    /// </summary>
    /// <returns> 0.1 초를 주기로 스위칭한다</returns>
    public IEnumerator JumpUp()
    {
        float fGravity = m_fGravity;
        float fJumpHeight = m_fJumpHeight;
        float fFactor = 0.0f;

        m_fGravity = 0.0f;

        while (true)
        {
            fJumpHeight -= 0.8f + fFactor;

           m_fGravity = fJumpHeight;

           fFactor += 0.05f;

            if (fJumpHeight <= 0.0f)
            {
                StartCoroutine(JumpDown(fGravity));
                break;
            }

        yield return new WaitForSeconds(0.1f);
           
        }
    }

    /// <summary>
    /// 0.1 초를 주기로 중력가속도를 적용시켜 지상으로 착지시키는 코루틴 메소드
    /// </summary>
    /// <param name="gravity"> 최초 중력값</param>
    /// <returns>0.1초를 주기로 스위칭 한다</returns>
    public IEnumerator JumpDown(float gravity)
    {
        float fGravity = gravity;
        float fJumpDownHeight = 0.0f;
        float fFactor = 0.0f;

        m_fGravity = 0.0f;

        while(true)
        {
            fJumpDownHeight -= 0.8f + fFactor;
            m_fGravity = fJumpDownHeight;

            fFactor += 0.05f;

            if (m_chatacterController.isGrounded)
            {
                m_anim.SetTrigger("Land");
                m_isJump = false;
                m_isLand = false;

                StartCoroutine(LandToIdle());

                m_fGravity = fGravity;
                break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// 착지 애니메이션동안 이동잠금을 시킬 메소드
    /// </summary>
    /// <returns></returns>
    IEnumerator LandToIdle()
    {
        yield return new WaitForSeconds(1.0f);
        m_isLand = true;

    }


    /// <summary>
    /// 구르기 이동을 실행할 코루틴 메소드
    /// </summary>
    /// <returns></returns>
    IEnumerator TranslateRolling(Vector3 Dir)
    {
        m_isRolling = true;

        int count = 0;

        yield return new WaitForSeconds(0.2f);

        while (true)
        {
            m_chatacterController.Move(Dir * m_fRollSpeed);
            yield return new WaitForSeconds(0.01f);

            count++;

            if (count >= 20)
            {
                m_anim.SetInteger("HRun", 0);
                m_anim.SetInteger("VRun", 0);
                m_anim.SetBool("MoveToIdle", true);

                yield return new WaitForSeconds(0.6f);
                
                m_isRolling = false;
                break;
            }
        }
    }

    #endregion
}
