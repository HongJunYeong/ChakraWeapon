using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour {


    #region 공통ENUM

    
    /// <summary>
    /// 원소속성
    /// </summary>
    public enum eElement
    {
        //기본속성
        NORMAL = 0,              // 불 >> 잎 >> 물 >> 불
        FIRE,
        WATER,
        GRASS,
        //시너지 속성
        WIND,                   // 불과 시너지
        ELECTRONIC,             // 물과 시너지
        LIGHT,                  // 잎과 시너지
    };

    /// <summary>
    /// 애니메이션 상태
    /// </summary>
    public enum eAnimState
    {
        IDLE = 0,
        RUN,
        NORMALATTACK,
        RUSH,
        SHOOTINGSTAR,
        SKILL3,
        SKILL4,
        SKILL5,
        SKILL6,
        SKILL7,
        SKILL8,
        SKILL9,
        SKILL10,
        DIE,
    };

    /// <summary>
    /// 디버프 종류
    /// </summary>
    public enum eDebuffList
    {
        NONE = 0,
        STUN,
    }

    /// <summary>
    /// 던전 이름
    /// </summary>
    public enum eDungeonName
    {
        FINALBOSS,
    }

    /// <summary>
    /// 왼클릭인지 오른클릭인지 클릭 안했는지
    /// </summary>
    public enum eClick
    {
        NONE = 0,
        L_CLICK,
        R_CLICK,
    }

    #endregion

    #region 스킬 관련 ENUM

    /// <summary>
    /// 스킬 타입
    /// </summary>
    public enum eSkillType
    {
       MELEE = 0,                     //근거리 스킬
       RANGE,                         //원거리 스킬
       OTHER,                         //보조(방어, 회피, 버프) 스킬
    }

    /// <summary>
    /// 타격 범위
    /// </summary>
    public enum eSkillRange
    {
        SINGLE = 0,
        CIRCLE,                        //주변원형 타겟
        CORN,                          //방사형 타켓
        STRAIGHT,                      //직선 타겟
    }
    #endregion

    /// <summary>
    /// 버프, 디버프 비트연산 이넘
    /// </summary>
    public enum eBuffDebuff
    {
        NORMAL                  = 0,
        STUN                    = 1 << 0, //스턴
        HEMORRHAGE              = 1 << 1, //출혈
        CRITICALRATIOINCRESE    = 1 << 2, //크리티컬확률 증가
        DECREASEARMOR_50        = 1 << 3, //50%방어감소
        INVINCIBLE              = 1 << 4, //무적
        FROZEN                  = 1 << 5, //빙결
        BLINDNESS               = 1 << 6, //실명
        BURN                    = 1 << 7, //화상
        INCREASERANGE           = 1 << 8, //사거리 증가
        HIDE                    = 1 << 9, //은신
    }

    #region 아이템 관련 ENUM

    /// <summary>
    /// 아이템 부위
    /// </summary>
    public enum eItemPart
    {
        NONE = 0,
        ARMOR,
        BOOTS,
        PANTS,
        GLOVES,
        HELMET,
    }

    /// <summary>
    /// 아이템 타입
    /// </summary>
    public enum eItemType
    {
        NONE = 0,
        ARMOR,
        POTION,
        QUESTITEM,
        SKILLBOOK,
        SKILL_OPTION,                //스킬 속성 부여 아이템
    }

    #endregion

    #region 그 외 ENUM

    /// <summary>
    /// 퀘스트 발동조건
    /// </summary>
    public enum eQuestCondition
    {
        MELEE_LEVEL1 = 1 << 0,                  //
        MELEE_LEVEL2 = 1 << 1,                  //
        MELEE_LEVEL3 = 1 << 2,

        RANGE_LEVEL1 = 1 << 3,
        RANGE_LEVEL2 = 1 << 4,
        RANGE_LEVEL3 = 1 << 5,

        BUFF_LEVEL1 = 1 << 6,
        BUFF_LEVEL2 = 1 << 7,
        BUFF_LEVEL3 = 1 << 8,
    }

    #endregion


    //private static Information sInstatance;
    //public static Information Instance
    //{
    //    get
    //    {
    //        if (sInstatance == null)
    //        {
    //            GameObject newGameObject = new GameObject(" GameManager ");
    //            sInstatance = newGameObject.AddComponent<Information>();
    //        }
    //        return sInstatance;
    //    }
    //}

    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}
}
