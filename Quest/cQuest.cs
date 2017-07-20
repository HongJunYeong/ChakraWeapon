using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cQuest : MonoBehaviour {

    #region public:

    public Information.eDungeonName m_eDungeonName;         //수행 던전
    public Information.eQuestCondition m_eQuestCondition;   //퀘스트 발동 조건 (근접 1단계인지, 원거리 2단계 인지~ 등등)

    public int m_nRewardMoney;                              //보상금   
    public int m_nRewardPhysicalAttack;                     //보상 물공 스탯 (0일수도 있음)
    public int m_nRewardMagicalAttack;                      //보상 마공 스탯
    public int m_nNumber;                                   //고유번호

    public string m_sName;                                  //제목
    public string m_sInfo;                                  //설명
    public string m_sNPC_Name;                              //퀘스트를 가지고 있을 NPC이름

    
    public string m_sRewardSkillName;                       //보상할 스킬 이름
    public string m_sQuestItemName;                         //퀘스트 수행시 NPC가 주는 아이템  
    
                  

    #endregion
    ///--------------------------------------------------------------------
    ///                             protected:
    ///--------------------------------------------------------------------
}
