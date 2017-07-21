using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCaster : MonoBehaviour { 

    #region public 변수

    public GameObject m_objRushCollider;
    public Canvas m_canvasSkillTree;
    public GameObject[] m_arraySkill;
	public Animator m_animatorAnim;

    #endregion


    #region private 변수

    private GameObject m_objHit;                                 

    #endregion

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K))
		{
			m_canvasSkillTree.gameObject.SetActive(!m_canvasSkillTree.gameObject.activeInHierarchy);
		}
		SwitchSkillSlot();
		CastSkill();
	}

	private void LateUpdate()
	{

	}

    #region 스킬 발동 조건 구현

    /// <summary>
    /// 1, 2, 3의 숫자키를 이용하여 근거리/원거리/보조 스킬 슬롯을 교체한다.
    /// </summary>
    public void SwitchSkillSlot()
	{
		int nComboStep = -1;
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			nComboStep = cCharacterInformation.Instance.m_nMeleeSkillComboStep;
            cCharacterInformation.Instance.m_dicCurrentSkillSlot = cCharacterInformation.Instance.m_listDicMeleeSkillSlot[nComboStep];
			cCharacterInformation.Instance.m_nCurrentSkillSlotIndex = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			nComboStep = cCharacterInformation.Instance.m_nRangeSkillComboStep;
            cCharacterInformation.Instance.m_dicCurrentSkillSlot = cCharacterInformation.Instance.m_listDicRangeSkillSlot[nComboStep];
			cCharacterInformation.Instance.m_nCurrentSkillSlotIndex = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			nComboStep = cCharacterInformation.Instance.m_nOtherSkillComboStep;
            cCharacterInformation.Instance.m_dicCurrentSkillSlot = cCharacterInformation.Instance.m_listDicOtherSkillSlot[nComboStep];
			cCharacterInformation.Instance.m_nCurrentSkillSlotIndex = 3;
		}
	}

	/// <summary>
	/// 좌, 우클릭을 통한 스킬을 선택 및 시전한다.
	/// </summary>
	public void CastSkill()
	{
        // >> 스킬 선택하는 부분
        cSkillInformation cCurrentSkill = null;
		if(Input.GetMouseButtonDown(0))
		{
			cCurrentSkill = cCharacterInformation.Instance.m_dicCurrentSkillSlot[Information.eClick.L_CLICK];
            cCharacterInformation.Instance.m_eClick = Information.eClick.L_CLICK;
		}
		else if (Input.GetMouseButtonDown(1))
		{
			cCurrentSkill = cCharacterInformation.Instance.m_dicCurrentSkillSlot[Information.eClick.R_CLICK];
            cCharacterInformation.Instance.m_eClick = Information.eClick.R_CLICK;
		}
		// << 
		print(cCurrentSkill.m_sName);

		// >> 스킬 시전 가능여부 검사하는 부분
		if (cCharacterInformation.Instance.m_nChkra < cCurrentSkill.m_nChakraCost)
		{
			Debug.Log("차크라가 부족합니다.");
			return; 
		}
		// <<


		// >> 스킬 실제 시전하는 부분
		print(cCurrentSkill.m_nIdNumber + "시전");
		// 콤보가 0이 아닐때, 시전 스킬이 비어있는 경우는 종료되도록
		if (cCurrentSkill.m_nIdNumber == -1)
		{
			switch(cCharacterInformation.Instance.m_nCurrentSkillSlotIndex)
			{
				case 1:
					if(cCharacterInformation.Instance.m_nMeleeSkillComboStep >= 1) return;
					break;
				case 2:
					if (cCharacterInformation.Instance.m_nRangeSkillComboStep >= 1) return;
					break;
				case 3:
					if (cCharacterInformation.Instance.m_nOtherSkillComboStep >= 1) return;
					break;
			}
		}
		else
		{
			m_arraySkill[cCurrentSkill.m_nIdNumber].gameObject.SetActive(true);
		}		
		// << 



		// >> 스킬 사용시 공통적으로 변경되는 정보들
		// 일반공격/스킬사용 버튼 중 일반공격 눌렀을 경우
		if (cCurrentSkill.m_nIdNumber == 0 ||
			cCurrentSkill.m_nIdNumber == 15 ||
			cCurrentSkill.m_nIdNumber == 30)
		{
			
		}
		// 일반공격/스킬사용 버튼 중 스킬사용 눌렀을 경우
		else if (cCurrentSkill.m_nIdNumber == -1)
		{
			switch(cCharacterInformation.Instance.m_dicCurrentSkillSlot[Information.eClick.L_CLICK].m_eType)
			{
				case Information.eSkillType.MELEE:
                    cCharacterInformation.Instance.m_nMeleeSkillComboStep += 1;				
					break;
				case Information.eSkillType.RANGE:
                    cCharacterInformation.Instance.m_nRangeSkillComboStep += 1;
					break;
				case Information.eSkillType.OTHER:
                    cCharacterInformation.Instance.m_nOtherSkillComboStep += 1;
					break;
			}
		}
		//그 외
		else
		{
			switch (cCurrentSkill.m_eType)
			{
				case Information.eSkillType.MELEE:
                    cCharacterInformation.Instance.m_nMeleeSkillComboStep += 1;
					if (cCharacterInformation.Instance.m_nMeleeSkillComboStep > 3) cCharacterInformation.Instance.m_nMeleeSkillComboStep = 0;
					break;
				case Information.eSkillType.RANGE:
                    cCharacterInformation.Instance.m_nRangeSkillComboStep += 1;
					if (cCharacterInformation.Instance.m_nRangeSkillComboStep > 3) cCharacterInformation.Instance.m_nRangeSkillComboStep = 0;
					break;
				case Information.eSkillType.OTHER:
                    cCharacterInformation.Instance.m_nOtherSkillComboStep += 1;
					if (cCharacterInformation.Instance.m_nOtherSkillComboStep > 3) cCharacterInformation.Instance.m_nOtherSkillComboStep = 0;
					break;
			}

			// 콤보제한시간 안끝났으면 +1
			if (cCharacterInformation.Instance.m_fComboLimitTime > 0) cCharacterInformation.Instance.m_nTotalComboNum += 1;

            // 스킬 쓴 뒤 콤보제한시간 최대치부터 다시 시작
            cCharacterInformation.Instance.m_fComboLimitTime = cCharacterInformation.Instance.m_fMaxComboLimitTime;
		}
		// <<
	}


	/// <summary>
	/// 스킬별 애니메이션을 시작시킨다.
	/// </summary>
	private void StartAnimation()
	{
		
	}

	#endregion

}
