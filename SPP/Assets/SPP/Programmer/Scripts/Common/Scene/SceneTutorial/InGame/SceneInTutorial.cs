/**************************************************************************************/
/*! @file   SceneGame.cs
***************************************************************************************
@brief      ゲームシーンの管理（Initializer）
***************************************************************************************
@author     Kaneko Kazuki
***************************************************************************************/

using UnityEngine;
using System.Collections;

public class SceneInTutorial : SceneBase
{

    [SerializeField]
    private GameObject[] stagePrefs;

    protected override void mOnRegistered()
    {
        base.mOnRegistered();
    }

    protected override void Start()
    {
        //初期化したい順番ごとにクラスを追加していく
        var type = (ECourseType)PlayerPrefs.GetInt(SaveKey.mTutorialKey);
        Debug.Log("StageType" + type);
        mCreate(stagePrefs[(int)type]);
        //ECourseType.
    }
}
