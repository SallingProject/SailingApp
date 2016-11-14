using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneTitle : SceneBase
{
    [SerializeField]
    Button m_button;

    protected override void mOnRegistered()
    {
        base.mOnRegistered();
    }

    public void NextScene()
    {
        GameInstance.mInstance.AsyncLoad("Home");
    }
}