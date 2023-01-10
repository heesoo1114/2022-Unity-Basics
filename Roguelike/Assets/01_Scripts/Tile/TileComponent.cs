using UnityEngine;
using UnityEngine.Tilemaps;

public class TileComponent : MonoBehaviour, IComponent
{
    [SerializeField] private Tilemap tile;

    [SerializeField] private TileBase tilebase;

    private int size = 64;

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
        GameManager.Instance.GetGameComponent<ChunkComponent>().ChunkSubscribe(ChunkEvent);
    }

    private void ChunkEvent(Chunk chunk)
    {
        var chunkSize = chunk.map.GetLength(0);

        var start = chunk.index * (chunkSize - 1);
        start += new Vector3Int(chunk.index.x - (int)(chunkSize * 0.5f), chunk.index.y - (int)(chunkSize * 0.5f));

        for (int i = 0; i < chunkSize; i++)
        {
            for (int j = 0; j < chunkSize; j++)
            {
                var tilePosition = new Vector3Int(start.x + i, start.y + j);

                tile.SetTile(tilePosition, tilebase);
            }
        }
    }

    private void Reset()
    {
        tile.ClearAllTiles();
    }
}
