using UnityEngine;

public interface IDamageAble
{
    public void OnDamage(int damage, Vector3 point, Vector3 normal);
}