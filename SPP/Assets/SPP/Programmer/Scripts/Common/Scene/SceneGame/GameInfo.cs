﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameInfo : BaseObjectSingleton<GameInfo>{

    [SerializeField]
    public TargetMarker m_targetMarker;
    [SerializeField]
    private UIHandleController m_handleController;
    [SerializeField]
    private UISailController m_sailController;

    [SerializeField]
    public WindObject m_wind;       //風オブジェクト

    [SerializeField]
    public PointArrayObject m_pointArray;       //ポイント配列管理クラス(Staticなので問題ない)

    private List<ItemDefine> m_itemList = new List<ItemDefine>();

    public bool mControllerTrigger { get; private set; }
    private float m_prevControllerRotation = 0;

    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        mUnregisterList(this);
        mUnregister();
    }


    /****************************************************************************** 
    @brief      コントローラー回転量を取得する
    @note       常に取得する
    @return     回転量
    *******************************************************************************/
    public float mGetHandleRotation()
    {
            return -m_handleController.mHandleRotationZ;
    }

    /****************************************************************************** 
    @brief      セールコントローラーの位置を取得？する
    @note       常に取得する
    @return     回転量
    *******************************************************************************/
    public float mGetSailRotation()
    {
        return 0.0f;
    }


    /****************************************************************************** 
    @brief      発動するアイテムを保存する。
    @param[in]  発動するアイテムの定義情報
    @param[in]  使用者ID
    @return     none
    *******************************************************************************/
    public void SetInvokeItem(ItemDefine define,int userId)
    {
        m_itemList.Add(define);
    }   
}
