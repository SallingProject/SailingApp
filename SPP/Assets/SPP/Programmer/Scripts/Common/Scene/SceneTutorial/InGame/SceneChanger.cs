using UnityEngine;
using System.Collections;

public class SceneChanger : BaseObject {

    void OnDisable()
    {
        GameInstance.mInstance.mSceneLoad(new LoadInfo("Home"));
    }

}
