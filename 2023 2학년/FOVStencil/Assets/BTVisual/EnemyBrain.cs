using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBrain : MonoBehaviour
{
    public Transform targetTrm;
    //기타 등등 브레인에서 필요한 것들을 여기 넣어라
    public abstract void Attack();
}
