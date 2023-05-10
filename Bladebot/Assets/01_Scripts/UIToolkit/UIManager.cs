using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private UIDocument _document;
    private VisualElement _root;
    private EnemyHPBar _enemyBar;

    [SerializeField]
    private float _enemyBarTimer = 4f;
    private float _currentEnemybarTimer = 0;

    private EnemyHealth _subscribedEnemy = null;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
    }
}
