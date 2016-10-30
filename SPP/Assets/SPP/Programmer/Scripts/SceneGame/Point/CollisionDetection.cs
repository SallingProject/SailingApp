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

    void OnTriggerEnter(Collider other)
    {
        mIsEntered = true;
    }

    protected override void mOnRegistered()
    {
        mUnregisterList(this);
    }


}
