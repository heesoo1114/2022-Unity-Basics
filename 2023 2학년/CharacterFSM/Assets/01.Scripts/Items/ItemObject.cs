using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private ItemData _itemData;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_itemData == null) return;
        if(_spriteRenderer == null )
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        _spriteRenderer.sprite = _itemData.itemIcon;
        gameObject.name = $"ItemObject-[{_itemData.itemName}]";
    }
#endif

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //이건 아이템 드랍할때 사용될 함수야.
    public void SetUpItem(ItemData itemData, Vector2 velocity)
    {
        _itemData = itemData;
        _rigidbody.velocity = velocity;
        _spriteRenderer.sprite = itemData.itemIcon;
    }

    public void PickUpItem()
    {
        Inventory.Instance.AddItem(_itemData);
        Destroy(gameObject);
    }
}
