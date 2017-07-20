using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class cBuffDebuffInfo : MonoBehaviour
{

    #region public 변수

    public GameObject m_objTarget;                                       //버프, 디버프 타겟
    public Information.eBuffDebuff m_eBuffDebuffName;                   //버프, 디버프 이넘네임

    public float m_fTotalTime;                                           //총시간
    public float m_fCurTime;                                             //현재 시간

    #endregion

    #region 함수들


    /// <summary>
    /// 각 버프 초기화
    /// </summary>
    /// <param name="obj">대상 오브젝트</param>
    /// <param name="enumName">버프, 디버프 이넘네임 infomation.eBuffDebuff</param>
    /// <param name="totalTime">버프, 디버프 총 시간</param>
    public void Init(GameObject obj, Information.eBuffDebuff enumName, float totalTime)
    {
        m_objTarget = obj;
        m_eBuffDebuffName = enumName;

        m_fTotalTime = totalTime;
        m_fCurTime = m_fTotalTime;
    }
    #endregion
}

