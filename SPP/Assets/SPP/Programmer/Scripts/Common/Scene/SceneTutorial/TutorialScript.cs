using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript : SceneBase
{

    [SerializeField]
    private EShipType m_id;

    protected override void mOnRegistered()
    {
        base.mOnRegistered();
    }
    public void ButtonPush()
    {
        PlayerPrefs.SetInt("ShipKey", (int)m_id);
        PlayerPrefs.GetInt("ShipKey");
        //.Log("Button Push !!");
    }

}
