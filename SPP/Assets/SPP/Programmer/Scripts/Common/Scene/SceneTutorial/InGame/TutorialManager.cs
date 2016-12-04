using UnityEngine;
using System.Collections;

public class TutorialManager : BaseObject
{


    [SerializeField]
    private GameObject[] m_stagePrefs;
    [SerializeField]
    private RaceTimer m_timer;

    protected override void mOnRegistered()
    {
    }

    protected override void Start()
    {
        //ECourseType　ステージ生成
        var type = (ECourseType)PlayerPrefs.GetInt(SaveKey.mTutorialKey);
        Debug.Log("StageType" + type);
        mCreate(m_stagePrefs[(int)type]);


        if (type == ECourseType.accelerate)
        {
            m_timer.gameObject.SetActive(true);
            m_timer.StartTime();
        }

    }

    public override void mOnUpdate()
    {
        if (GameInfo.mInstance.mIsEnd)
        {
            if (m_timer.gameObject.activeSelf)
            {
                m_timer.StopTime();
            }
            GameInstance.mInstance.mSceneLoad(new LoadInfo("Home"));
            GameInfo.mInstance.mIsEnd = false;
        }
    }


}
