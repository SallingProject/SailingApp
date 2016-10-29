using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIWind : BaseObject
{

    

    /* 初期化 */
    public float y = 0.0f;

   

    public override void mOnUpdate()
    {

        base.mOnUpdate();

        /* 右矢印を押している間右に回転 */
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, y + 0.2f, 0, Space.World);

        }

        /* 左矢印を押している間左に回転 */
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            transform.Rotate(0, y - 0.2f, 0, Space.World);

        }

        



    }


}