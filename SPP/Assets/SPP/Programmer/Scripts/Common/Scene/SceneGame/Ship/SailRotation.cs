using UnityEngine;
using System.Collections;

public class SailRotation : BaseObject{

    public override void mOnUpdate()
    {
        transform.eulerAngles = new Vector3(0, GameInfo.mInstance.mGetSailRotation(), 0);
    }

}
