using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PopupButton : BaseObject{
    
    [System.Serializable]
    class ButtonText
    {
        [SerializeField]
        Button _button;
        public Button Button
        {
            get { return _button; }
        }

        [SerializeField]
        Text _text;
        public Text Text
        {
            get{ return _text; }
        }

        [SerializeField]
        EButtonId _id;
        public EButtonId Id
        {
            get{return _id;}
        }
    }

    [SerializeField]
    ButtonText m_ok;

    [SerializeField]
    ButtonText m_cancel;

    public System.Action<EButtonId> mOnClickCallback
    {
        private get;
        set;
    }
    protected override void Awake()
    {
        base.Awake();
        mUnregisterList(this);
        this.transform.SetActive(false);
    }

    public void Init()
    {
        if (m_ok.Button != null)
        {
            m_ok.Button.onClick.AddListener(() =>
            {
                if (mOnClickCallback != null)
                {
                    mOnClickCallback.Invoke(m_ok.Id);
                }
            });
        }

        if (m_cancel.Button != null)
        {
            m_cancel.Button.onClick.AddListener(() =>
            {
                if (mOnClickCallback != null)
                {
                    mOnClickCallback.Invoke(m_cancel.Id);
                }
            });
        }
    }
}
