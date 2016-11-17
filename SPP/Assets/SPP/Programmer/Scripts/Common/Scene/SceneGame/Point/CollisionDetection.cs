/**************************************************************************************/
/*! @file   CollisionDetection.cs
***************************************************************************************
@brief      子どもとかに当たり判定を付けた場合に親側で取得する用
***************************************************************************************
@author     Kaneko Kazuki
***************************************************************************************/

using UnityEngine;
using System.Collections;

public class CollisionDetection : BaseObject{

    private bool m_isEnter = false;
    public bool mIsEntered
    {
        get { return m_isEnter; }
        set { m_isEnter = value; }
    }

    private float m_direction;
    public float mDirection
    {
        set { m_direction = value; }
    }

    void OnTriggerEnter(Collider other)
    {
        mIsEntered = true;
    }

    protected override void mOnRegistered()
    {
        mUnregisterList(this);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.6f);
        var prefs = Resources.Load("Ship/cursor") as GameObject;
        Vector3 rote = transform.eulerAngles;
        rote.y += m_direction * 90;
        Gizmos.DrawMesh(prefs.GetComponentInChildren<MeshFilter>().sharedMesh,-1, transform.position+Vector3.up*2, Quaternion.Euler(rote), new Vector3(100,100,100));
    }

}
