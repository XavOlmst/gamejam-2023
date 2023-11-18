using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void ResetAnimationIndex() => _animator.SetInteger("animationIndex", -1);
}
