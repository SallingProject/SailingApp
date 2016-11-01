using UnityEngine;
using System.Collections;

public class GameInfo : BaseObjectSingleton<GameInfo>{

    [SerializeField]
    public TargetMarker m_targetMarker;

    [HideInInspector]
    public WindObject m_wind;       //風オブジェクト
    [HideInInspector]
    public PointArrayObject m_pointArray;       //ポイント配列管理クラス(Staticなので問題ない)

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

    

}
