using UnityEngine;
using System.Collections;

public class GameInfo : BaseObjectSingleton<GameInfo>{

    [SerializeField]
    public TargetMarker m_targetMarker;

    public WindObject m_wind;

    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        m_wind = new WindObject();
        m_wind.mWindForce = 2;
        m_wind.mWindDirection = 0;
    }

    public override void mOnUpdate()
    {

    }


}
