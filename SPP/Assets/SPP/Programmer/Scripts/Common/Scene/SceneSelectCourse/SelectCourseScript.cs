using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectCourseScript : SceneBase {
    [SerializeField]
    Button ButtonId;

    protected override void mOnRegistered()
    {
        base.mOnRegistered();
    }
    public void onClickNextScene()
    {
        GameInstance.mInstance.mSceneLoad(new LoadInfo("Tutorial"));
    }

}
