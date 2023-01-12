using UnityEngine;

public struct Chunk
{

    public readonly int[,] map;

    public readonly Vector3Int index;

    public Chunk(int[,] map, Vector3Int index)
    {
        this.map = map;

        this.index = index;
    }

}