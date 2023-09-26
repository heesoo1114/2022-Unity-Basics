using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidBody;

    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void Fire(Vector3 dir)
    {
        _rigidBody.AddForce(dir * moveSpeed, ForceMode.Impulse);
        StartCoroutine(Col_Delete());
    }

    private IEnumerator Col_Delete()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}