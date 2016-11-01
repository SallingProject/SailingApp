using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
//自分用メモ
//間違えたのでここは完成したら消すのを忘れずに
public class Controller : BaseObject{
    
    /*
    void Update()
    {
        //マウスクリックされているか、されていないかの判断用
        //プロトタイプ用
        if (Input.GetMouseButton(0))
        {
            //押されている間回転
            transform.Rotate(new Vector3(0, 0, 5));
            Debug.Log("押されています");
        }
        if (Input.GetMouseButtonUp(0))
        {
            //離したら元の位置に戻る
            Debug.Log("離れました");
        }
    }*/

public class Controller : BaseObject{

    [SerializeField]
    Button m_controllerButton;



    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        m_controllerButton.onClick.AddListener(() =>
        {
            //押されている間回転
            transform.Rotate(new Vector3(0, 0, 5));
            Debug.Log("押されています");
        });
    }
}
