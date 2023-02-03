using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{
    private UIDocument _document;
    private Camera _mainCam;
    private VisualElement _potion;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _mainCam = Camera.main;
    }

    private void OnEnable()
    {
        VisualElement root = _document.rootVisualElement;
        _potion = root.Q<VisualElement>("Potion");

        _potion.AddManipulator(new Dragger(PotionDrop));
    }

    private void PotionDrop(Vector2 startPos, Vector2 endPos)
    {
        Vector2 endScreenPos = new Vector2(endPos.x, Screen.height - endPos.y);
        
        Ray ray = _mainCam.ScreenPointToRay(endScreenPos);
        RaycastHit hit;
        int playerLayer = LayerMask.NameToLayer("Player");

        bool isRayHit = Physics.Raycast(ray, out hit, _mainCam.farClipPlane, 1 << playerLayer);

        if (isRayHit)
        {
            PlayerController p = hit.collider.GetComponent<PlayerController>();
            if (p != null)
            {
                _potion.parent.Remove(_potion); // 노드트리에서만 빼는거임
                // VisualElement를 삭제할 때는 부모로부터 삭제하면 잘 됨

                p.ChangeHealth(20);
            }
        }
        else
        {
            _potion.style.top = startPos.x;
            _potion.style.left = startPos.x;
        }
    }
}
