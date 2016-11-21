using UnityEngine;
using System.Collections;

public class PointCreater : BaseObject{

    [SerializeField]
    private PointArrayObject m_pointArray;

    protected override void mOnRegistered()
    {
        mUnregisterList(this);
        mUnregister();
    }


    // Use this for initialization
    protected override void Start()
    {
        var obj = m_pointArray.mGetPoint();
        Debug.Log(obj+obj.name);
        if (obj)
        {
            obj.GetComponent<Point>().enabled = true;
        }
    }
}
