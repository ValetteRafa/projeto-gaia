
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private int hp;
    private bool IsDie;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void GetHit(int amount)
    {
        hp -= amount;

        if (hp > 0)
        {
            anim.SetTrigger("getHit");
        }
        else
        {
            anim.SetTrigger("die");
            IsDie = true;
        }
    }
}
