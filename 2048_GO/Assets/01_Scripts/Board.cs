using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Wait  = 0, Processing, End } // ( 대기, 이동 or 병합, 후처리 )

public class Board : MonoBehaviour
{
    [SerializeField] private NodeSpawner nodeSpawner;
    [SerializeField] private TouchController touchController;

    [SerializeField] private GameObject blockPrefab; // 블록 원본 프리팹
    [SerializeField] private Transform blockRect; // 생성한 블록들의 부모 Transform

    public List<Node> NodeList { private set; get; }
    public Vector2Int BlockCount { private set; get; }

    private List<Block> blockList;
    private State state = State.Wait; // 현재 상태

    private void Awake()
    {
        // 블록의 개수를 4x4 크기로 설정
        BlockCount = new Vector2Int(4, 4);

        // 노드 블록 판 생성, 모든 노드의 정보를 NodeList에 저장
        NodeList = nodeSpawner.SpawnNodes(this, BlockCount);

        blockList = new List<Block>();
    }

    private void Start()
    {
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(nodeSpawner.GetComponent<RectTransform>());

        foreach ( Node node in NodeList )
        {
            node.localPosition = node.GetComponent<RectTransform>().localPosition;
        }

        SpawnBlockToRandomNode();
        SpawnBlockToRandomNode();
    }

    private void Update()
    {
        if (IsGameOver())
        {
            OnGameOver();
        }

        if (state == State.Wait)
        {
            Direction direction = touchController.UpdateTouch();

            if (direction != Direction.None)
            {
                AllBlockProcess(direction);
            }
        }
        else
        {
            UpdateState();
        }
    }

    private void SpawnBlockToRandomNode()
    {
        #region 아래 코드와 동일한 결과
        /*List<Node> emptyNodes = new List<Node>();

        foreach ( Node node in NodeList )
        {
            if (node.placedBlock = null)
            {
                emptyNodes.Add(node);
            }
        }*/
        #endregion
        List<Node> emptyNodes = NodeList.FindAll(x => x.placedBlock == null);

        // 블록이 배치되어 있지 않은 노드가 있을 때
        if (emptyNodes.Count != 0)
        {
            // 블록이 배치되어 있지 않은 노드 중 임의의 노드를 랜덤으로 가져오고
            // 랜덤으로 가져온 노드의 point 정보를 가져온다
            int index = Random.Range(0, emptyNodes.Count);
            Vector2Int point = emptyNodes[index].Point;

            SpawnBlock(point.x, point.y);
        }
        else
        {
            if (IsGameOver())
            {
                OnGameOver();
            }
        }
    }

    private void SpawnBlock(int x, int y)
    {
        // 해당 위치에 블록이 배치되어 있으면 return
        if (NodeList[y * BlockCount.x + x].placedBlock != null) return;

        GameObject clone = Instantiate(blockPrefab, blockRect);
        Block block = clone.GetComponent<Block>();
        Node node = NodeList[y * BlockCount.x + x];

        // 생성한 블록의 위치를 노드의 위치와 동일하게 설정
        clone.GetComponent<RectTransform>().localPosition = node.localPosition;

        // 블록이 처음 생성될 때 숫자 설정 
        block.Setup();

        // 방금 생성한 블록을 노드에 등록
        node.placedBlock = block;

        // 리스트에 블록 정보 저장
        blockList.Add(block);
    }

    private void AllBlockProcess(Direction direction)
    {
        if (direction == Direction.Right)
        {
            for (int y = 0; y < BlockCount.y; ++y)
            {
                for (int x = (BlockCount.x - 2); x >= 0; --x)
                {
                    BlockProcess(NodeList[y * BlockCount.x + x], direction);
                }
            }
        }
        else if (direction == Direction.Down)
        {
            for (int y = (BlockCount.y - 2); y >= 0; --y)
            {
                for (int x = 0; x < BlockCount.x; ++x)
                {
                    BlockProcess(NodeList[y * BlockCount.x + x], direction);
                }
            }
        }
        else if (direction == Direction.Left)
        {
            for (int y = 0; y < BlockCount.y; ++y)
            {
                for (int x = 1; x < BlockCount.x; ++x)
                {
                    BlockProcess(NodeList[y * BlockCount.x + x], direction);
                }
            }
        }
        else if (direction == Direction.Up)
        {
            for (int y = 1; y < BlockCount.y; ++y)
            {
                for (int x = 0; x < BlockCount.x; ++x)
                {
                    BlockProcess(NodeList[y * BlockCount.x + x], direction);
                }
            }
        }

        foreach (Block block in blockList)
        {
            if (block.Target != null)
            {
                state = State.Processing;
                block.StartMove();
            }
        }

        if (IsGameOver())
        {
            OnGameOver();
        }
    }

