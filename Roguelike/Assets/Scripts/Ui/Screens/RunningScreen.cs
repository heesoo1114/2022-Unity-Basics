using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class RunningScreen : UIScreen
    {
               
        [SerializeField] private Button tapToResult;

        public override void Init()
        {
            tapToResult.onClick.AddListener(() => GameManager.Instance.UpdateState(GameState.RESULT));
        }
        
    }
}