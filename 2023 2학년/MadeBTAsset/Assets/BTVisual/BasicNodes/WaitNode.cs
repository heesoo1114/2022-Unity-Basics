using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual.BasicNode
{
    public class WaitNode : ActionNode
    {
        public float duration = 1f; // 대기 시간
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
            // 시간이 지나면
            if (Time.time - _startTime > duration)
            {
                return State.SUCCESS;
            }

            return State.RUNNING;
        }
    }
}
