using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIWind : BaseObject
{

    [SerializeField]
    private WindObject m_wind;


    public override void mOnUpdate()
    {

        base.mOnUpdate();
        transform.Rotate(0, m_wind.mWindDirection, 0);
        //transform.eulerAngles += new Vector3(0, m_wind.mWindDirection+1, 1);
    }
}