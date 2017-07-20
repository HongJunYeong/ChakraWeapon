using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cItemInformation : MonoBehaviour {

    #region public 변수

    public Information.eItemType m_eItemType;                           //아이템 타입
    public Information.eItemPart m_eItemArmorPart;                      //방어구 부위
    public Information.eElement m_eItemElement;                         //아이템 속성

    public int m_nPrice;                                                //가격
    public int m_nIdNumber;                                             //아이템 고유번호
    public int m_nIncreaseHp;                                           //최대체력 증가량
    public int m_nIncreaseChakra;                                       //최대마나 증가량

    public float m_fMoveSpeed;                                          //이동속도

    public int m_nHpRegenRate;                                          //초당 체력 회복량
    public int m_nChakraRegenRate;                                      //초당 마나 회복량

    public int m_nPhysicalArmor;                                        //물리 방어력증가
    public int m_nMasicalArmor;                                         //마법 방어력증가

    public string m_sName;                                              //이름
    public string m_sEnglishName;                                       //영문이름
    public string m_sInfo;                                              //설명

    #endregion

    ///--------------------------------------------------------------------
    ///                             protected:
    ///--------------------------------------------------------------------
    ///


    #region 스탯 공식

    /// <summary>
    /// 방어력 공식 : log(1 + 방어력/1000) * 100;
    /// 방어력 최대 범위 = 0 ~ 9000;
    /// 플레이어 최대 방어력 2162 
    /// 방어율 50% = 2162
    /// 방어율 25% = 778;
    /// </summary>

    #endregion
}
