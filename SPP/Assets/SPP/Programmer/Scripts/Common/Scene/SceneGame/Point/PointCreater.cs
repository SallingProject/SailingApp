using UnityEngine;
using System.Collections;

public class PointCreater : BaseObject{

    [SerializeField]
    private Point[] m_point;

    protected override void mOnRegistered()
    {
        mUnregisterList(this);
        mUnregister();
    }


    // Use this for initialization
    public void mInitializer()
    {
        foreach (var obj in m_point)
        {
            obj.mInitializer();
        }
        GameInfo.mInstance.m_pointArray.mGetPoint().GetComponent<Point>().enabled = true;
        var instance = GameInfo.mInstance.m_pointArray.mGetLastPoint().transform.FindInChildren("In", false);
        instance.AddComponent<SceneChanger>();
    }

}
