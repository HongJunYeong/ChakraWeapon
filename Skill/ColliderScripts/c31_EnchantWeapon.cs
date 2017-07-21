using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기 크기 증가
/// </summary>
public class c31_EnchantWeapon : MonoBehaviour {

    #region 변수

    [Tooltip("근접무기 오브젝트")]                  public GameObject m_objMeleeWeaPon;
    //[Tooltip("원거리무기 오브젝트")]              public GameObject m_objRangeWeapon;

    [Tooltip("스킬데이터 베이스 얕은복사")]         private cSkillInformation m_cSkillInformation;             
    [Tooltip("현재 쿨 타임")]                       private float m_fCurCoolTime = 0.0f;
    [Tooltip("현재 오브젝트가 활성화 되있는지?")]   private bool m_isActivated = false;

    #endregion

    void Awake()
    {
        //스킬데이터 베이스 얕은복사
        m_cSkillInformation = cSkillDataBase.Instance.m_dictionarySkillDataBase[31];
    }

    void Update()
    {
        //쿨타임 남아 있다면 무기 크기는 1.2배
        if(m_fCurCoolTime > 0)
        {
            m_objMeleeWeaPon.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        else
        {
            m_isActivated = false;
            gameObject.SetActive(false);
        }


        //쿨타임 줄이기
        if(m_isActivated)   m_fCurCoolTime -= Time.deltaTime;
        
    }

    public void init()
    {
        //스킬 쿨타임
        //m_fCurCoolTime = m_cSkillInformation.m_

        //쿨타임 줄이기 활성화
        m_isActivated = true;
    }

}
