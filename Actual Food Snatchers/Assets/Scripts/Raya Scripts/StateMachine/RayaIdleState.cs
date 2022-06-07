using UnityEngine;

namespace Raya
{
    public class RayaIdleState : State<RayaBot>
    {
        public override void Enter(RayaBot raya)
        {
            //raya.FindClosestFood();
            Debug.Log("Hahahaha");
        }

        public override void Update(RayaBot raya)
        {
            Debug.Log("Haha");
        }

        public override void OnCollisionEnter(RayaBot raya)
        {

        }

    }
}
