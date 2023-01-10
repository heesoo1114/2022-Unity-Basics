using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationComponent : IPlayerComponent
{
    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private readonly int IsWalk = Animator.StringToHash("iswalk");

    public PlayerAnimationComponent(GameObject player) : base(player)
    {
        animator = player.GetComponent<Animator>();
        spriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    public override void UpdateState(GameState state)
    {
        switch (state)
        {
            case GameState.INIT:
                Init();
                break;
        }
    }

    private void Init()
    {
        GameManager.Instance.GetGameComponent<PlayerComponent>().PlayerMoveSubscribe(PlayerMvoeEvent);
    }

    private void PlayerMvoeEvent(Vector3 playerPosition)
    {
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        spriteRenderer.flipX = direction.x < 0;

        UpdateAnimation(GetPlayerAnimationType(direction));
    }

    private void UpdateAnimation(PlayerAnimationType animationType)
    {
        switch (animationType)
        {
            case PlayerAnimationType.Walk:
                animator.SetBool(IsWalk, true);
                break;
            default:
                animator.SetBool(IsWalk, false);
                break;
        }
    }

    private PlayerAnimationType GetPlayerAnimationType(Vector2 direction)
    {
        return direction == Vector2.zero ? PlayerAnimationType.Idle : PlayerAnimationType.Walk;
    }
}

public enum PlayerAnimationType
{
    Idle,
    Walk,
}