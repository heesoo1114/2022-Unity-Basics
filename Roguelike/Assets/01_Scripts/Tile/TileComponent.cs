using UnityEngine;
using UnityEngine.Tilemaps;

public class TileComponent : MonoBehaviour, IComponent
{
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private TileBase tilebase;

    private int size = 64;

    public void UpdateState(GameState state)
    {
        switch (state)
        {
            case GameState.STANDBY:
                Reset();
                Generate();
                break;
        }
    }

    private void Generate()
    {
        var tileStartPosition = -new Vector3Int(size / 2, size / 2);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var tilePosition = new Vector3Int((int)tileStartPosition.x + i, (int)tileStartPosition.y + j);

                tilemap.SetTile(tilePosition, tilebase);
            }
        }
    }

    private void Reset()
    {
        tilemap.ClearAllTiles();
    }
}
