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

    private bool m_controllerTrigger = false;
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

    public float mGetHandleRotationZ()
    {

        //if (m_handleController.mIsDown){
        //    m_controllerTrigger = true;
        //}
        bool trigger = m_controllerTrigger & !m_handleController.mIsDown;
            m_controllerTrigger = m_handleController.mIsDown;

        if (trigger) { 
            return -m_handleController.mHandleRotationZ;
        }else
        {
            return 0.0f;
        }
    }
    

}
