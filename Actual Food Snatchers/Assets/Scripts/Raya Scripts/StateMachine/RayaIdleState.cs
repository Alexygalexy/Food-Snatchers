using UnityEngine;

namespace Raya
{
    public class RayaIdleState : State<RayaBot>
    {
        public override void Enter(RayaBot raya)
        {
            raya.FindClosestFood();
        }

        public override void Update(RayaBot raya)
        {

        }

        public override void OnCollisionEnter(RayaBot raya)
        {

        }

    }
}
