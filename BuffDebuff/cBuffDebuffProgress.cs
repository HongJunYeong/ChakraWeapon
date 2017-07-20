using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBuffDebuffProgress : MonoBehaviour
{

    #region private 변수

    public Information.eBuffDebuff m_eBuffDebuffState;                           //버프, 디버프 상태 (비트연산)
    List<cBuffDebuffInfo> m_listBuffDebuff = new List<cBuffDebuffInfo>();        //버프, 디버프 목록

    #endregion

    private void Update()
    {
        //시간 감소 및 그에 따른 상태 체크
        CheckCountState();

        //버프, 디버프 상태에따른 스탯 변화
        CheckBuffDebuffState();
    }

    /// <summary>
    /// 01.버프 푸쉬
    /// </summary>
    /// <param name="obj">버프, 디버프걸 타겟 오브젝트</param>
    /// <param name="enumName">버프, 디버프의 enum네임(Information.eBuffDebuff.)</param>
    /// <param name="totalTime">버프, 디버프의 총 시간</param>
    public void PushBuffDebuff(GameObject obj, Information.eBuffDebuff enumName, float totalTime)
    {
        //주려는 버프가 현재 캐릭터가 가지고 있을 때(남은 시간이 갱신하려는 시간보다 작으면 갱신 후 Add안함)
        if ((m_eBuffDebuffState & enumName) == enumName)
        {
            IfSameBuffInit(enumName, totalTime);
            return;
        }

        cBuffDebuffInfo buffDebuff = new cBuffDebuffInfo();
        buffDebuff.Init(obj, enumName, totalTime);
        m_listBuffDebuff.Add(buffDebuff);
        m_eBuffDebuffState |= enumName;
    }

    /// <summary>
    /// 02. 시간 감소 맟 시간 = 0 일 때 상태제거하는 메소드
    /// </summary>
    void CheckCountState()
    {
        for (int i = 0; i < m_listBuffDebuff.Count; i++)
        {
            //현재 버프시간을 델타 타임으로 줄임.
            m_listBuffDebuff[i].m_fCurTime -= Time.deltaTime;

            //버프 타임 끝 
            if (m_listBuffDebuff[i].m_fCurTime <= 0)
            {
                //버프 상태 제거 후 리스트에서 삭제.
                m_eBuffDebuffState -= m_listBuffDebuff[i].m_eBuffDebuffName;
                m_listBuffDebuff.RemoveAt(i);

            }
        }
    }

    /// <summary>
    /// 주려는 버프가 현재 캐릭터가 가지고 있을 때 (남은 시간이 갱신하려는 시간보다 작으면 갱신)
    /// </summary>
    /// <param name="enumName">버프 이넘네임</param>
    /// <param name="totalTime">버프 총 시간</param>
    void IfSameBuffInit(Information.eBuffDebuff enumName, float totalTime)
    {
        //해당 버프를 찾음
        for (int i = 0; i < m_listBuffDebuff.Count; i++)
        {
            //해당 버프 이넘네임과 같지 않으면 컨티뉴
            if (m_listBuffDebuff[i].m_eBuffDebuffName != enumName) continue;

            //만약 현재 남은시간이 갱신하려는 시간보다 크다면 초기화 안하고 리턴 
            if (m_listBuffDebuff[i].m_fCurTime > totalTime) return;

            //해당버프를 갱신함
            m_listBuffDebuff[i].m_fCurTime = totalTime;
        }
    }

    
    void CheckBuffDebuffState()
    {
        switch(m_eBuffDebuffState)
        {
            default:
                break;
        }
    }

}


