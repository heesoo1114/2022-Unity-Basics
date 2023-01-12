﻿using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class ResultScreen : UIScreen
    {
        
        [SerializeField] private Button tapToStandby;

        public override void Init()
        {
            tapToStandby.onClick.AddListener(() => GameManager.Instance.UpdateState(GameState.STANDBY));
        }

    }
}