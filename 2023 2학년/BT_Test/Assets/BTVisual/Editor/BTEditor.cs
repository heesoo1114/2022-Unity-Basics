using System;
using BTVisual;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class BTEditor : EditorWindow
{
    
        
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private BehaviourTreeView _treeView;
    private InspectorView _inspectorView;
    private IMGUIContainer _blackboardView;

    private SerializedObject _treeObject;
    private SerializedProperty _blackboardProperty;
    
    [MenuItem("Window/BTEditor")]
    public static void OpenWindow()
    {
        BTEditor wnd = GetWindow<BTEditor>();
        wnd.titleContent = new GUIContent("BTEditor");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int line)
    {
        if (Selection.activeObject is BehaviourTree)
        {
            OpenWindow();
            return true;
        }

        return false;
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        
        // Instantiate UXML
        var template = m_VisualTreeAsset.Instantiate();
        template.style.flexGrow = 1;
        root.Add(template);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BTVisual/Editor/BTEditor.uss");
        root.styleSheets.Add(styleSheet);

        _treeView = root.Q<BehaviourTreeView>("TreeView"); //이름은 생략해도 동작한다.
        _inspectorView = root.Q<InspectorView>("Inspector");
        _blackboardView = root.Q<IMGUIContainer>("black-imgui");
        _blackboardView.onGUIHandler = () =>
        {
            if (_treeObject != null && _treeObject.targetObject != null)
            {
                _treeObject.Update(); //갱신후에
                EditorGUILayout.PropertyField(_blackboardProperty); //프로퍼티 그려준다.
                _treeObject.ApplyModifiedProperties(); //갱신사항 적용
            }
        };
        _treeView.OnNodeSelected += OnSelectionNodeChanged;
        OnSelectionChange();
    }

    /// <summary>
    /// 그래프에서 선택된 노드가 변경되었을 때
    /// </summary>
    /// <param name="nodeView">그래프 노드</param>
    private void OnSelectionNodeChanged(NodeView nodeView)
    {
        _inspectorView.UpdateSelection(nodeView);
    }

    private void OnSelectionChange()
    {
        //마우스로 클릭한 오브젝트가 BehaviourTree라면
        BehaviourTree tree = Selection.activeObject as BehaviourTree;

        if (tree == null) //만약 BTSO가 아니라면 혹시 게임오브젝트인지 검사
        {
            if (Selection.activeGameObject)
            {
                var runner = Selection.activeGameObject.GetComponent<BehviourTreeRunner>();
                if (runner != null)
                {
                    tree = runner.tree;
                }
            }
        }

        if (Application.isPlaying)
        {
            if (tree != null)
            {
                _treeView?.PopulateView(tree);
            }   
        }
        else
        {
            if (tree != null && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                _treeView?.PopulateView(tree);
            }    
        }

        //오브젝트 직렬화후 블랙보드 프로퍼티만 뺀다.
        if (tree != null)
        {
            _treeObject = new SerializedObject(tree);
            _blackboardProperty = _treeObject.FindProperty("blackboard");
        }
    }


    private void OnEnable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged; //안전을 위해
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        switch (state)
        {
            case PlayModeStateChange.EnteredEditMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingEditMode:
                break;
            case PlayModeStateChange.EnteredPlayMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingPlayMode:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void OnInspectorUpdate()
    {
        _treeView?.UpdateNodeStates();
    }
}
