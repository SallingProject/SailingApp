using UnityEngine;
using System.Collections;

public class PointCreater : BaseObject{


    protected override void mOnRegistered()
    {
        mUnregisterList(this);
        mUnregister();
    }


    // Use this for initialization
    public void mInitializer()
    {
        var obj = GameInfo.mInstance.m_pointArray.mGetPoint();
        Debug.Log(obj+obj.name);
        if (obj)
        {
            obj.GetComponent<Point>().enabled = true;
        }
    }
}
