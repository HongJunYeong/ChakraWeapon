using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class cSkillChanger : MonoBehaviour, IPointerClickHandler
{
	public Information.eClick m_eClickedButton = Information.eClick.NONE;
	public int m_nSkillIdNumber;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SelectSkill();
	}

	/// <summary>
	/// 스킬을 플레이어 자신의 슬롯에 장착함
	/// </summary>
	public void SelectSkill()
	{
		//print(m_nSkillIdNumber + " : " + m_eClickedButton.ToString());
		if(m_eClickedButton == Information.eClick.NONE) return;

        cSkillInformation skill = cCharacterInformation.Instance.m_dicSkills[m_nSkillIdNumber];

		Information.eSkillType nSkillType = skill.m_eType;
		int nSkillStep = skill.m_nChainLevel;
		List<Dictionary<Information.eClick, cSkillInformation>> listDicSkillTree = null;

		// 스킬트리가 선택되는 부분
		switch (nSkillType)
		{
			case Information.eSkillType.MELEE:
				listDicSkillTree = cCharacterInformation.Instance.m_listDicMeleeSkillSlot;
				break;
			case Information.eSkillType.RANGE:
				listDicSkillTree = cCharacterInformation.Instance.m_listDicRangeSkillSlot;
				break;
			case Information.eSkillType.OTHER:
				listDicSkillTree = cCharacterInformation.Instance.m_listDicOtherSkillSlot;
				break;
		}

		// 선택된 스킬트리에 선택한 스킬 집어넣는 부분
		if (listDicSkillTree == null) return;
		listDicSkillTree[nSkillStep][m_eClickedButton] = skill;


		m_eClickedButton = Information.eClick.NONE;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			m_eClickedButton = Information.eClick.L_CLICK;
			Debug.Log("왼쪽으로 눌림");
		}			
		//else if (eventData.button == PointerEventData.InputButton.Middle)
		//	Debug.Log("Middle click");
		else if (eventData.button == PointerEventData.InputButton.Right)
		{
			m_eClickedButton = Information.eClick.R_CLICK;
			Debug.Log("오른쪽으로 눌림");
		}
			
	}
}
