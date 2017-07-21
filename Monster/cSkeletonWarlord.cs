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
        m_animator = GetComponentInChildren<Animator>();
        m_rigidBody = GetComponent<Rigidbody>();
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_player = GameObject.FindWithTag("Player");

        StartCoroutine(ChecktState());
        StartCoroutine(ActionState());
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
        m_navMeshAgent.isStopped = true;
        m_animator.SetBool("Run", false);
    }

    private void Run()
    {
        if (m_isAttack) return;

        m_navMeshAgent.isStopped = false;
        m_navMeshAgent.SetDestination(m_player.transform.position);
        m_animator.SetBool("Run", true);
    }

    private void NormalAttack()
    {
        m_navMeshAgent.isStopped = true;

        if(!m_isAttack)
        {
            m_isAttack = true;
            transform.LookAt(m_player.transform.position);
            m_animator.SetTrigger("NormalAttack");
            StartCoroutine(WaitForNormalAttack(1.0f));
        }

    }

    #endregion

    #region 코루틴
    IEnumerator ChecktState()
    {
        while(true)
        {
            float fDist = Vector3.Distance(transform.position, m_player.transform.position);

            if(fDist <= 2.0f)
            {
                m_eAnimState = Information.eAnimState.NORMALATTACK;
            }
            else if(fDist <= 10.0f)
            {
                m_eAnimState = Information.eAnimState.RUN;
            }
            else
            {
                m_eAnimState = Information.eAnimState.IDLE;
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
