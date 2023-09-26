using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBrain : MonoBehaviour
{
    public Transform targetTrm;

    public LayerMask whatIsObstacle;
    //기타 등등 브레인에서 필요한 것들을 여기 넣어라

    public abstract void Attack();
}
