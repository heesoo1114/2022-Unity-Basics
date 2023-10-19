using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadingScreen : MonoBehaviour
{
    private UIDocument _uiDocument;
    private Label _titleLabel;
    private Label _descLabel;
    private VisualElement _loadingComplete;

    private bool _complete = false;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        VisualElement root = _uiDocument.rootVisualElement;
        _titleLabel = root.Q<Label>("title-label");
        _descLabel = root.Q<Label>("desc-label");
        _loadingComplete = root.Q<VisualElement>("load-complete");
        _loadingComplete.style.visibility = Visibility.Hidden;
        _complete = false;

        AssetLoader.OnCategoryMessage += HandleCategoryMsg;
        AssetLoader.OnDescMessage += HandleDescMsg;
        AssetLoader.OnLoadComplete += HandleLoadComplete;
    }

    private void HandleCategoryMsg(string msg)
    {
        _titleLabel.text = msg;
    }

    private void HandleDescMsg(string msg)
    {
        _descLabel.text = msg;
    }

    private void HandleLoadComplete()
    {
        _titleLabel.text = "Load Complete";
        _descLabel.text = "게임을 시작합니다.";
        _complete = true;
        _loadingComplete.style.visibility = Visibility.Visible;
    }

    private void Update()
    {
        if (_complete)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                //여기에 씬 넘기는 로직이 들어오면 된다.
                SceneManager.LoadScene(SceneList.Menu);
            }
        }
    }

    private void OnDisable()
    {
        AssetLoader.OnCategoryMessage -= HandleCategoryMsg;
        AssetLoader.OnDescMessage -= HandleDescMsg;
        AssetLoader.OnLoadComplete -= HandleLoadComplete;
    }
}