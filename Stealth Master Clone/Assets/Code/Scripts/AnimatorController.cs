using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        _animator.Play("Idle_Battle");
    }

    public void PlayRun()
    {
        _animator.Play("RunForwardBattle");
    }

    public void PlayAttack()
    {
        _animator.Play("Attack01");
    }


    public void PlayDie()
    {
        _animator.Play("Die");
    }
}
