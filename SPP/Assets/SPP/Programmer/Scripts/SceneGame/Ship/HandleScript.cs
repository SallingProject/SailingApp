using UnityEngine;
using System.Collections;

public class HandleScript : BaseObject{


    private bool m_left_B = false;
    private bool m_right_B = false;

    public override void mOnUpdate()
    {
        if (m_left_B)
        {
            transform.Rotate(0, -ShipMove.mkMoveValue, 0);
        }
        if (m_right_B)
        {
            transform.Rotate(0, ShipMove.mkMoveValue, 0);
        }
    }




    public void mOnLeftButtonUp()
    {
        m_left_B = false;
    }
    public void mOnLeftButtonDown()
    {
        m_left_B = true;
    }

    public void mOnRightButtonUp()
    {
        m_right_B = false;
    }
    public void mOnRightButtonDown()
    {
        m_right_B = true;
    }


}
