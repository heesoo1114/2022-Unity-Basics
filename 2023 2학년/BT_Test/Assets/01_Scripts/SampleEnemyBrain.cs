using System.Collections;
using UnityEngine;
using TMPro;

public class SampleEnemyBrain : EnemyBrain
{
    [SerializeField] private TextMeshProUGUI noticeText;
    [SerializeField] private ParticleSystem effect;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform fireTr;

    [SerializeField] private Container _container;

    private int maxAmmo = 8;
    private int minAmmo = 0;
    private int currentAmmo = 0;

    private bool isReloading = false;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    public override void Attack()
    {
        if (isReloading) return;

        Bullet clone = Instantiate(bulletPrefab, fireTr.position, Quaternion.Euler(fireTr.transform.forward));
        clone.Fire(transform.forward);

        effect.Play();

        currentAmmo--;
        NoticeTextUpdate();

        if (currentAmmo == 0)
        {
            StartCoroutine(Col_Reload());
        }
    }

    private IEnumerator Col_Reload()
    {
        isReloading = true;

        noticeText.text = "Reloading";
        currentAmmo = maxAmmo;
        yield return new WaitForSeconds(2f);

        NoticeTextUpdate();

        isReloading = false;
    }

    private void NoticeTextUpdate()
    {
        noticeText.text = currentAmmo.ToString();

        if (currentAmmo == maxAmmo)
        {
            for (int i = 0; i <  maxAmmo; i++)
            {
                _container.container[i].gameObject.SetActive(true);
            }
        }
        else
        {
            _container.container[currentAmmo].gameObject.SetActive(false);
        }
    }
}
