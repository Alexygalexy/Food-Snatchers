//ABSTRACT STATE - prototype for concrete states
using UnityEngine;

public abstract class RayaBaseState
{
    public abstract void EnterState(RayaBot raya);

    public abstract void UpdateState(RayaBot raya);

    public abstract void OnCollisionEnter(RayaBot raya);
}
