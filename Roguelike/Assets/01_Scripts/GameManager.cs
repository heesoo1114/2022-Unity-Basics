using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameState State { get; private set; }
    
    public static GameManager Instance;

    private readonly List<IComponent> components = new();
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        BetterStreamingAssets.Initialize();
    }

    private void Start()
    {
        components.Add(new PlayerComponent());
        components.Add(new UIComponent());
        components.Add(new CameraComponent());
        components.Add(new ChunkComponent());
        components.Add(new StageComponent());

        components.Add(GetComponent<TileComponent>());
        components.Add(GetComponent<EnemyComponent>());
        
        UpdateState(GameState.INIT);
    }

    public void UpdateState(GameState state)
    {
        foreach (var component in components) 
            component.UpdateState(state);

        State = state;

        if (state == GameState.INIT)
            UpdateState(GameState.STANDBY);
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
