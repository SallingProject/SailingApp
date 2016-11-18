using UnityEngine;
using System.Collections;

public class PointCreater : BaseObject{


    private PointArrayObject m_pointArray = new PointArrayObject();

    protected override void mOnRegistered()
    {
        mUnregisterList(this);
        mUnregister();
    }


    // Use this for initialization
    protected override void Start()
    {
        m_pointArray.mGetPoint().GetComponent<Point>().enabled = true;
    }
}
