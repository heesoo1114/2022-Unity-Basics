using UnityEngine;

public class Clip : MonoBehaviour
{
    public int maxAmmo;
    private int _currentAmmo;
    public int Ammo
    {
        get => _currentAmmo;
        set
        {
            _currentAmmo = Mathf.Clamp(value, 0, maxAmmo);
        }
    }

    public bool IsEmpty => _currentAmmo == 0;
    public int EmptyCount => maxAmmo - _currentAmmo;

    private void Start()
    {
        _currentAmmo = maxAmmo;
    }

    public void Reload()
    {
        _currentAmmo += EmptyCount;
    }
}
