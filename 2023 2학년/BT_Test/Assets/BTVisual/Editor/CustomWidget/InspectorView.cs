using BTVisual;
using UnityEditor;
using UnityEngine.UIElements;

public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits>
    { }
    public new class UxmlTraits : VisualElement.UxmlTraits
    { }

    private Editor editor;
    
    public InspectorView()
    {
        
    }

    public void UpdateSelection(NodeView nodeView)
    {
        Clear(); //VisualElement의 기능으로 하위 컨텐츠를 싸그리 순차적으로 날린다.
    
        UnityEngine.Object.DestroyImmediate(editor);
        
        editor = Editor.CreateEditor(nodeView.node);
        //IMGUI컨테이너 안에 editor의 OnInspectorGUI를 실행한 결과를 띄운다.
        IMGUIContainer container = new IMGUIContainer(() =>
        {
            if (editor.target)
            {
                editor.OnInspectorGUI();    
            }
        });
        
        Add(container); //이 비쥬얼 엘레멘트에 자식으로 넣는다.
    }
}
