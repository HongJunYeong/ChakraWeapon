﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c01_Rush : MonoBehaviour {

    //토탈 콤보 증가, 차크라 소모는 cCaster에서 함.

    #region 변수
    private Vector3 m_vBasicPt;                                //첫 위치값 저장
    private Quaternion m_quaBasicQuaternion;                   //첫 회전값 저장
    private const int m_nSkillIndex = 0;                       //데이터 베이스 로드하기 위한 스킬 넘버 
    private bool m_isActivate = false;                         //다시 액티베이트 됐을 때 한번만 실행되게 하기 위함.

    #endregion

    #region public

    public cSkillInformation m_cSkillInformation;              //스킬데이터 베이스 얕은복사

    #endregion

    void Awake()
    {
        //첫 위치, 회전값 저장
        m_vBasicPt = gameObject.transform.position;
        m_quaBasicQuaternion = gameObject.transform.rotation;
    }

    void Update()
    {
        //스킬 내용 구현
        MeleeBasic();
    }

    void OnCollisionEnter(Collision coll)
    {
        float damage = (cCharacterInformation.Instance.m_nPhysicalAtk * m_cSkillInformation.m_fDamage) +
            (cCharacterInformation.Instance.m_nTotalComboNum * 0.05f + 1.0f);

        //디버프 없음


        //이펙트, 사운드 생성


        //이전상태로 돌리기
        Reset();

        //콜라이더 끔.
        gameObject.SetActive(false);
    }


    /// <summary>
    /// 00. 근거리 기본 내용 구현
    /// </summary>
    void MeleeBasic()
    {
        //한번만 실행되야 할 것들
        if (!m_isActivate)
        {
            //이펙트 생성.

            //애니메이션시 못움직임
            cCharacterInformation.Instance.m_isDontMove = true;

            //다시 이동속도 올리지 않기 위해 트루로.
            m_isActivate = true;
        }
    }

    /// <summary>
    /// 이전 상태로 되돌리기
    /// </summary>
    void Reset()
    {
        //포지션, 회전 돌려놓기()
        gameObject.transform.position = m_vBasicPt;
        gameObject.transform.rotation = m_quaBasicQuaternion;

        //그 외 실행된 것들 돌려놓기
    }

}
