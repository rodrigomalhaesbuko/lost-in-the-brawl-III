using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Bolt.Matchmaking;
using UnityEngine.InputSystem;

public class Building : Bolt.EntityBehaviour<IBuildingState>
{
    public override void Attached()
    {
        state.SetTransforms(state.Transform, gameObject.transform);
    }
}
