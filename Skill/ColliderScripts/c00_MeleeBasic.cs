using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일반공격 클래스
/// </summary>
public class c00_MeleeBasic : MonoBehaviour {

    #region 변수

    private cSkillInformation m_cSkillInformation;             //스킬데이터 베이스 얕은복사

    #endregion

    void Awake()
    {
        //스킬데이터 베이스 얕은복사
        m_cSkillInformation = cSkillDataBase.Instance.m_dictionarySkillDataBase[0];     
    }

    void OnCollisionEnter(Collision coll)
    {
        //적일 때만 부딪치기
        if (coll.gameObject.tag != "Enemy") return;

        //데미지 계산
        float damage = ((float)cCharacterInformation.Instance.m_nPhysicalAtk * m_cSkillInformation.m_fDamage) +
            ((float)cCharacterInformation.Instance.m_nTotalComboNum * 0.05f + 1.0f);

        print(damage);

        //데미지 주기

        //디버프 없음

        //이펙트, 사운드 생성

        gameObject.SetActive(false);
    }
}
