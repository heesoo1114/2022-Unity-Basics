using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    [Serializable]
    public class BlackBoard : MonoBehaviour
    {
        public Vector3 moveToPosition;
        public Vector3 lastSpotPosition;
        public LayerMask whatisEnemy;
    }
}
