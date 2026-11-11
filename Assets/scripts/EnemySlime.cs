using UnityEngine;
using System.Collections;

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
            StartCoroutine(Died());
        }
    }

    private IEnumerator Died()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
    
    private void StateManager()
    {
        switch (State)
        {
            case EnemyState.IDLE:
                break;
            case EnemyState.ALERT:
                break;
            case EnemyState.EXPLORE:
                break;
            case EnemyState.PATROL:
                break;
            case EnemyState.FOLLOW:
                break;
            case EnemyState.FURRY:
                break;
           
                private Ienumerator IdleRoutine()

    

       }
   }
}