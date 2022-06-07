using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    public class BotMovementStateMachine : StateMachine
    {
        public Alex_Bot Alex_Bot { get; }
        public BotStateReusableData reusableData { get; }
        public BotMovingState MovingState { get; }

        public BotMovementStateMachine(Alex_Bot alex_Bot)
        {
            Alex_Bot = alex_Bot;

            reusableData = new BotStateReusableData();

            MovingState = new BotMovingState(this);
        }
    }
}
