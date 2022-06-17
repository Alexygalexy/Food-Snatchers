using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{

    /// <summary>
    /// 
    /// This script allows the state machine scripts to use all of theses variables between each other. Additionally can be used also in the Alex_Bot,
    /// which inherits from Monobehaviour class
    /// 
    /// -Alex
    /// 
    /// </summary>
    public class BotStateReusableData
    {
        public Transform alexMovePoint { get; set; }
        public Transform foodPos { get; set; }
        public Transform playerPos { get; set; }
        public Vector3 evadePos { get; set; }

        public bool canInvis { get; set; } = true;
        public bool cdInvis { get; set; } = false;
        public bool isInvis { get; set; } = true;
        public bool willSnatch { get; set; } = false;

        public int timeToSnatch { get; set; } = 0;

        public float invisTime { get; set; } = 3f;
        public float navSpeed { get; set; } = 3.5f;
    }
}