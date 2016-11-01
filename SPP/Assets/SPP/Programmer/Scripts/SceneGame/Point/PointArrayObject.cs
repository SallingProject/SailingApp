/**************************************************************************************/
/*! @file   PointArrayManager.cs
***************************************************************************************
@brief      ポイントを管理するArrayを管理する
@note       とりあえず
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
        Debug.Log(this+""+m_pointArray.Count+"Name:"+name);
    }

    

    /****************************************************************************** 
    @brief      ポイントを次へ進める
    @note       次がないときは最後/継承先でしか利用できない
    @return     のね
    *******************************************************************************/
    protected void mNext()
    {
        Debug.Log("ClearPoint:"+m_currentId);
        if (m_currentId >= m_pointArray.Count) return;

        //Baseクラスに書き換える
        mUnregisterList(m_pointArray[m_currentId]);
        m_pointArray[m_currentId].GetComponent<Point>().enabled = false;
        m_currentId++;
        if (m_currentId >= m_pointArray.Count) return;
        m_pointArray[m_currentId].GetComponent<Point>().enabled = true;
        Debug.Log(GameInfo.mInstance);
        GameInfo.mInstance.m_targetMarker.mSetTarget(m_pointArray[m_currentId].GetComponent<ReflectedOnCamera>());
    }


    /****************************************************************************** 
    @brief      現在の（次の）ポイントを取得する
    @note       
    @return     ポイント(BaseObject)
    *******************************************************************************/
    public BaseObject mGetPoint() {
        if (m_currentId >= m_pointArray.Count) return null;
        return m_pointArray[m_currentId];
    }

    /****************************************************************************** 
    @brief      前のポイントを取得する
    @note       最初は前が無いのでNullを返します。
    @return     ポイント(BaseObject)
    *******************************************************************************/
    public BaseObject mGetPrevPoint()
    {
        if (m_currentId <= 0 || m_currentId >= m_pointArray.Count) return null;
        return m_pointArray[m_currentId-1];
    }

    


}
    

