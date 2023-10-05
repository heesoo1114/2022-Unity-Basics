using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthText;

    private int health = 100;
    private float coolTime = 1f;

    private void Start()
    {
        healthText.value = health * 0.01f;
        StartCoroutine(HealthDownLoop());
    }

    private IEnumerator HealthDownLoop()
    {
        while (true)
        {
            health--;
            healthText.value = health * 0.01f;

            yield return new WaitForSeconds(coolTime);
        }
    }
}
