using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject nonPrefab; // 노드 블록 원본 프리팹
    [SerializeField] private RectTransform nodeRect; // 생성한 노드들의 부모 RectTransform

    public List<Node> SpawnNodes(Board board ,Vector2Int blockCount)
    {
        List<Node> nodeList = new List<Node>(blockCount.x * blockCount.y);

        for (int y = 0; y < blockCount.y; y++)
        {
            for (int x = 0; x < blockCount.x; x++)
            {
                GameObject clone = Instantiate(nonPrefab, nodeRect.transform);

                // 현재 노드의 x, y 격자 좌표 정보
                Vector2Int point = new Vector2Int(x, y);

                // 인접 노드 정보 저장
                Vector2Int?[] neighborNodes = new Vector2Int?[4];

                Vector2Int right = point + Vector2Int.right;
                Vector2Int down = point + Vector2Int.up;
                Vector2Int left = point + Vector2Int.left;
                Vector2Int up = point + Vector2Int.down;

                if (IsValid(right, blockCount)) neighborNodes[0] = right;
                if (IsValid(down, blockCount)) neighborNodes[1] = down;
                if (IsValid(left, blockCount)) neighborNodes[2] = left;
                if (IsValid(up, blockCount)) neighborNodes[3] = up;

                // 방금 생성한 노드의 SetUp() 메소드 호출
                Node node = clone.GetComponent<Node>();
                node.Setup(board, neighborNodes, point);

                // 노드의 이름을 x, y 격자 좌표로 설정
                clone.name = $"[{node.Point.y}, {node.Point.x}]";

                // nodeList에 방금 생성한 노드 정보 저장
                nodeList.Add(node);
            }
        }

        return nodeList;
    }

    private bool IsValid(Vector2Int point, Vector2Int blockCount)
    {
        if (point.x == -1 || point.x == blockCount.x || point.y == blockCount.y || point.y == -1)
        {
            return false;
        }

        return true;
    }
}
