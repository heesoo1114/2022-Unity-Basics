using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private PlayerInput _playerInput;

    [SerializeField] private Transform fireTr;
    [SerializeField] private Projctile prefab;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.OnFire += FireHandle;
    }

    private void FireHandle()
    {
        Projctile clone = Instantiate(prefab, fireTr.position, Quaternion.identity);
        clone.Fire(transform.forward);
    }
}
