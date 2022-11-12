using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NavAgent : MonoBehaviour
{
    private PriorityQueue<Node> _openList;
    private List<Node> _closeList;

    private List<Vector3Int> _routePath;

    public float speed = 5f;
    public bool cornerCheck = false;

    private Vector3Int _currentPosition; // ���� Ÿ�� ��ġ
    private Vector3Int _destination; // ��ǥ Ÿ�� ��ġ

    [SerializeField]
    private Tilemap _tilemap;

    private void Awake()
    {
        _openList = new PriorityQueue<Node>();
        _closeList = new List<Node>();
    }

    private void Start()
    {
        Vector3Int cellPos = _tilemap.WorldToCell(transform.position);
        _currentPosition = cellPos;
        transform.position = _tilemap.GetCellCenterWorld(cellPos);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mPos = Input.mousePosition;
            mPos.z = 0;
            Vector3 world = Camera.main.ScreenToWorldPoint(mPos);
            Vector3Int cellPos = _tilemap.WorldToCell(world); // �̰ɷ� ���带 Ÿ�� �� ���������� ����

            // Debug.Log(cellPos);

            _destination = cellPos;

            CalcuRoute();
            PrintRoute();
        }
    }

    private void PrintRoute() // ����� ��θ� ����׷� ����.
    {

    }

    private bool CalcuRoute()
    {
        _openList.Clear();
        _closeList.Clear();

        _openList.Push(new Node { pos = _currentPosition, _parent = null, G = 0, F = CalcH(_currentPosition) });

        bool result = false; // �� �� �ִ� ������
        int cnt = 0;  // ���� �ڵ�
        
        while (_openList.Count > 0)
        {
            Node n = _openList.Pop(); // ���� ������ �� �� �ִ� �༮�� ���� ��
            FindOpenList(n);
            _closeList.Add(n); // n�� �� �� ��������� Ŭ���� ����Ʈ�� �ֱ�
            if (n.pos == _destination) // ������ �湮�ߴ� �༮�� ��������. �׷� ������.
            {
                result = true;
                break;
            }

            // �����ڵ�
            cnt++;
            if (cnt >= 10000)
            {
                Debug.Log("while ���� �ʹ� ���� ���Ƽ� ����");
                break;
            }
        }

        if (result) // ���� ã��
        {
            Node last = _closeList[_closeList.Count - 1];
            while (last._parent != null)
            {
                Debug.Log(last.pos);
                last = last._parent;
            }
        }

        return result;
    }

    // Node n �� ����� ���� ����Ʈ�� �� ã�Ƽ� _openList�� �־���
    private void FindOpenList(Node n)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x == y) continue; // �̰� ���� ��ġ�� �ڸ��ϱ� ����
                
                Vector3Int nextPos = n.pos + new Vector3Int(x, y, 0);

                Node temp = _closeList.Find(x => x.pos == nextPos); // �̹� �湮
                if (temp != null) continue;

                // Ÿ�Ͽ��� ��¥ �� �� �ִ� ������
                {
                    float g = (n.pos - nextPos).magnitude + n.G;

                    Node nextOpennode = new Node { pos = nextPos, _parent = n, G = g, F = g + CalcH(nextPos) };

                    // �ֱ� ���� �˻�
                    Node exist = _openList.Contains(nextOpennode);

                    if (exist != null)
                    {
                        if (nextOpennode.G < exist.G)
                        {
                            exist.G = nextOpennode.G;
                            exist.F = nextOpennode.F;
                            exist._parent = nextOpennode._parent;
                        }
                    }
                    else
                    {
                        _openList.Push(nextOpennode);
                    }
                }
            }
        }
    }

    private float CalcH(Vector3Int pos)
    {
        // F = G + H
        Vector3Int distance = _destination - pos;
        return distance.magnitude;
    }
}
