using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMeleeSkill : MonoBehaviour {

    [Tooltip("검 오브젝트")]                          public GameObject m_objSword;
    [Tooltip("기본공격시 생성될 콜라이더")]         public GameObject m_objMeleeBasicCollider;

    [Tooltip("돌진시 생성될 콜라이더")]             public GameObject m_objRushCollider;


    /// <summary>
    /// 00 기본공격 Animation 시작시 이벤트발생되는 메소드
    /// </summary>
    void Num00_MelleBasic_Start()
    {
        //검오브젝트 키기
        m_objSword.SetActive(true);

        //검 콜라이더 켜기
        m_objMeleeBasicCollider.SetActive(true);
    }

    /// <summary>
    /// 00 기본공격 Animation 마지막에 이벤트발생되는 메소드
    /// </summary>
    void Num00_MelleBasic_End()
    {
        //검 콜라이더 끄기
        m_objSword.SetActive(false);
    }

    /// <summary>
    /// 01 러쉬 Animation 시작시 이벤트발생되는 메소드
    /// </summary>
    void Num01_Rush_Start()
    {
        //콜라이더 켜기
        m_objRushCollider.SetActive(true);
    }

    /// <summary>
    /// 01 러쉬 Animation 마지막에 이벤트발생되는 메소드
    /// </summary>
    void Num01_Rush_End()
    {
        //콜라이더 끄기
        m_objRushCollider.SetActive(false);
    }
}
