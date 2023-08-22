using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorHierachy : MonoBehaviour
{
#if UNITY_EDITOR
    private static Dictionary<Object, ColorHierachy> coloredObjects = new Dictionary<Object, ColorHierachy>();

    static ColorHierachy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleDraw;
    }

    private static void HandleDraw(int instanceId, Rect selectionRect)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceId);

        if (obj != null && coloredObjects.ContainsKey(obj))
        {
            GameObject gObj = obj as GameObject;
            ColorHierachy ch = gObj.GetComponent<ColorHierachy>();
            if (ch != null)
            {
                PaintObject(obj, selectionRect, ch);
            }
            else
            {
                coloredObjects.Remove(obj); //이제 없으니 제거
            }
        }

    }

    private static void PaintObject(Object obj, Rect selectionRect, ColorHierachy ch)
    {
        Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height);

        if (Selection.activeObject != obj)
        {
            EditorGUI.DrawRect(bgRect, ch.backColor);

            string name = $"{ch.prefix}  {obj.name}";

            EditorGUI.LabelField(bgRect, name, new GUIStyle()
            {
                normal = new GUIStyleState() { textColor = ch.fontColor },
                fontStyle = FontStyle.Bold
            });
        }
        //선택상태일경우에는 유니티 기본으로 되도록
    }

    public string prefix;
    public Color backColor;
    public Color fontColor;

    private void Reset()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        if (false == coloredObjects.ContainsKey(this.gameObject)) // notify editor of new color
        {
            coloredObjects.Add(this.gameObject, this);
        }
    }
#endif
}
