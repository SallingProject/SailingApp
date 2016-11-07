using UnityEngine;
using System.Collections;

public class DebugManager : BaseObjectSingleton<DebugManager> {

    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        mCheckInstance();
        mUnregisterList(this);
        logQueue = new Queue();
        iNumLog = 20;
    }


    private Queue logQueue;      ///< ログを格納するキュー
    private uint iNumLog = 20;   ///< 格納するログの数

    /**
     * @brief ログのプッシュ(エンキュー)
     * @param str プッシュするログ
     * @param console trueならばUnityのコンソール上にも表示する
     */
    public void Log(string str, bool console = false)
    {
        if (logQueue.Count >= iNumLog) logQueue.Dequeue();

        logQueue.Enqueue(str);
        if (console) Debug.Log(str);
    }


    private void OnGUI()
    {
        RenderLog(new Rect(0, 0, 100, 20), new Color(0, 0, 0));

    }

    /**
     * @brief ログを描画する
     * @param rect 描画開始位置、および1行のサイズ
     * @param color 文字の色
     * @note Debug.Logより実行速度が早いので、細かい値の変化が見ることができる
     */
    private void RenderLog(Rect rect, Color color)
    {
        Rect curRect = rect;
        curRect.y += rect.height * (logQueue.Count - 1);
        Color prevColor = GUI.color;
        GUI.color = color;

        System.Collections.IEnumerator ienum = logQueue.GetEnumerator();
        while (ienum.MoveNext())
        {
            GUI.Label(curRect, (string)ienum.Current);
            curRect.y -= rect.height;
        }

        GUI.color = prevColor;
    }
}
