
using System.Collections.Generic;

public enum PlayerStateEnum
{
    Idle,
    Move,
    Jump,
    Fall,
    Dash,
    WallSlide,
    WallJump,
    PrimaryAttack
}


public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }
    public Dictionary<PlayerStateEnum, PlayerState> StateDictionary 
        = new Dictionary<PlayerStateEnum, PlayerState>();

    private Player _player;

    public void Initialize(PlayerStateEnum startState, Player player)
    {
        _player = player;
        CurrentState = StateDictionary[startState];
        CurrentState.Enter();
    }


    public void ChangeState(PlayerStateEnum newState)
    {
        CurrentState.Exit();
        CurrentState = StateDictionary[newState];
        CurrentState.Enter();
    }
    
    public void AddState(PlayerStateEnum state, PlayerState playerState)
    {
        StateDictionary.Add(state, playerState);
    }

}