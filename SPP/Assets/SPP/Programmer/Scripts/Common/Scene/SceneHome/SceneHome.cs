using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SceneHome : SceneBase
{
    [SerializeField]

    public void mGoToHome()
    {
        GameInstance.mInstance.mSceneLoad(new LoadInfo("Home", LoadInfo.ELoadType.Async, 1f));
    }

    public void mGoToGame()
    {
        GameInstance.mInstance.mSceneLoad(new LoadInfo("Game", LoadInfo.ELoadType.Async, 1f));
    }

    public void mGoToLibrary()
    {
        GameInstance.mInstance.mSceneLoad(new LoadInfo("Library", LoadInfo.ELoadType.Async, 1f));
    }
}