using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    /// <summary>
    /// 
    /// By creating an abstarct class of statemachine it is possible now to create a specific state machine for the Bot Movement in the game
    /// 
    /// -Alex
    /// </summary>
    public class BotMovementStateMachine : StateMachine
    {
        /// <summary>
        /// 
        /// This class holds all the scripts that the state machine use and references them so that they could be used in other scripts
        /// 
        /// -Alex
        /// </summary>
        public Alex_Bot Alex_Bot { get; }
        public BotStateReusableData reusableData { get; }
        public BotMovingState MovingState { get; }
        public BotEvadeState EvadeState { get; }
        public BotSnatchingState SnatchingState { get; }

        public BotMovementStateMachine(Alex_Bot alex_Bot)
        {
            Alex_Bot = alex_Bot;

            reusableData = new BotStateReusableData();

            MovingState = new BotMovingState(this);

            EvadeState = new BotEvadeState(this);

            SnatchingState = new BotSnatchingState(this);
        }
    }
}
