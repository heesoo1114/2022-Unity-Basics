using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using Unity.EditorCoroutines.Editor;
using System.IO;

#if UNITY_EDITOR

public class SpreadDataLoader : EditorWindow
{
    [MenuItem("Tools/SpreadDataLoader")]
    public static void OpenWindow()
    {
        EditorWindow wnd = GetWindow<SpreadDataLoader>();
        wnd.titleContent = new GUIContent("�������� ��Ʈ �δ�");
    }

    private string _documentID = "1bcwnK6zfc3nseuCMuMmFQE8zf56LVDS_l7tfztdELX8";
    private Label _statusLabel;

    // â �����߰� �ȿ� �ִ� ������ ä�� �غ� �������� ȣ��Ǵ� �޽���
    private void CreateGUI()
    {
        VisualTreeAsset ui = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editors/UI/SpreadDataLoader.uxml");
        VisualElement rootUI = ui.Instantiate();
        rootUI.style.width = new Length(100, LengthUnit.Percent);
        rootVisualElement.Add(rootUI);

        AddEvent(rootUI);
    }

    private void AddEvent(VisualElement rootUI)
    {
        TextField urlText = rootUI.Q<TextField>("URLText");
        _statusLabel = rootUI.Q<Label>("StatusLabel");
        VisualElement loadingPopup = rootUI.Q<VisualElement>("LoadingPopup");

        urlText.SetValueWithoutNotify(_documentID);

        rootUI.Q<Button>("LoadBtn").RegisterCallback<ClickEvent>(evt =>
        {
            _statusLabel.text = "";
            
            loadingPopup.visible = true;
            int coroutineCount = 0;

            EditorCoroutineUtility.StartCoroutine(GetDataFromSheed(urlText.value, "0", (sucess, data) =>
            {
                if (sucess)
                {
                    HandleData(data, CreateSourceCode);
                }
                else
                {
                    _statusLabel.text = data;
                    // statusLabel�� css�� error��� Ŭ������ ����� �ش� Ŭ���� �ο��ϱ�
                }
                coroutineCount++;
                if (coroutineCount == 2) loadingPopup.visible = false;
            }), this);

            EditorCoroutineUtility.StartCoroutine(GetDataFromSheed(urlText.value, "1614758744", (sucess, data) =>
            {
                if (sucess)
                {
                    HandleData(data, CreateSciptableObject);
                }
                else
                {
                    _statusLabel.text = data;
                    // statusLabel�� css�� error��� Ŭ������ ����� �ش� Ŭ���� �ο��ϱ�
                }
                coroutineCount++;
                if (coroutineCount == 2) loadingPopup.visible = false;
            }), this);
        });
    }

    private void CreateSciptableObject(string[] dataArr)
    {
        // data order : �̸�, hp, dex, critical

        // SO�� asset����
        CharDataSO asset = AssetDatabase.LoadAssetAtPath<CharDataSO>($"Assets/SO/{dataArr[0]}.asset");
        if (asset == null) // ���̸� �����ϱ�, ���⼭�� ����
        {
            asset = ScriptableObject.CreateInstance<CharDataSO>();
            string fileName = AssetDatabase.GenerateUniqueAssetPath($"Assets/SO/{dataArr[0]}.asset");
            AssetDatabase.CreateAsset(asset, fileName); // �޸𸮻� �ִ� asset�̶�� SO�� ���ϰ�λ� ����
        }

        asset.MaxHP = int.Parse(dataArr[1]);
        asset.Dex = int.Parse(dataArr[2]);
        asset.critical = float.Parse(dataArr[3]);

        AssetDatabase.SaveAssets(); // �޸𸮻󿡼� ����� ���� ���Ϸ� ����
    }

    private void CreateSourceCode(string[] dataArr)
    {
        // ������ �̸�, Ŭ������, Ÿ��
        string code = string.Format(CodeFormat.CharacterFormat, dataArr[1], dataArr[0], dataArr[2]);
        string path = $"{Application.dataPath}/01_Scripts/Characters/";
        // ���� ������ �����ϸ� ����°�
        File.WriteAllText($"{path}{dataArr[1]}.cs", code);
    }

    private void HandleData(string data, Action<string[]> JobAction)
    {
        // data = data.Replace("\r", "");
        string[] lines = data.Split("\n"); // ���κ��� ������ �迭�� ����
        int lineNum = 1;

        for (lineNum = 1; lineNum < lines.Length; lineNum++)
        {
            string[] dataArr = lines[lineNum].Split("\t"); // ���� csv�� �ߴٸ� ���⼭ "\t" ��� "," �� ���ָ� �ȴ�
            JobAction(dataArr);
        }
        
        _statusLabel.text += $"{lineNum}���� �����Ͱ� ó���Ǿ����ϴ�.\n";
        AssetDatabase.Refresh(); // ���� ���ΰ�ħ
    }

    // �� �ڷ�ƾ�� ǥ��ȭ�� ���� ���� �ʿ�
    IEnumerator GetDataFromSheed(string documentID, string sheetID, Action<bool, string> Process)
    {
        string url = $"https://docs.google.com/spreadsheets/d/{documentID}/export?format=tsv&gid={sheetID}";
        Debug.Log(url);
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest(); // ������ �� ������ �ڷ�ƾ�� ����

        if (req.result == UnityWebRequest.Result.ConnectionError || req.responseCode != 200)
        {
            Process?.Invoke(false, "���� �ҷ������� ���� �߻�");
            yield break; 
        }

        Process?.Invoke(true, req.downloadHandler.text); // ���μ������� ������ �����͸� �Ѱ��ش�
    }
}

#endif