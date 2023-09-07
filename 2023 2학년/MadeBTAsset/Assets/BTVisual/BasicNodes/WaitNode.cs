using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual.BasicNode
{
    public class WaitNode : ActionNode
    {
        public float duration = 1f; // ��� �ð�
        private float _startTime;

        protected override void OnStart()
        {
            _startTime = Time.time;
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            // �ð��� ������
            if (Time.time - _startTime > duration)
            {
                return State.SUCCESS;
            }

            return State.RUNNING;
        }
    }
}
