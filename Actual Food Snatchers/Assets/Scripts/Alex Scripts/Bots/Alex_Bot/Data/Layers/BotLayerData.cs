using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BotLayerData
{
    [field: SerializeField] public LayerMask Food { get; private set; }
    [field: SerializeField] public LayerMask Player { get; private set; }
}
