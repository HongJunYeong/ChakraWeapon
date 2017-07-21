using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c14_ShadowAttack : MonoBehaviour {
    #region private

    private Vector3 m_vecStartPos;
    private Quaternion m_QuatStartRot;

    private bool m_isUsing;

    #endregion

    #region public

    public cSkillInformation m_cSkillInformation = cSkillDataBase.Instance.m_dictionarySkillDataBase[14];

    public float m_fMoveSpeed = 10.0f;

    #endregion
    // Use this for initialization
    void Start () {
        m_vecStartPos = transform.position;
        m_QuatStartRot = transform.rotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(transform.forward * m_fMoveSpeed * Time.deltaTime);

        if(!m_isUsing)
        {
            if (transform.position.z >= m_vecStartPos.z + 15.0f)
            {
                init();
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            Attack(other.gameObject);
        }
    }

    private void Attack(GameObject monster)
    {
        init();
    }

    private void init()
    {

    }
}
