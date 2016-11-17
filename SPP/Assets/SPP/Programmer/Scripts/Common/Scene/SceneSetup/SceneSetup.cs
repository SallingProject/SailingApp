using UnityEngine;
using System.Collections;

public class SceneSetup : SceneBase {

	
    protected override void Start()
    {
        base.Start();
        GameInstance.mInstance.mSceneLoad(new LoadInfo("DebugHome"));
    }
}
