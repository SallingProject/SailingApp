/**************************************************************************************/
/*! @file   WindObject.cs
***************************************************************************************
@brief      風とするオブジェクトに設定するクラス
***************************************************************************************
@author     Kaneko Kazuki
***************************************************************************************/

using UnityEngine;
using System.Collections;

public class WindObject : BaseObject{

    [SerializeField]
    private float m_windForce;

    [SerializeField]
    [Range(-180, 180)]
    private float m_windDirection;

    public float mWindForce
    {
        get { return m_windForce; }
        set { m_windForce = value; }
    }
    public float mWindDirection
    {
        get { return m_windDirection; }
        set {
            //180°以上にはならないようにする
            if (Mathf.Abs(value) > 180)
            {
                m_windDirection = Mathf.Clamp(value, -180, 180);
            }
        }
    }

    protected override void mOnRegistered()
    {
        mUnregisterList(this);
        mUnregister();
    }

}
