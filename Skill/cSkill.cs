using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSkill : MonoBehaviour {

    public cSkillInformation m_cSkillInformation;

    #region protected virtual 함수

    /// <summary>
    /// 스킬 공통 변수 초기화 메소드
    /// </summary>
    /// <param name="skillIndex">스킬 고유번호</param>
    /// 
    protected virtual void FirstAwake(int skillIndex)
    {

        m_cSkillInformation.m_fDamage = cSkillDataBase.Instance.m_dictionarySkillDataBase[skillIndex].m_fDamage;                           //스킬 데미지 (영웅 스탯 비례)

        //캐릭터에서 처리해야함.
        m_cSkillInformation.m_nChainLevel = cSkillDataBase.Instance.m_dictionarySkillDataBase[skillIndex].m_nChainLevel;                   //스킬 연계 단계

        //나중에 구현 해야함.
        m_cSkillInformation.m_nDamageType = cSkillDataBase.Instance.m_dictionarySkillDataBase[skillIndex].m_nDamageType;                   //공격 속성      
        m_cSkillInformation.m_nOptionNumber = cSkillDataBase.Instance.m_dictionarySkillDataBase[skillIndex].m_nOptionNumber;               //옵션 번호


        //아직 쓸지 않쓸지 모름
        //m_eElement = cSkillDataBase.Instance.m_dictionarySkillDataBase[m_nSkillIndex].m_eElement;                       //스킬 속성
        //m_eType = cSkillDataBase.Instance.m_dictionarySkillDataBase[m_nSkillIndex].m_eType;                             //스킬 타입
        //m_eAttackRange = cSkillDataBase.Instance.m_dictionarySkillDataBase[m_nSkillIndex].m_eAttackRange;               //스킬 공격 범위
        //m_nIdNumber = cSkillDataBase.Instance.m_dictionarySkillDataBase[m_nSkillIndex].m_nIdNumber;                     //스킬 고유 번호 
    }

    protected virtual void GeneralCollision()
    {
        //데미지 계산 (총 콤보에 따라 데미지 강해짐.)
        m_cSkillInformation.m_fDamage = cCharacterInformation.Instance.m_nPhysicalAtk * (cCharacterInformation.Instance.m_nTotalComboNum * 0.05f + 1.0f);

        //데미지 줘야함.
        //(적 클래스에서 데미지 받는 함수 구현하면 그 함수 불러와서 데미지 줘야함.)

        //캐릭터 상태변화
        cCharacterInformation.Instance.m_eAnimState = Information.eAnimState.IDLE;
    }


    #endregion
}
