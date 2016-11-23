using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfo : BaseObjectSingleton<GameInfo>{

    [SerializeField]
    public TargetMarker m_targetMarker;
    [SerializeField]
    private UIHandleController m_handleController;

    [System.NonSerialized]
    public WindObject m_wind;       //風オブジェクト
    [System.NonSerialized]
    public PointArrayObject m_pointArray;       //ポイント配列管理クラス(Staticなので問題ない)

    private List<ItemDefine> m_itemList = new List<ItemDefine>();

    public bool mControllerTrigger { get; private set; }
    private float m_prevControllerRotation = 0;
    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        m_wind = GetComponent<WindObject>();

        m_pointArray = GetComponent<PointArrayObject>();
        mUnregisterList(this);
        mUnregister();

    }


    /****************************************************************************** 
    @brief      コントローラー回転量を取得する
    @note       離した瞬間だけ値が取れる
    @return     回転量
    *******************************************************************************/
    public float mGetHandleRotationTrigger()
    {
        bool trigger = mControllerTrigger & !m_handleController.mIsDown;
            mControllerTrigger = m_handleController.mIsDown;
        if (trigger) {
            Debug.Log(m_prevControllerRotation);
            return -m_prevControllerRotation;
            
        }else
        {
            m_prevControllerRotation = m_handleController.mHandleRotationZ;
            return 0.0f;
        }
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
