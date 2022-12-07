using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator anim;

    EnemyController enemy;
    void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<EnemyController>();
    }

    private void PlayHurtAnimation()
    {
        anim.SetTrigger("Hurt");
    }

    private void PlayDieAnimation()
    {
        anim.SetTrigger("Die");
    }

    private void EnemyHurt(EnemyController _enemy)
    {
        if (enemy == _enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }

    private void EnemyDead(EnemyController _enemy)
    {
        if (enemy == _enemy)
        {
            StartCoroutine(PlayDie());
        }
    }


    private float GetCurrentAnimationLenght()
    {
        return anim.GetCurrentAnimatorClipInfo(0).Length;
    }

    private IEnumerator PlayHurt()
    {
        enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght()-0.25f);
        enemy.ResumeMovement();
    }

    private IEnumerator PlayDie()
    {
        enemy.StopMovement();
        PlayDieAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.25f);
        enemy.GetComponent<HealthManager>().RestartHealth();
        //enemy.ResumeMovement();
        gameObject.SetActive(false);
    }

   

    private void OnEnable()
    {
        HealthManager.onEnemyHurt += EnemyHurt;
        HealthManager.onEnemyDead += EnemyDead;
    }

    private void OnDisable()
    {
        HealthManager.onEnemyHurt -= EnemyHurt;
        HealthManager.onEnemyDead -= EnemyDead;
    }

}
