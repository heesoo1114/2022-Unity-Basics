using UnityEngine.UIElements;
using UnityEditor;

namespace BTVisual
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        private Editor _eidtor;

        public InspectorView()
        {
            //나중에 여기 뭔가 작성할 예정이다.
        }

        public void UpdateSelection(NodeView nv)
        {
            Clear();
            UnityEngine.Object.DestroyImmediate(_eidtor); // 이전 에디터를 즉시 해제

            _eidtor = Editor.CreateEditor(nv.node);

            IMGUIContainer container = new IMGUIContainer(() => _eidtor.OnInspectorGUI());

            Add(container);
        }
    }
}
