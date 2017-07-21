using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class cSkillDataBase : MonoBehaviour {

    #region private 변수

    private TextAsset m_textAssetSkillData;                         //엑셀파일을 읽어드릴 텍스트 에셋
    private static cSkillDataBase sInstance = null;                 //데이터 베이스를 사용할 static형 변수

    #endregion

    #region public 변수

    public Dictionary<int, cSkillInformation> m_dictionarySkillDataBase;      //스킬 데이터 베이스 

    public static cSkillDataBase Instance
    {
        get
        {
            if(sInstance == null)
            {
                GameObject _gameObject = new GameObject("SkillDataBase");
                sInstance = _gameObject.AddComponent<cSkillDataBase>();
            }

            return sInstance;
        }
    }                          

    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);                                     //삭제 되지 않게함

        m_dictionarySkillDataBase = new Dictionary<int, cSkillInformation>();              //Dictionary 인스턴스 할당

        m_textAssetSkillData = (TextAsset)Resources.Load("chakraweapon_skill"); //Resoruces폴더에서 데이터파일 불러들임

        string sSkillData = m_textAssetSkillData.text;                          //텍스트 에셋의 text를 저장
        string[] sSplitSkillData = sSkillData.Split('\n');                      //엔터를 기준으로 나눠서 다시 저장

        List<string> listSkillData = new List<string>();                        //엔터를 기준으로 나눈 텍스를 저장할 리스트 선언

        foreach (string splitData in sSplitSkillData)                           //리스트에 텍스트 저장
            listSkillData.Add(splitData);


        listSkillData.RemoveAt(0);                                              //: 처음과 끝부분 데이터를 지움
        listSkillData.Reverse();                                                //  (처음과 끝에는 아무것도 들어있지 않음)
        listSkillData.RemoveAt(0);                                              //
        listSkillData.Reverse();                                                //:

        List<string[]> listSkillDataArray = new List<string[]>();               // ","로 구분지어서 나눌 문자열 배열형 리스트를 선언

        foreach (string skillData in listSkillData)                             // ","로 구분지어서 나눠서 담음
        {
            string[] skillDataArray = skillData.Split(',');
            listSkillDataArray.Add(skillDataArray);
        }

        foreach (string[] skillDataArray in listSkillDataArray)                 // 여기서부터 스킬에 정보를 할당하고
        {
            cSkillInformation skill = new cSkillInformation();

            skill.m_nIdNumber = Int32.Parse(skillDataArray[0]);                     //스킬 고유번호
            skill.m_sKoreanName = skillDataArray[1];                                //스킬 한글명
            skill.m_sName = skillDataArray[2];                                      //스킬 영문명
            skill.m_eType = (Information.eSkillType)Int32.Parse(skillDataArray[3]); //스킬 계열
            skill.m_nChainLevel = Int32.Parse(skillDataArray[4]);                   //스킬 연계레벨
            skill.m_eElement = (Information.eElement)Int32.Parse(skillDataArray[5]);//스킬 원소 속성
            skill.m_nDamageType = Int32.Parse(skillDataArray[6]);                   //스킬 공격 속성
            skill.m_fDamage = float.Parse(skillDataArray[7]);                       //스킬 계수
            skill.m_nChakraCost = Int32.Parse(skillDataArray[8]);                   //스킬 차크라 소모량
            skill.m_sInfo = skillDataArray[9];                                      //스킬 정보
            skill.m_nOptionNumber = Int32.Parse(skillDataArray[10]);                //스킬 옵션번호
            skill.m_sOption_1_Info = skillDataArray[11];                            //스킬 옵션1 정보
            skill.m_sOption_2_Info = skillDataArray[12];                            //스킬 옵션2 정보
            skill.m_sOption_3_Info = skillDataArray[13];                            //스킬 옵션3 정보

            //이미지 경로(작성해야함)
            //skill.m_imageIcon = (Image)Resources.Load(skillDataArray[14]);

            m_dictionarySkillDataBase.Add(skill.m_nIdNumber, skill);                //스킬 고유번호를 키값으로 스킬을 Dictioary에 담는다              
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
