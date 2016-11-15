using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIWind : BaseObject
{


    [SerializeField]
    private BaseObject m_player;
    /* 初期化 */
    public float y = 0.0f;


    public override void mOnUpdate()
    {

        base.mOnUpdate();
        transform.rotation = Quaternion.Euler(50, m_player.transform.rotation.y, 0);



    }


}