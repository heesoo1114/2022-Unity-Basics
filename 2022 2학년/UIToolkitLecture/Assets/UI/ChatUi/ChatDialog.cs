using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChatDialog : MonoBehaviour
{
    [SerializeField]
    private VisualTreeAsset _chatTemplate;
    [SerializeField]
    private Sprite _profileSprite;

    private VisualElement _root;
    private UIDocument _document;
    private int _showIndex;
    private List<VisualElement> _chatList;
    private ScrollView _scrollView;
    private TextField _txtInput;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _document.rootVisualElement;
        _showIndex = 0;
        _chatList = _root.Query<VisualElement>(className: "chat").ToList();
        _scrollView = _root.Q<ScrollView>("ScrollViewChat"); // 스크롤뷰 가져옴

        _scrollView.contentContainer.Clear(); // 자식에 있는 애들 삭제

        // 버튼 가져오기
        Button btn = _root.Q<Button>("BtnSend");
        btn.RegisterCallback<ClickEvent>(evt =>
        {
            ShowMsg();
        });

        // 입력창 가져오기
        _txtInput = _root.Q<TextField>("InputChat");
        _txtInput.RegisterCallback<KeyDownEvent>(evt =>
        {
            if (evt.keyCode == KeyCode.Return)
            {
                ShowMsg();
            }
        });
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_showIndex >= _chatList.Count)
            {
                ClearOn();
                _showIndex = 0;
            }
            else
            {
                _chatList[_showIndex].AddToClassList("on");
                _showIndex++;
            }
        }*/

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            VisualElement chat = _chatTemplate.Instantiate();
            chat = chat.Q("ChatMessage");
            // chat.AddToClassList("chat");
            StartCoroutine(DelayCoroutine(0.1f, () =>
            {
                chat.AddToClassList("on");
                _scrollView.verticalScroller.ScrollPageDown(1);
            }));

            _scrollView.contentContainer.Add(chat);
        }*/
    }

    private void ShowMsg()
    {
        VisualElement chat = _chatTemplate.Instantiate();
        chat = chat.Q("ChatMessage");
        Label msgLabel = chat.Q<Label>("MsgLabel");

        msgLabel.text = _txtInput.text;
        _txtInput.value = "";
        
        StartCoroutine(DelayCoroutine(0.1f, () =>
        {
            chat.AddToClassList("on");
            _scrollView.verticalScroller.ScrollPageDown(1);
        }));

        _scrollView.contentContainer.Add(chat);
    }

    IEnumerator DelayCoroutine(float time, Action Callback)
    {
        yield return new WaitForSeconds(time);
        Callback();
    }

    private void ClearOn()
    {
        foreach (VisualElement chat in _chatList)
        {
            chat.RemoveFromClassList("on");
        }
    }
}
