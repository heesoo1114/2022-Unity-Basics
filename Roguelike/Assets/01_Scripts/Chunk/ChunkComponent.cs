using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Random = System.Random;

public class ChunkComponent : IComponent
{
    public const int ChunkSize = 32;

    private const int SEED = 1;

    private Random random;

    private Subject<Chunk> chunkStream = new();
    private Subject<Chunk> chunkRemoveStream = new();

    private List<Chunk> chunks = new();

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
        GameManager.Instance.GetGameComponent<PlayerComponent>()
            .PlayerChunkMoveSubscribe(PlayerChunkMoveEvent);

        random = new Random(SEED.GetHashCode());
    }

    private void PlayerChunkMoveEvent(Vector3Int index)
    {
        Debug.Log(index);

        CreateChunk(index);

        for (var i = 0; i < chunks.Count; i++)
        {
            var distance = Vector3Int.Distance(index, chunks[i].index);

            if (distance < 2)
            {
                chunkStream.OnNext(chunks[i]);
            }
            else
            {
                chunkRemoveStream.OnNext(chunks[i]);

                chunks.RemoveAt(i);

                i--;
            }
        }
    }

    private void CreateChunk(Vector3Int index)
    {
        for (var i = index.x - 1; i <= index.x + 1; i++)
        {
            for (var j = index.y - 1; j <= index.y + 1; j++)
            {
                var chunkMap = new int[ChunkSize, ChunkSize];
                var chunkIndex = new Vector3Int(i, j);

                RandomFillGrassAndHillMap(chunkMap);

                for (var z = 0; z < 20; z++)
                    SmoothGrassAndHillMap(chunkMap);

                RandomFillGrassAndDirtMap(chunkMap);

                for (var z = 0; z < 20; z++)
                    SmootGrassAndDirtMap(chunkMap);

                RandomDecorFillMap(chunkMap);

                var chunk = new Chunk(chunkMap, chunkIndex);

                if (!IsExist(chunk))
                    chunks.Add(chunk);
            }
        }
    }

    private void RandomFillGrassAndHillMap(int[,] map)
    {
        for (var x = 0; x < ChunkSize; x++)
        {
            for (var y = 0; y < ChunkSize; y++)
            {
                if (x == 0 || x == ChunkSize - 1 || y == 0 || y == ChunkSize - 1)
                {
                    map[x, y] = 0;
                }
                else
                {
                    map[x, y] = (random.Next(0, 100) < 50) ? 0 : 1;
                }
            }
        }
    }

    private void SmoothGrassAndHillMap(int[,] map)
    {
        for (var x = 0; x < ChunkSize; x++)
        {
            for (var y = 0; y < ChunkSize; y++)
            {
                var neighbourWallTiles = GetSurroundingGrassCount(map, x, y);

                if (neighbourWallTiles > 4) map[x, y] = 0;
                else if (neighbourWallTiles < 4) map[x, y] = 1;
            }
        }
    }

    private int GetSurroundingGrassCount(int[,] map, int gridX, int gridY)
    {
        var grassCount = 0;

        for (var neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (var neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < ChunkSize && neighbourY >= 0 && neighbourY < ChunkSize)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        grassCount += map[neighbourX, neighbourY] == 0 ? 1 : 0;
                    }
                }
                else
                {
                    grassCount++;
                }
            }
        }

        return grassCount;
    }

    private void RandomFillGrassAndDirtMap(int[,] map)
    {
        for (var x = 0; x < ChunkSize; x++)
        {
            for (var y = 0; y < ChunkSize; y++)
            {
                if (map[x, y] == 0)
                    map[x, y] = (random.Next(0, 100) < 50) ? 0 : 2;
            }
        }
    }

    private void SmootGrassAndDirtMap(int[,] map)
    {
        for (var x = 0; x < ChunkSize; x++)
        {
            for (var y = 0; y < ChunkSize; y++)
            {
                if (map[x, y] == 1)
                    continue;

                var neighbourWallTiles = GetSurroundinAllTileWithoutDirtTileCount(map, x, y);

                if (neighbourWallTiles > 4) map[x, y] = 0;
                else if (neighbourWallTiles < 4) map[x, y] = 2;
            }
        }
    }


    private int GetSurroundinAllTileWithoutDirtTileCount(int[,] map, int gridX, int gridY)
    {
        var grassCount = 0;

        for (var neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (var neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < ChunkSize && neighbourY >= 0 && neighbourY < ChunkSize)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        grassCount += map[neighbourX, neighbourY] != 2 ? 1 : 0;
                    }
                }
                else
                {
                    grassCount++;
                }
            }
        }

        return grassCount;
    }

    private void RandomDecorFillMap(int[,] map)
    {
        for (var x = 0; x < ChunkSize; x++)
        {
            for (var y = 0; y < ChunkSize; y++)
            {
                if (map[x, y] == 0)
                    map[x, y] = UnityEngine.Random.Range(0, 101) < 90 ? 0 : 3;
            }
        }
    }

    private bool IsExist(Chunk target)
    {
        return chunks.Any(chunk => chunk.index.Equals(target.index));
    }

    public void ChunkCreateSubscribe(Action<Chunk> action)
    {
        chunkStream.Subscribe(action);
    }

    public void ChunkRemoveSubscribe(Action<Chunk> action) { chunkRemoveStream.Subscribe(action); }

}