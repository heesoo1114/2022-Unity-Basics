using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComponent : IComponent
{
    private Camera camera;

    public void UpdateState(GameState state)
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
        camera = Camera.main;

        GameManager.Instance.GetGameComponent<PlayerComponent>().PlayerMoveSubscribe(PlayerMoveEvent);
    }

    private void PlayerMoveEvent(Vector3 playerPosition)
    {
        camera.transform.position = playerPosition + (Vector3.back * 10);
    }
}
