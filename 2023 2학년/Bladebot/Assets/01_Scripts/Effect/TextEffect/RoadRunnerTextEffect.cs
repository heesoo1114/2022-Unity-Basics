using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeChar
{
    public bool IsComplete = false;
    private Vector3[] _originPosition; //여기로 가는 이펙트를 만들자.
    private Vector3[] _startPosition; //여기서 시작해서
    private float _currentTime = 0; //현재 시간
    private float _delayTime = 0; //기다리는 값
    private float _effectTime = 0; //이펙트 진행 값
    private int _vertexIndex = 0; //처음 시작 버텍스 값

    private Color _startColor;
    private Color _endColor;

    public TypeChar(int vertexIndex, Vector3[] vertices, Vector3[] startPos, Color32[] colors,
            Color startColor, Color endColor, float delayTime, float effectTime)
    {
        _vertexIndex = vertexIndex;
        _originPosition = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            Vector3 point = vertices[_vertexIndex + i];
            _originPosition[i] = point;

            colors[_vertexIndex + i].a = 0; //투명상태로 해주고
        }
        _startPosition = startPos; //시작위치도 넣어주고

        _startColor = startColor;
        _endColor = endColor;
        _delayTime = delayTime;
        _effectTime = effectTime;
    }

    public void UpdateChar(Vector3[] vertices, Color32[] colors)
    {
        _currentTime += Time.deltaTime;
        if (_currentTime < _delayTime || IsComplete) return; //아직 딜레이중인 애니메이션이거나, 완료되었다면 리턴

        float time = _currentTime - _delayTime;
        float percent = time / _effectTime; //현재 흘러간 0~1값을 구하고

        for (int i = 0; i < 4; i++)
        {
            vertices[_vertexIndex + i] = Vector3.Lerp(_startPosition[i], _originPosition[i], percent);
            colors[_vertexIndex + i] = Color.Lerp(_startColor, _endColor, percent);
        }

        if (percent >= 1)
        {
            IsComplete = true;
        }
    }

}

public class RoadRunnerTextEffect : MonoBehaviour
{
    [SerializeField]
    private float _typeTime = 0.2f;
    [SerializeField]
    private Color _startColor, _endColor;

    private bool _isTyping = false;
    private TMP_Text _tmpText;

    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _isTyping == false)
        {
            StartEffect("Hello world! 안녕하세요");
        }
    }

    private void StartEffect(string text)
    {
        _tmpText.SetText(text);
        _tmpText.color = _endColor;
        _tmpText.ForceMeshUpdate();
        _isTyping = true;
        StartCoroutine(TypeText());
    }

    [SerializeField]
    private float _ruuningDistance = 15f;

    private IEnumerator TypeText()
    {
        List<TypeChar> charList = new List<TypeChar>();

        TMP_TextInfo textInfo = _tmpText.textInfo;
        Vector3[] vertices = textInfo.meshInfo[0].vertices;
        Color32[] colors = textInfo.meshInfo[0].colors32;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (charInfo.isVisible == false) continue;

            Vector3[] startPos = new Vector3[4];
            for (int j = 0; j < 4; j++)
            {
                if (j == 0 || j == 3)
                    startPos[j] = vertices[charInfo.vertexIndex + j] + new Vector3(_ruuningDistance, 0, 0);
                else
                    startPos[j] = vertices[charInfo.vertexIndex + j] + new Vector3(_ruuningDistance + 0.5f, 0, 0);
            }
            TypeChar t = new TypeChar(charInfo.vertexIndex, vertices, startPos, colors,
                                        _startColor, _endColor, i * _typeTime, _typeTime);
            charList.Add(t);
        }

        _tmpText.UpdateVertexData(); //전체 갱신

        bool isComplete = false;

        while (isComplete == false)
        {
            foreach (TypeChar t in charList)
            {
                t.UpdateChar(vertices, colors);
                isComplete = t.IsComplete;
            }
            //여기 왔는데 isComplete이 true라는건 모든 글자가 목적지까지 도착한거지
            _tmpText.UpdateVertexData();
            yield return null;
        }
        //여기까지 왔으면 타이핑 모두 끝난거다.
        _isTyping = false;
    }
}