    // node에 배치되어 있는 블록을 direction 방향으로 이동 or 병합 처리
    private void BlockProcess(Node node, Direction direction)
    {
        if (node.placedBlock == null) return;

        // 해당방향에 있는 노드 검사
        Node neighborNode = node.FindTarget(node, direction);

        if (neighborNode != null)
        {
            // 현재 노드와 아웃노드에 블록이 있고, 두 블록의 값이 같으면 병합 
            if (node.placedBlock != null && neighborNode.placedBlock != null)
            {
                if (node.placedBlock.Numeric == neighborNode.placedBlock.Numeric)
                {
                    Combine(node, neighborNode);
                }
            }
            // 이동하려는 방향에 노드는 있지만 블록이 없으면 노드가 비어있기 때문에 이동
            else if (neighborNode != null && neighborNode.placedBlock == null)
            {
                Move(node, neighborNode);
            }
        }
    }

    // from 노드에 있는 블록을 to 노드로 이동
    private void Move(Node from, Node to)
    {
        // from 노드에 있는 블록의 목표 노드를 to 노드로 설정
        from.placedBlock.MoveToNode(to);

        if (from.placedBlock != null)
        {
            to.placedBlock = from.placedBlock;
            from.placedBlock = null;
        }
    }

    private void Combine(Node from, Node to)
    {
        // from 노드에 있으면 블록이 to 노드에 있는 블록에 병합되도록 설정
        from.placedBlock.CombineToNode(to);
        // from 노드의 블록 정보를 비워줌
        from.placedBlock = null;
        // to 노드의 combined = true 설정해 병합되는 노드로 설정
        to.Combined = true;
    }

    private void UpdateState()
    {
        // 모든 블록의 이동, 병합 처리가 진행중이면 false
        // 모든 블록의 이동, 병합 처리가 완료되면 true
        bool targetAllNull = true;

        foreach (Block block in blockList)
        {
            if (block.Target != null)
            {
                targetAllNull = false;
                break;
            }
        }

        if (targetAllNull && state == State.Processing)
        {
            List<Block> removeBlocks = new List<Block>();

            foreach (Block block in blockList)
            {
                if (block.NeedDestory)
                {
                    removeBlocks.Add(block);
                }
            }

            removeBlocks.ForEach(x =>
            {
                blockList.Remove(x);
                Destroy(x.gameObject);
            });

            state = State.End;
        }

        if (state == State.End)
        {
            state = State.Wait;

            SpawnBlockToRandomNode();

            NodeList.ForEach(x => x.Combined = false);
        }
    }

    private bool IsGameOver()
    {
        foreach (Node node in NodeList)
        {
            // 현재 노드에 블록이 없으면 게임 진행 가능
            if (node.placedBlock == null) return false;

            // 각 노드의 이웃  노드 개수만큼 반복
            for (int i =  0;  i < node.NeighborNodes.Length; ++i)
            {
                // 이웃 노드가 없으면 건너뛴다. (바깥쪽 라인)
                if (node.NeighborNodes[i] == null) continue;

                // 이웃 노드의 정보 가져오기
                Vector2Int point = node.NeighborNodes[i].Value;
                Node neighborNode = NodeList[point.y * BlockCount.x + point.x];

                if (node.placedBlock != null && neighborNode.placedBlock != null)
                {
                    if (node.placedBlock.Numeric == neighborNode.placedBlock.Numeric)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private void OnGameOver()
    {
        Debug.Log("Game Over");
    }
}
