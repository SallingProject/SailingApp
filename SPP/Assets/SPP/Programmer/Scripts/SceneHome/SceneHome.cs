using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SceneHome : BaseObject
{
    [SerializeField]
    Button m_homeButton;



    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        m_homeButton.onClick.AddListener(() =>
        {
            Debug.Log("押されました");
        });
    }


}