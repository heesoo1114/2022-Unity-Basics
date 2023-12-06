using UnityEngine;

public class ItemObjectTrigger : MonoBehaviour
{
    private ItemObject _itemObject;

    private void Awake()
    {
        _itemObject = transform.parent.GetComponent<ItemObject>();  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _itemObject.PickUpItem();
        }
    }
}
