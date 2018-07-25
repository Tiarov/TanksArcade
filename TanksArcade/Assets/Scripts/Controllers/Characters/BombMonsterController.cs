using System.Collections;
using UnityEngine;

public class BombMonsterController : AMonsterController
{
    [SerializeField]
    private GameObject BombMonsterBody;
    [SerializeField]
    private GameObject BombWeapon;

    protected override void OnEnabling()
    {
        BombMonsterBody.SetActive(true);
        BombWeapon.SetActive(false);
    }

    protected override void OnDisabling()
    {
        BombWeapon.SetActive(false);
        GetComponent<HealthController>().enabled = true;
    }

    public override void Atak(Transform taget)
    {
        GetComponent<HealthController>().enabled = false;
        StartCoroutine(Death(0.3f));
    }

    private IEnumerator Death(float time)
    {
        BombMonsterBody.SetActive(false);
        BombWeapon.SetActive(true);
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
