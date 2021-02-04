using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbTeste : Bolt.EntityBehaviour<ILimbState>
{

    public override void Attached()
    {
        state.SetTransforms(state.LimbTransform, gameObject.transform);
    }

}
