using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{

    /// <summary>
    /// 
    /// A specific script that holds Layers for the Alex_Bot to use and to be used all around the state machine 
    /// 
    /// -Alex
    /// 
    /// </summary>
    [System.Serializable]
    public class BotLayerData
    {
        [field: SerializeField] public LayerMask Food { get; private set; }
        [field: SerializeField] public LayerMask Player { get; private set; }
    }
}