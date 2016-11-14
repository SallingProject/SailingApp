using UnityEngine;
using System.Collections;

public class SceneSetup : SceneBase {

	
    protected override void Start()
    {
        base.Start();
        GameInstance.mInstance.AsyncLoad("DebugHome");
    }
}
