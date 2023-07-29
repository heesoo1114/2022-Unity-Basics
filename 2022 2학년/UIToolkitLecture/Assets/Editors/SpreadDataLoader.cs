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
        wnd.titleContent = new GUIContent("스프레드 시트 로더");
    }

    private string _documentID = "1bcwnK6zfc3nseuCMuMmFQE8zf56LVDS_l7tfztdELX8";
    private Label _statusLabel;

    // 창 오픈했고 안에 있는 내용을 채울 준비가 끝났으면 호출되는 메시지
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
                    // statusLabel에 css로 error라는 클래스를 만들고 해당 클래스 부여하기
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
                    // statusLabel에 css로 error라는 클래스를 만들고 해당 클래스 부여하기
                }
                coroutineCount++;
                if (coroutineCount == 2) loadingPopup.visible = false;
            }), this);
        });
    }

    private void CreateSciptableObject(string[] dataArr)
    {
        // data order : 이름, hp, dex, critical

        // SO는 asset파일
        CharDataSO asset = AssetDatabase.LoadAssetAtPath<CharDataSO>($"Assets/SO/{dataArr[0]}.asset");
        if (asset == null) // 널이면 없으니까, 여기서는 생성
        {
            asset = ScriptableObject.CreateInstance<CharDataSO>();
            string fileName = AssetDatabase.GenerateUniqueAssetPath($"Assets/SO/{dataArr[0]}.asset");
            AssetDatabase.CreateAsset(asset, fileName); // 메모리상에 있는 asset이라는 SO를 파일경로상에 저장
        }

        asset.MaxHP = int.Parse(dataArr[1]);
        asset.Dex = int.Parse(dataArr[2]);
        asset.critical = float.Parse(dataArr[3]);

        AssetDatabase.SaveAssets(); // 메모리상에서 변경된 값이 파일로 들어간다
    }

    private void CreateSourceCode(string[] dataArr)
    {
        // 각각이 이름, 클래스명, 타입
        string code = string.Format(CodeFormat.CharacterFormat, dataArr[1], dataArr[0], dataArr[2]);
        string path = $"{Application.dataPath}/01_Scripts/Characters/";
        // 기존 파일이 존재하면 덮어쓰는거
        File.WriteAllText($"{path}{dataArr[1]}.cs", code);
    }

    private void HandleData(string data, Action<string[]> JobAction)
    {
        // data = data.Replace("\r", "");
        string[] lines = data.Split("\n"); // 라인별로 나눠서 배열에 담음
        int lineNum = 1;

        for (lineNum = 1; lineNum < lines.Length; lineNum++)
        {
            string[] dataArr = lines[lineNum].Split("\t"); // 만약 csv로 했다면 여기서 "\t" 대신 "," 만 써주면 된다
            JobAction(dataArr);
        }
        
        _statusLabel.text += $"{lineNum}개의 데이터가 처리되었습니다.\n";
        AssetDatabase.Refresh(); // 파일 새로고침
    }

    // 이 코루틴은 표준화를 위해 변경 필요
    IEnumerator GetDataFromSheed(string documentID, string sheetID, Action<bool, string> Process)
    {
        string url = $"https://docs.google.com/spreadsheets/d/{documentID}/export?format=tsv&gid={sheetID}";
        Debug.Log(url);
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest(); // 응답이 올 때까지 코루틴은 멈춤

        if (req.result == UnityWebRequest.Result.ConnectionError || req.responseCode != 200)
        {
            Process?.Invoke(false, "서버 불러오기중 오류 발생");
            yield break; 
        }

        Process?.Invoke(true, req.downloadHandler.text); // 프로세스한테 가져온 데이터를 넘겨준다
    }
}

#endif