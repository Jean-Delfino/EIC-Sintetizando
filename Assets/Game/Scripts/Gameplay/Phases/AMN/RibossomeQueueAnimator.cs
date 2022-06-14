using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RibossomeState{
    Sinthetizing,
    HoldingQueue,
    Exiting
}

/*
    This script will put the RNAt in their right position

    Sinthetizing : right
    HoldingQueue: middle
    Exiting: left
*/

public class RibossomeQueueAnimator : MonoBehaviour{
    [SerializeField] List<Transform> ribossomeStatePosition = default;
}
