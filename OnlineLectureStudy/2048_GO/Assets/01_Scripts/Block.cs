using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    [SerializeField] private Color[] blockColors; // 블록이 가질 수 있는 모든 색상
    [SerializeField] private Image imageBlock; // 블록 색상 변경을 위한 Image
    [SerializeField] private TextMeshProUGUI textBlockNumeric; // 블록 숫자 테스트 변경을 위한 Text

    private int numeric; // 블록이 가지는 숫자
    private bool combine = false; // 해당 블록의 병합 여부 (블록 이동이 종료되고 나서 병합해주기 위해) 

    // 이동 or 병합 위해 이동하는 목표 Node
    public Node Target { get; set; }

    public bool NeedDestory { get; set; } = false;
    
    public int Numeric
    {
        set
        {
            // 실제 숫자 값 변경, 블록에 출력되는 숫자 설정, 블록의 색상 설정
            numeric = value;
            textBlockNumeric.text = value.ToString();
            imageBlock.color = blockColors[(int)Mathf.Log(value, 2) - 1];
            // ex) Log(8, 2) = log(small"2")8 = 3
        }
        get => numeric;
    }

    public void Setup()
    {
        Numeric = Random.Range(0, 100) < 90 ? 2 : 4;
        StartCoroutine(OnScaleAnimation(Vector3.one * 0.5f, Vector3.one, 0.15f));
    }

    public void MoveToNode(Node to)
    {
        Target = to;
        combine = false;
    }

    public void CombineToNode(Node to)
    {
        Target = to;
        combine = true;
    }

    public void StartMove()
    {
        float moveTime = 0.1f;
        StartCoroutine(OnLocalMoveAnimation(Target.localPosition, moveTime, OnAfterMove));
    }

    private void OnAfterMove()
    {
        if (Target != null)
        {
            if (combine) // 병합되는 블록
            {
                // 블록이 병합되었기 때문에 목표 블록의 숫자들 x2
                Target.placedBlock.Numeric *= 2;
                // 목표 블록의 확대/축소 애니메이션 재생
                Target.placedBlock.StartPunchScale(Vector3.one * 0.25f, 0.15f, OnAfterAnimation);
                // 현재 블록을 비활성화
                gameObject.SetActive(false);
            }
            else // 이동하는 블록
            {
                // 목표(Target) 위치까지 이동을 완료했기 때문에 목표 해제
                Target = null;
            }
        }
    }

    public void StartPunchScale(Vector3 punch, float time, UnityAction action = null)
    {
        StartCoroutine(OnPunchScale(punch, time, action));
    }

    private void OnAfterAnimation()
    {
        Target = null;
        NeedDestory = true;
    }

    private IEnumerator OnPunchScale(Vector3 punch, float time, UnityAction action)
    {
        Vector3 current = Vector3.one;
        yield return StartCoroutine(OnScaleAnimation(current, current + punch, time * 0.5f));
        yield return StartCoroutine(OnScaleAnimation(current + punch, current, time * 0.5f));
        action?.Invoke();
    }

    private IEnumerator OnScaleAnimation(Vector3 start, Vector3 end, float time)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.localScale = Vector3.Lerp(start, end, percent);

            yield return null;
        }
    }

    private IEnumerator OnLocalMoveAnimation(Vector3 end, float time, UnityAction action)
    {
        float current = 0;
        float percent = 0;
        Vector3 start = GetComponent<RectTransform>().localPosition;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.localPosition = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        if (action != null) action.Invoke();
    }
}
