using UnityEngine;
using System.Collections;

public class GameInfo : BaseObjectSingleton<GameInfo>{

    [SerializeField]
    public TargetMarker m_targetMarker;
    [SerializeField]
    private UIHandleController m_handleController;

    [HideInInspector]
    public WindObject m_wind;       //風オブジェクト
    [HideInInspector]
    public PointArrayObject m_pointArray;       //ポイント配列管理クラス(Staticなので問題ない)

    public bool mControllerTrigger { get; private set; }
    private float m_prevControllerRotation = 0;
    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        m_wind = new WindObject();
        m_wind.mWindForce = 4;
        m_wind.mWindDirection = 0;

        m_pointArray = new PointArrayObject();

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


}
