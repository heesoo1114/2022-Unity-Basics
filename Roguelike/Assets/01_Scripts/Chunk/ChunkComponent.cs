using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ChunkComponent : IComponent
{
    private Subject<Chunk> chunkStream = new ();

    private List<Chunk> chunks = new ();

    public const int ChunkSize = 32;

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
        GameManager.Instance.GetGameComponent<PlayerComponent>().PlayerChunkMoveSubscribe(PlayerChunkMoveEvent);
    }

    private void PlayerChunkMoveEvent(Vector3Int index)
    {
        CreateChunk(index);

        for (int i = 0; i < chunks.Count; i++)
        {
            chunkStream.OnNext(chunks[i]);  
        }
    }

    private void CreateChunk(Vector3Int index)
    {
        for (int i = index.x - 1; i <= index.x + 1; i++)
        {
            for (int j = index.y - 1; j <= index.y + 1; j++)
            {
                var chunkMap = new int[ChunkSize, ChunkSize];
                var chunkIndex = new Vector3Int(i, j);

                var chunk = new Chunk(chunkMap, chunkIndex);

                chunks.Add(chunk);
            }
        }
    }

    public void ChunkSubscribe(Action<Chunk> action)
    {
        chunkStream.Subscribe(action);
    }
}
