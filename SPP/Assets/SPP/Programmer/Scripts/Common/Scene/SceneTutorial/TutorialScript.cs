using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript : SceneBase
{
    [SerializeField]
    protected override void mOnRegistered()
    {
        base.mOnRegistered();
    }
    public void ButtonPush()
    {
        Debug.Log("Button Push !!");
    }

}
