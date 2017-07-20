using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cSkillInformation : MonoBehaviour {

    #region public변수들

    public Image m_imageIcon;                //스킬 아이콘 이미지
    public Information.eElement m_eElement;                 //스킬 속성
    public Information.eSkillType m_eType;                    //스킬 타입
    public Information.eSkillRange m_eAttackRange;             //스킬 공격 범위

    public int m_nIdNumber;                //스킬 고유 번호 
    public int m_nChainLevel;              //스킬 연계 단계
    public int m_nChakraCost;              //차크라 소모 비용
    public int m_nDamageType;              //공격 속성      
    public int m_nOptionNumber;            //옵션 번호

    public float m_fDamage;                  //스킬 데미지 (영웅 스탯 비례)

    public string m_sName;                    //스킬 이름(영문)
    public string m_sKoreanName;              //스킬 한글 이름(UI 표현용)
    public string m_sInfo;                    //스킬 정보(설명)
    public string m_sOption_1_Info;           //옵션 1 정보
    public string m_sOption_2_Info;           //옵션 2 정보
    public string m_sOption_3_Info;           //옵션 3 정보


    #endregion
}
