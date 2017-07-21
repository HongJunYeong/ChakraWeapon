using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 차크라 10회복
/// </summary>
public class c30_BuffBasic : MonoBehaviour {

    // 단순히 차크라 30회복이라 콜라이더 생성관련 물어보기

    #region 변수

    [Tooltip("스킬데이터 베이스 얕은복사")]            private cSkillInformation m_cSkillInformation;
    [Tooltip("현재 쿨 타임")]                          private float m_fCurCoolTime = 0.0f;
    [Tooltip("현재 오브젝트가 활성화 되있는지?")]      private bool m_isActivated = false;

    #endregion

    void Awake()
    {
        //스킬데이터 베이스 얕은복사
        m_cSkillInformation = cSkillDataBase.Instance.m_dictionarySkillDataBase[30];
    }

    void Update()
    {

        //쿨타임 줄이기
        if (m_isActivated)  m_fCurCoolTime -= Time.deltaTime;     
    }


    public void init()
    {
        //스킬 쿨타임
        //m_fCurCoolTime = m_cSkillInformation.m_

        //차크라 증가
        cCharacterInformation.Instance.m_nChkra += 10;

        //쿨타임 줄이기 활성화
        m_isActivated = true;
    }

}
