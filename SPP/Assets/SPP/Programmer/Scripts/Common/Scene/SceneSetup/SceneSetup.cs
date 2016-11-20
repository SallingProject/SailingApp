using UnityEngine;
using System.Collections;

public class SceneSetup : SceneBase {


    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        // アプリの環境設定
        // 画面の回転する向きを固定
        Screen.autorotateToPortrait = true;   // 縦
        Screen.autorotateToPortraitUpsideDown = true;   // 上下逆
        Screen.autorotateToLandscapeLeft = false;  // 左
        Screen.autorotateToLandscapeRight = false;  // 右
    }
    
    public override void mOnUpdate()
    {
        base.mOnUpdate();
        GameInstance.mInstance.mSceneLoad(new LoadInfo("DebugHome", LoadInfo.ELoadType.Sync, 1));
    }
}
