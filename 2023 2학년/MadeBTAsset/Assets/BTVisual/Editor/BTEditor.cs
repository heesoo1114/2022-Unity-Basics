using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BTEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/BTEditor")]
    public static void OpenWindow()
    {
        BTEditor wnd = GetWindow<BTEditor>();
        wnd.titleContent = new GUIContent("BTEditor");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        var styleSheed = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BTVisual/Editor/BTEditor.uss");
        root.styleSheets.Add(styleSheed);
    }
}
