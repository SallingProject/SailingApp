using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneTitle : BaseObject
{
    [SerializeField]
    Button m_button;

    protected override void mOnRegistered() { }

    public void NextScene()
    {
        GameInstance.mInstance.AsyncLoad("Home");
    }
}