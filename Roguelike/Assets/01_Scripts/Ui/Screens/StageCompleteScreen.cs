using UnityEngine;
using UnityEngine.UI;

public class StageCompleteScreen : UIScreen
{

    [SerializeField] private Button tapToStandby;

    public override void Init()
    {
        tapToStandby.onClick.AddListener(() => GameManager.Instance.UpdateState(GameState.STANDBY));
    }

}