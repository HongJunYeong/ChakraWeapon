using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class cMonster : MonoBehaviour {

    #region public 변수

    //public Information.eBuffDebuff m_eBuffDebuffState;           //버프, 디버프 상태 (비트연산)

    #endregion

    #region Protected 변수
    //protected List<class명>                                  //버프, 디버프 상태리스트
    public Information.eElement m_eAttributes;                //개체 속성
    public Information.eAnimState m_eAnimState;                 //애니메이션 상태
   
    public int m_nOriginHP, m_nHP;                            //최대체력, 현재체력
    public int m_nOriPhysicalAtk, m_nPhysicalAtk;             //물리공격력, 변화된 물리공격력
    public int m_nOriginPhysicalDefence, m_nPhysicalDefence;  //물리방어력
  
    public int m_nOriginMagicalAttack, m_nMagicalAttack;      //마법공격력, 변화된 마법공격력
    public int m_nOriginMagicalDefence, m_nMagicalDefence;    //마법방어력
 
    public float m_fOriginSpeed, m_fSpeed;                    //이동속도
    public float m_fOriginAttackSpeed, m_fAttackSpeed;        //공격속도

    public float m_nTraceRange;                               //최대 추적범위
    public float m_nAttackRange;                              //최대 공격범위

#endregion


    #region 하위 클래스에서 무조건 구현해야 하는 함수

    protected abstract void Damaged(Information.eElement element, int physicalAttack, int magicalAttack);
    protected abstract void Debuffed(Information.eDebuffList Type, float duration);

    #endregion
}