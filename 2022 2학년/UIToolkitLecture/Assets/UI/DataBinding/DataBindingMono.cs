using DataBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DataBindingMono : MonoBehaviour
{
    private UIDocument _document;
    private TextField _nameInput;
    private TextField _infoInput;
    private DropdownField _genderInput;
    private VisualElement _content;
    private DropDownController _dropDownController;

    [SerializeField]
    private VisualTreeAsset _cardTemplate;

    [SerializeField]
    private PersonLitSO _personList;
    private Person _currentPerson = null;

    private List<Card> _cardList = new List<Card>();

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        VisualElement root = _document.rootVisualElement;
        _nameInput = root.Q<TextField>("NameInput");
        _infoInput = root.Q<TextField>("InfoInput");
        _genderInput = root.Q<DropdownField>("GenderInput");

        _nameInput.RegisterCallback<ChangeEvent<string>>(OnNameChanged);
        _infoInput.RegisterCallback<ChangeEvent<string>>(OnInfoChanged);

        _content = root.Q<VisualElement>("Content");

        // 배경 선택시 선택해제
        _content.RegisterCallback<ClickEvent>(evt =>
        {
            ClearSelect();
            _nameInput.SetValueWithoutNotify("");
            _infoInput.SetValueWithoutNotify("");
            _currentPerson = null;
        });

        _content.Clear(); //기존에 만들어있던 카드 클리어 해주고

        for (int count = 0; count < _personList.personList.Count; count++)
        {
            MakeCard(count);
        }

        // 선택 해제
        Button btnNew = root.Q<Button>("BtnNew");
        btnNew.RegisterCallback<ClickEvent>(evt => _currentPerson = null);

        // 생성
        Button btnMake = root.Q<Button>("BtnMake");
        btnMake.RegisterCallback<ClickEvent>(MakeBtnClick);
    }

    private void MakeBtnClick(ClickEvent evt)
    {
        if (_nameInput.value == "" || _infoInput.value == "" || _genderInput.value == null)
        {
            Debug.Log("입력을 안 했습니다.");
            return;
        }

        _currentPerson = null;

        PersonInfo newInfo = new PersonInfo();
        newInfo.Name = _nameInput.text;
        newInfo.Info = _infoInput.text;

        if (_genderInput.text == "남자")
        {
            newInfo.Image = _personList.MenProfileImage;
        }
        else
        {
            newInfo.Image = _personList.WomenProfileImage;
        }

        _personList.personList.Add(newInfo);
        MakeCard(_personList.personList.Count - 1);
    }

    private void MakeCard(int listCount)
    {
        PersonInfo p = _personList.personList[listCount];

        Person person = new Person($"{p.Name}", $"{p.Info}", p.Image);
        VisualElement cardXML = _cardTemplate.Instantiate().Q("CardBorder");
        _content.Add(cardXML);

        Card c = new Card(cardXML, person);
        _cardList.Add(c);

        cardXML.RegisterCallback<ClickEvent>(evt =>
        {
            // 내 선에서 끝낸다. 다음으로 넘어가지 않게.

            evt.StopPropagation();

            _currentPerson = person;

            // 변경 이벤트 발생 X
            _nameInput.SetValueWithoutNotify(p.Name);
            _infoInput.SetValueWithoutNotify(p.Info);
            
            ClearSelect();
            c.Selectvisible(true);
        });

        StartCoroutine(DelayCo(0.1f, () =>
        {
            cardXML.AddToClassList("on");
        }));
    }



    private void ClearSelect()
    {
        _cardList.ForEach(c => c.Selectvisible(false));
    }

    IEnumerator DelayCo(float time, Action Callback)
    {
        yield return new WaitForSeconds(time);
        Callback();
    }

    private void OnNameChanged(ChangeEvent<string> evt)
    {
        if (_currentPerson == null) return;
        _currentPerson.Name = evt.newValue;
    }

    private void OnInfoChanged(ChangeEvent<string> evt)
    {
        if (_currentPerson == null) return;
        _currentPerson.Info = evt.newValue;
    }
}