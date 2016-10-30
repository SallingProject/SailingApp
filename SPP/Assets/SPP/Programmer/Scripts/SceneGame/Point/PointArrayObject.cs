/**************************************************************************************/
/*! @file   PointArrayManager.cs
***************************************************************************************
@brief      ポイントを管理するArrayを管理する
***************************************************************************************
@author     Kaneko Kazuki
***************************************************************************************/


using UnityEngine;
using System.Collections.Generic;

public class PointArrayObject : BaseObject {

    

    static private List<BaseObject>m_pointArray = new List<BaseObject>();
    static private int m_currentId;


    //ポイント配列に登録
    protected override void mOnRegistered()
    {
        m_pointArray.Add(this);
        Debug.Log(this+""+m_pointArray.Count);
    }

    //次のポイントへ進めるために
    protected void mNext()
    {
        m_currentId++;
    }


    //ポイントを取得する
    protected BaseObject mGetPoint() {
        return m_pointArray[m_currentId];
    }
    //前のポイントを取得する
    protected BaseObject mGetPrevPoint()
    {
        if (m_currentId <= 0) return null;
        return m_pointArray[m_currentId-1];
    }




}
    

