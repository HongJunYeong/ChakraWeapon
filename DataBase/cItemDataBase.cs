using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class cItemDataBase : MonoBehaviour {

    #region private 변수

    private TextAsset m_textAssetItemData;                         //엑셀파일을 읽어드릴 텍스트 에셋
    private static cItemDataBase sInstance = null;                 //데이터 베이스를 사용할 static형 변수

    #endregion

    #region public 변수

    public Dictionary<int, cItemInformation> m_dictionaryItemDataBase;        //아이템 데이터베이스
    public static cItemDataBase Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject _gameObject = new GameObject("ItemDataBase");
                sInstance = _gameObject.AddComponent<cItemDataBase>();
            }

            return sInstance;
        }
    }

    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);                                     //삭제 되지 않게함

        m_dictionaryItemDataBase = new Dictionary<int, cItemInformation>();                //Dictionary 인스턴스 할당

        m_textAssetItemData = (TextAsset)Resources.Load("chakraweapon_item");   //Resoruces폴더에서 데이터파일 불러들임

        string sItemData = m_textAssetItemData.text;                            //텍스트 에셋의 text를 저장
        string[] sSplitItemData = sItemData.Split('\n');                        //엔터를 기준으로 나눠서 다시 저장

        List<string> listItemData = new List<string>();                          //엔터를 기준으로 나눈 텍스를 저장할 리스트 선언

        foreach (string splitData in sSplitItemData)                             //리스트에 텍스트 저장
            listItemData.Add(splitData);


        listItemData.RemoveAt(0);                                                 //: 처음과 끝부분 데이터를 지움
        listItemData.Reverse();                                                   //  (처음과 끝에는 아무것도 들어있지 않음)
        listItemData.RemoveAt(0);                                                 //
        listItemData.Reverse();                                                   //:

        List<string[]> listItemDataArray = new List<string[]>();                  // ","로 구분지어서 나눌 문자열 배열형 리스트를 선언

        foreach (string itemData in listItemData)                                 // ","로 구분지어서 나눠서 담음
        {
            string[] itemDataArray = itemData.Split(',');
            listItemDataArray.Add(itemDataArray);
        }

        foreach (string[] itemDataArray in listItemDataArray)                      // 여기서부터 아이템에 정보를 할당하고
        {
            cItemInformation item = new cItemInformation();

            item.m_nIdNumber = Int32.Parse(itemDataArray[0]);                                 //아이템 고유번호
            item.m_sName = itemDataArray[1];                                                //아이템 한글명
            item.m_sEnglishName = itemDataArray[2];                                         //아이템 영문명
            item.m_eItemType = (Information.eItemType)Int32.Parse(itemDataArray[3]);        //아이템 타입
            item.m_eItemArmorPart = (Information.eItemPart)Int32.Parse(itemDataArray[4]);   //아이템 부위
            item.m_eItemElement = (Information.eElement)Int32.Parse(itemDataArray[5]);      //아이템 원소속성
            item.m_nPrice = Int32.Parse(itemDataArray[6]);                                  //아이템 가격
            item.m_nIncreaseHp = Int32.Parse(itemDataArray[7]);                             //아이템 최대체력 증가량
            item.m_nIncreaseChakra = Int32.Parse(itemDataArray[8]);                         //아이템 최대차크라 증가량
            item.m_nHpRegenRate = Int32.Parse(itemDataArray[9]);                            //아이템 초당 체력 회복량
            item.m_nChakraRegenRate = Int32.Parse(itemDataArray[10]);                       //아이템 초당 차크라 회복량
            item.m_fMoveSpeed = float.Parse(itemDataArray[11]);                             //아이템 이동속도 증가량
            item.m_nPhysicalArmor = Int32.Parse(itemDataArray[12]);                         //아이템 물리방어 증가량
            item.m_nMasicalArmor = Int32.Parse(itemDataArray[13]);                          //아이템 마법방어 증가량
            item.m_sInfo = itemDataArray[14];                                               //아이템 설명

            m_dictionaryItemDataBase.Add(item.m_nIdNumber, item);                             //아이템 고유번호를 키값으로
                                                                                            //Dictiionary에 담는다.
        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
