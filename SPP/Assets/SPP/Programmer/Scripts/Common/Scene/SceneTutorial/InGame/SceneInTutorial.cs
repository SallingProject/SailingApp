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

    /*
    とりあえずGameInstanceを持ってくる
    */
    [SerializeField]
    private PointCreater m_pointCreater;

    protected override void mOnRegistered()
    {
        base.mOnRegistered();
    }

    protected override void Start()
    {
        //初期化したい順番ごとにクラスを追加していく
        m_pointCreater.mInitializer();

    }
}
