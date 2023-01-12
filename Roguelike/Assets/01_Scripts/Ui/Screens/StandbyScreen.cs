using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class StandbyScreen : UIScreen
    {
        
        [SerializeField] private Button tapToStart;

        public override void Init()
        {
            tapToStart.onClick.AddListener(() => GameManager.Instance.UpdateState(GameState.RUNNING));
        }
        
    }
}