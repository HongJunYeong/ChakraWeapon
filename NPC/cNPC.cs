using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cNPC : MonoBehaviour {

    //아이템들은 Dictionary로 미리 저장

    #region Private 변수

    Dictionary<int, List<string>>   m_dicDialogue;         //대화내용 dic
    int                             m_nDialogueCount;      //몇 번째 대화인지 카운트

    #endregion

    #region Public 변수

    public string                   m_sName;               //이름(database키값)

#endregion

}
