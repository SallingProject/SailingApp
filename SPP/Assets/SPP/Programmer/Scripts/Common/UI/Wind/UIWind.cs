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

        float rotationY = transform.eulerAngles.y + m_wind.mWindDirection;

        if (rotationY < 180 && rotationY > -180)
        {

            //m_WindDirectionの値分回転
            transform.Rotate(0, m_wind.mWindDirection, 0);
        }
    }
}