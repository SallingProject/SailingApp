using UnityEngine;
using System.Collections.Generic;

public class InductionRing : BaseObject {

    static readonly int m_kCreateCount = 9;

    [SerializeField]
    GameObject m_ringPref;

    [SerializeField]
    float m_radius = 10f;
    List<GameObject> m_ringCashList = new List<GameObject>();

    Point m_nextpoint;

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
    }


    public void mSetNextPoint(Point nextPoint)
    {
        m_nextpoint = nextPoint;

        if (nextPoint.m_buoyType == Point.eBuoyType.Curve)
        {
            //オブジェクト間の角度差
            float angleDiff = 270f / (float)m_ringCashList.Count;
            //各オブジェクトを円状に配置
            for (int i = 0; i < m_ringCashList.Count; i++)
            {
                Vector3 childPostion = transform.position;

                float angle = ((angleDiff * i) - angleDiff) * Mathf.Deg2Rad + m_nextpoint.transform.eulerAngles.y;
                childPostion.x += m_radius * Mathf.Cos(angle) + m_nextpoint.transform.position.x;
                childPostion.z += m_radius * Mathf.Sin(angle) + m_nextpoint.transform.position.z;

                m_ringCashList[i].transform.position = childPostion;
            }
        }
        else
        {
            int harf = m_ringCashList.Count / 2;
            //各オブジェクトを円状に配置
            for (int i = 0; i < m_ringCashList.Count; i++)
            {
                Vector3 pointPosition = m_nextpoint.transform.position;
                if(harf > i)
                {
                    m_ringCashList[i].transform.position = new Vector3(pointPosition.x, pointPosition.y, pointPosition.z - (m_radius * i));
                }
                else
                {
                    m_ringCashList[i].transform.position = new Vector3(pointPosition.x, pointPosition.y, pointPosition.z + (m_radius * (i - harf)));
                }
            }
        }
    }

    #region テスト用
#if false
    void Update()
    {
        mSetNextPoint(m_nextpoint);
    }
#endif
    #endregion
}
