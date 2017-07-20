using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c30_BuffBasic : MonoBehaviour {

    // 단순히 차크라 30회복이라 콜라이더 생성안해도됨

    #region 변수

    private cSkillInformation m_cSkillInformation;             //스킬데이터 베이스 얕은복사
    float m_fCurCoolTime = 0.0f;
    #endregion

    void Awake()
    {
        //스킬데이터 베이스 얕은복사
        m_cSkillInformation = cSkillDataBase.Instance.m_dictionarySkillDataBase[30];
    }

    void Update()
    {

    }

    public void init()
    {
        cCharacterInformation.Instance.m_nChkra += 10;

        //스킬 쿨타임
        //m_fCurCoolTime = m_cSkillInformation.m_
    }

}
