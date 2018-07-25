using UnityEngine;

public class GolemController : AMonsterController
{
    [SerializeField]
    private Animator Animator;

    public override void Atak(Transform target)
    {
        base.Atak(target);
        Animator.SetBool("Atak", true);

        Animator.SetBool("Walk", false);
    }

    public override void GoToTarget(Transform target)
    {
        base.GoToTarget(target);
        Animator.SetBool("Atak", false);

        Animator.SetBool("Walk", true);
    }

    protected override void OnEnabling()
    {
        base.OnEnabling();
        Animator.speed = Random.Range(0.8f, 1f);
    }

    protected override void OnDisabling()
    {

    }

    private void FinishAtak()
    {
        Animator.SetBool("Atak", false);
    }
}
