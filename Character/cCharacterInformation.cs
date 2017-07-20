using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCharacterInformation : MonoBehaviour {

    private static cCharacterInformation m_sInstance;
    public static cCharacterInformation Instance
    {
        get
        {
            if (m_sInstance == null)
            {
                GameObject newObject = new GameObject("_cCharacter");
                m_sInstance = newObject.AddComponent<cCharacterInformation>();
            }
            return m_sInstance;
        }
    }


    #region public:
    //protected List<class명>															//버프, 디버프 상태리스트
    public Information.eElement m_eAttributes;											//현재 속성
    public Information.eAnimState m_eAnimState;											//애니메이션 상태
    public Information.eClick m_eClick;													//왼클릭인지 오른클릭인지
    public Information.eQuestCondition m_eQuestCondition;                               //현재 퀘스트 상태
                                                                                        //public Information.eBuffDebuff m_eBuffDebuffState;                                  //버프, 디버프 상태 (비트연산)



    public Dictionary<int, cSkill> m_dicSkills;                              //획득한 전체 스킬 목록
    public List<Dictionary<Information.eClick, cSkill>> m_listDicMeleeSkillSlot;        //근접스킬 좌우클릭 장착 정보 (index of list:n단계, key:좌우클릭, value:스킬)
    public List<Dictionary<Information.eClick, cSkill>> m_listDicRangeSkillSlot;        //원거리스킬 좌우클릭 장착 정보(index of list:n단계, key:좌우클릭, value:스킬)
    public List<Dictionary<Information.eClick, cSkill>> m_listDicOtherSkillSlot;        //보조스킬 좌우클릭 장착 정보(index of list:n단계, key:좌우클릭, value:스킬)
    public Dictionary<Information.eClick, cSkill> m_dicCurrentSkillSlot;				//현재스킬 좌우클릭 장착 정보(key:좌우클릭, value:스킬)

    //public Dictionary<int, cItem>


    public int m_nMeleeSkillComboStep;													//근접스킬 콤보 단계(1,2,3단계 중)
    public int m_nRangeSkillComboStep;													//원거리스킬 콤보 단계(1,2,3단계 중)
    public int m_nOtherSkillComboStep;                                                  //보조스킬 콤보 단계(1,2,3단계 중)

    public int m_nTotalComboNum;														//총콤보 수         
    public int m_nOriginHP, m_nHP;                                                      //최대체력, 현재체력
    public int m_nOriginChakra, m_nChkra;												//최대차크라, 현재차크라
    public int m_nOriPhysicalAtk, m_nPhysicalAtk;										//물리공격력, 변화된 물리공격력
    public int m_nOriginPhysicalDefence, m_nPhysicalDefence;							//물리방어력, 변화된 물방
    public int m_nOriginMagicalAttack, m_nMagicalAttack;								//마법공격력, 변화된 마법공격력
    public int m_nOriginMagicalDefence, m_nMagicalDefence;								//마법방어력, 변화된 마방

    public int m_nOriginHpRengen, m_nHpRengen;											//초당 체력회복량, 변화된 초당 체력회복량
    public int m_nOriginChakraRengen, m_nChakraRengen;                                  //초당 마나회복량, 변화된 초당 마나회복량

    public float m_fMaxComboLimitTime;                                                  //최대 콤보 제한 시간(스킬 시전 시 이 변수로 현재콤보시간을 둔 뒤, 현재 콤보시간은 deltaTime에 의해 감소됨) 
    public float m_fComboLimitTime;                                                     //현재 콤보 제한 시간     

    public float m_fOriginSpeed, m_fSpeed;												//이동속도, 변화된 이동속도

    public float m_fOriginAttackRange, m_nAttackRange;									//활 공격사거리, 변화된 활 공격사거리

    public bool m_isDontMove = false;                                                   //움직일 수 있는지 없는지 변수. (못움직이면 트루)
    public bool m_isDontJump = false;

    #endregion

    ///--------------------------------------------------------------------
    ///                    클래스 구현되면 명명할 변수들
    ///--------------------------------------------------------------------

    //보유 아이템목록 리스트
    //현재진행중인 퀘스트

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InitializeCharacterInfo();
        InitializeSkillSlots();
        InputSkillsForTest();
        AddBasicSkills();
    }

    private void Start()
    {

    }

    private void Update()
    {
        // 콤보 제한 시간 감소 업데이트
        m_fComboLimitTime = Mathf.Clamp(m_fComboLimitTime - Time.deltaTime, 0.0f, m_fMaxComboLimitTime);
    }

    /// <summary>
    /// 모든 스킬 슬롯에 대한 동적할당한 뒤, 기본공격을 추가한다.
    /// </summary>
    private void InitializeSkillSlots()
    {
        // >> 동적 할당
        m_dicSkills = new Dictionary<int, cSkill>();

        m_listDicMeleeSkillSlot = new List<Dictionary<Information.eClick, cSkill>>();
        for (int i = 0; i < 4; i++)
        {
            Dictionary<Information.eClick, cSkill> newDic = new Dictionary<Information.eClick, cSkill>();
            m_listDicMeleeSkillSlot.Add(newDic);
        }

        m_listDicRangeSkillSlot = new List<Dictionary<Information.eClick, cSkill>>();
        for (int i = 0; i < 4; i++)
        {
            Dictionary<Information.eClick, cSkill> newDic = new Dictionary<Information.eClick, cSkill>();
            m_listDicRangeSkillSlot.Add(newDic);
        }

        m_listDicOtherSkillSlot = new List<Dictionary<Information.eClick, cSkill>>();
        for (int i = 0; i < 4; i++)
        {
            Dictionary<Information.eClick, cSkill> newDic = new Dictionary<Information.eClick, cSkill>();
            m_listDicOtherSkillSlot.Add(newDic);
        }

        m_dicCurrentSkillSlot = new Dictionary<Information.eClick, cSkill>();
        // << 


        // >> 기본 공격 추가	나중에 살려야 함
        //m_dicSkills.Add(cSkillDataBase.Instance.m_dictionarySkillDataBase[0]);
        //m_dicSkills.Add(cSkillDataBase.Instance.m_dictionarySkillDataBase[15]);
        //m_dicSkills.Add(cSkillDataBase.Instance.m_dictionarySkillDataBase[30]);
        // << 
    }

    private void InitializeCharacterInfo()
    {
        m_eClick = Information.eClick.NONE;
        m_nMeleeSkillComboStep = 0;
        m_nRangeSkillComboStep = 0;
        m_nOtherSkillComboStep = 0;
        m_nTotalComboNum = 0;
        m_nOriginHP = 5000;
        m_nHP = 5000;
        m_nOriginChakra = 100;
        m_nChkra = 100;
        m_fMaxComboLimitTime = 7;
        m_fComboLimitTime = 0;
    }

    private void AddBasicSkills()
    {
        // >> 각 스킬 타입별로 0단계일 때 일반공격/스킬선택을 넣어놓는 부분
        for (int i = 0; i < 4; i++)
        {
            m_listDicMeleeSkillSlot[i].Add(Information.eClick.L_CLICK, m_dicSkills[-1]);
            m_listDicMeleeSkillSlot[i].Add(Information.eClick.R_CLICK, m_dicSkills[-1]);
            m_listDicRangeSkillSlot[i].Add(Information.eClick.L_CLICK, m_dicSkills[-1]);
            m_listDicRangeSkillSlot[i].Add(Information.eClick.R_CLICK, m_dicSkills[-1]);
            m_listDicOtherSkillSlot[i].Add(Information.eClick.L_CLICK, m_dicSkills[-1]);
            m_listDicOtherSkillSlot[i].Add(Information.eClick.R_CLICK, m_dicSkills[-1]);
        }

        m_listDicMeleeSkillSlot[0][Information.eClick.L_CLICK] = m_dicSkills[0];
        m_listDicRangeSkillSlot[0][Information.eClick.L_CLICK] = m_dicSkills[15];
        m_listDicOtherSkillSlot[0][Information.eClick.L_CLICK] = m_dicSkills[30];
        m_dicCurrentSkillSlot = m_listDicMeleeSkillSlot[0];
        // <<
    }

    #region Test용 함수

    private void OnGUI()
    {
        string text = "m_eClick : " + m_eClick +
            "\nm_listSkills : " + m_dicSkills.Count +
            "\nm_listDicMeleeSkillSlot : " + m_listDicMeleeSkillSlot.Count +
            "\nm_listDicRangeSkillSlot : " + m_listDicRangeSkillSlot.Count +
            "\nm_listDicOtherSkillSlot : " + m_listDicOtherSkillSlot.Count +
            "\nm_dicCurrentSkillSlot : " + m_dicCurrentSkillSlot[Information.eClick.L_CLICK].m_cSkillInformation.m_sName + " : " + 
            m_dicCurrentSkillSlot[Information.eClick.R_CLICK].m_cSkillInformation.m_sName +
            "\nm_nChkra : " + m_nChkra +
            "\nm_nTotalComboNum : " + m_nTotalComboNum;
        GUI.TextArea(new Rect(0, 0, 150, 250), text);

        string skills = "< 근접 > \n" +
             IsSkillIn(1, 0, Information.eClick.L_CLICK) + " : " + IsSkillIn(1, 0, Information.eClick.R_CLICK) + "\n" +
             IsSkillIn(1, 1, Information.eClick.L_CLICK) + " : " + IsSkillIn(1, 1, Information.eClick.R_CLICK) + "\n" +
             IsSkillIn(1, 2, Information.eClick.L_CLICK) + " : " + IsSkillIn(1, 2, Information.eClick.R_CLICK) + "\n" +
             IsSkillIn(1, 3, Information.eClick.L_CLICK) + " : " + IsSkillIn(1, 3, Information.eClick.R_CLICK);
        GUI.TextArea(new Rect(200, 0, 150, 100), skills);

        skills = "< 원거리 > \n" +
     IsSkillIn(2, 0, Information.eClick.L_CLICK) + " : " + IsSkillIn(2, 0, Information.eClick.R_CLICK) + "\n" +
     IsSkillIn(2, 1, Information.eClick.L_CLICK) + " : " + IsSkillIn(2, 1, Information.eClick.R_CLICK) + "\n" +
     IsSkillIn(2, 2, Information.eClick.L_CLICK) + " : " + IsSkillIn(2, 2, Information.eClick.R_CLICK) + "\n" +
     IsSkillIn(2, 3, Information.eClick.L_CLICK) + " : " + IsSkillIn(2, 3, Information.eClick.R_CLICK);
        GUI.TextArea(new Rect(400, 0, 150, 100), skills);

        skills = "< 보조 > \n" +
     IsSkillIn(3, 0, Information.eClick.L_CLICK) + " : " + IsSkillIn(3, 0, Information.eClick.R_CLICK) + "\n" +
     IsSkillIn(3, 1, Information.eClick.L_CLICK) + " : " + IsSkillIn(3, 1, Information.eClick.R_CLICK) + "\n" +
     IsSkillIn(3, 2, Information.eClick.L_CLICK) + " : " + IsSkillIn(3, 2, Information.eClick.R_CLICK) + "\n" +
     IsSkillIn(3, 3, Information.eClick.L_CLICK) + " : " + IsSkillIn(3, 3, Information.eClick.R_CLICK);
        GUI.TextArea(new Rect(600, 0, 150, 100), skills);
    }

    private void InputSkillsForTest()
    {
        for (int i = 0; i < cSkillDataBase.Instance.m_dictionarySkillDataBase.Count; i++)
        {
            cSkill skill = new cSkill();
            skill.m_cSkillInformation = cSkillDataBase.Instance.m_dictionarySkillDataBase[i - 1];
            m_dicSkills.Add(skill.m_cSkillInformation.m_nIdNumber, skill);
        }
    }

    private string IsSkillIn(int index, int step, Information.eClick mouse)
    {
        cSkill skill = null;

        switch (index)
        {
            case 1:
                skill = cCharacterInformation.Instance.m_listDicMeleeSkillSlot[step][mouse];
                break;
            case 2:
                skill = cCharacterInformation.Instance.m_listDicRangeSkillSlot[step][mouse];
                break;
            case 3:
                skill = cCharacterInformation.Instance.m_listDicOtherSkillSlot[step][mouse];
                break;
        }

        return skill.m_cSkillInformation.name;
    }

    #endregion


    //보유 아이템목록 리스트
    //현재진행중인 퀘스트
}
