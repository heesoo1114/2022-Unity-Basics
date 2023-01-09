using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State { get; private set; }

    private readonly List<IComponent> components = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        components.Add(new UIComponent());

        components.Add(GetComponent<TileComponent>());

        UpdateState(GameState.INIT);
    }

    public void UpdateState(GameState state)
    {
        foreach (var component in components)
        {
            component.UpdateState(state);
        }

        State = state;

        if (state == GameState.INIT)
        {
            UpdateState(GameState.STANDBY);
        }
    }

    public T GetGameComponent<T>() where T : class, IComponent
    {
        var value = default(T);

        foreach (var component in components.OfType<T>())
        {
            value = component;
        }

        return value;
    }
}
