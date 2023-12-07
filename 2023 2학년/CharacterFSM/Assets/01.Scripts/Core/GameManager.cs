using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Player _player;
    public Transform PlayerTrm => _player.transform;
    public Player Player => _player;
}
