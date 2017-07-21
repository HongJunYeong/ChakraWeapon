using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cSkeletonWarlord : cMonster {

    #region private필드
    private Animator m_animator;
    private Rigidbody m_rigidBody;
    private NavMeshAgent m_navMeshAgent;
    private GameObject m_player;

    private bool m_isAttack = false;
    #endregion

    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponentInChildren<Animator>();        //애니메이터 컴포넌트
        m_rigidBody = GetComponent<Rigidbody>();                //리지드 바디 컴포넌트
        m_navMeshAgent = GetComponent<NavMeshAgent>();          //네비게이션 메쉬 컴포넌트
        m_player = GameObject.FindWithTag("Player");            //플레이어 게임오브젝트

        StartCoroutine(ChecktState());                          //상태를 체크하는 코루틴을 실행한다
        StartCoroutine(ActionState());                          //상태를 실행하는 코루튼일 실행한다
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    #region 메소드

    protected override void Damaged(Information.eElement element, int physicalAttack, int magicalAttack)
    {

    }

    protected override void Debuffed(Information.eDebuffList Type, float duration)
    {

    }

    private void Idle()
    {
        m_navMeshAgent.isStopped = true;            //네비게이션 추적을 끈다
        m_animator.SetBool("Run", false);           //IDLE 애니메이션으로 돌아간다
    }

    private void Run()
    {
        if (m_isAttack) return;                     //공격중이면 리턴한다

        m_navMeshAgent.SetDestination(m_player.transform.position);     //목적지를 플레이어 포지션으로 지정한다
        m_navMeshAgent.isStopped = false;                               //네비게이션 추적을 킨다
        m_animator.SetBool("Run", true);                                //달리기 애니메이션을 킨다
    }

    private void NormalAttack()
    {
        m_navMeshAgent.isStopped = true;                                //네비게이션 추적기능을 끈다

        if(!m_isAttack)                                                 //만약 공격중이 아니라면
        {
            m_isAttack = true;                                          //공격중으로 바꾸고
            transform.LookAt(m_player.transform.position);              //공격대상을 바라본다
            m_animator.SetTrigger("NormalAttack");                      //공격 애니매이션을 실행한다
            StartCoroutine(WaitForNormalAttack(1.0f));                  //공격애니메이션이 끝날때까지 기다리는 코루틴을 실행한다
        }

    }

    #endregion

    #region 코루틴
    IEnumerator ChecktState()
    {
        while(true)
        {
            float fDist = Vector3.Distance(transform.position, m_player.transform.position);        //플레이와 몬스터간의 거리를 구한다

            if(fDist <= 2.0f)                                           //거리가 2.0f 이하면
            {       
                m_eAnimState = Information.eAnimState.NORMALATTACK;     //상태를 NormalAttack으로 변경한다
            }
            else if(fDist <= 10.0f)                                     //거리가 10.0
            {
                m_eAnimState = Information.eAnimState.RUN;              //상태를 Run으로 변경한다
            }
            else                                                        //아무것도 속해 있지 않다면
            {
                m_eAnimState = Information.eAnimState.IDLE;             //상태를 Idle로 바꾼다
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ActionState()
    {
        while(true)
        {
            switch(m_eAnimState)
            {
                case Information.eAnimState.IDLE:
                    Idle();
                    break;
                case Information.eAnimState.RUN:
                    Run();
                    break;
                case Information.eAnimState.NORMALATTACK:
                    NormalAttack();
                    break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator WaitForNormalAttack(float time)
    {
        yield return new WaitForSeconds(time);

        m_isAttack = false;
    }
    #endregion
}
