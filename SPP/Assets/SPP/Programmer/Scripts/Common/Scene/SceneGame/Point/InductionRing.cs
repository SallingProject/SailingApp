using UnityEngine;
using System.Collections.Generic;

public class InductionRing : BaseObject {

    static readonly int m_kCreateCount = 8;

    [SerializeField]
    GameObject m_ringPref;

    [SerializeField]
    float m_radius = 0.5f;
    List<GameObject> m_ringCashList = new List<GameObject>();

    [SerializeField]
    Point m_nextpoint;

    [SerializeField]
    float m_reset;

    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        mUnregisterList(this);

        m_ringCashList.Clear();
        for(int i=0;i< m_kCreateCount; ++i)
        {
            var add = mCreate(m_ringPref) as GameObject;
            add.transform.SetParent(this.transform, false);
            m_ringCashList.Add(add);
        }

        mSetNextPoint(m_nextpoint);
    }


    public void mSetNextPoint(Point nextPoint)
    {
        m_nextpoint = nextPoint;
        //オブジェクト間の角度差
        float angleDiff = 180f / (float)m_ringCashList.Count;

        //各オブジェクトを円状に配置
        for (int i = 0; i < m_ringCashList.Count; i++)
        {
            Vector3 childPostion = transform.position;

            float angle = (((m_reset + angleDiff) * i) - angleDiff) * Mathf.Deg2Rad + m_nextpoint.transform.eulerAngles.y - 180;
            childPostion.x += m_radius * Mathf.Cos(angle) + m_nextpoint.transform.position.x;
            childPostion.z += m_radius * Mathf.Sin(angle) + m_nextpoint.transform.position.z;

            m_ringCashList[i].transform.position = childPostion;
        }
    }

    void Update()
    {
        mSetNextPoint(m_nextpoint);
    }

}
