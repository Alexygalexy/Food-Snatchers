using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    public class BotStateReusableData
    {
        public Transform alexMovePoint { get; set; }
        public Transform foodPos { get; set; }
        public Vector3 evadePos { get; set; }
        public bool canInvis { get; set; } = true;
        public bool isInvis { get; set; } = true;
    }
}