using System;
using System.Collections.Generic;
using UnityEngine;

public class FighterAnimationController : MonoBehaviour
{
    [SerializeField] private Animator m_animator;

    private const string LAYER_NAME = "Base Layer.";

    private float m_currentAnimationTime;
    private float m_currentAnimationDuration;

    private FighterAnimations m_nextAnimation;
    private bool m_isPaused;

    private readonly Dictionary<int, float> m_animationLengths = new();
    private static readonly Dictionary<FighterAnimations, int> m_animationHashes = new()
    {
        { FighterAnimations.Idle, Animator.StringToHash(LAYER_NAME + "Idle") },
        { FighterAnimations.Crouching, Animator.StringToHash(LAYER_NAME + "Crouching") },
        { FighterAnimations.Overhead, Animator.StringToHash(LAYER_NAME + "Overhead") },
        { FighterAnimations.Low, Animator.StringToHash(LAYER_NAME + "Low") },
        { FighterAnimations.JumpIn, Animator.StringToHash(LAYER_NAME + "JumpIn") },
    };

    public bool AnimationFinished => m_currentAnimationTime >= m_currentAnimationDuration;

    public void PlayAnimation(FighterAnimations animation)
    {
        m_nextAnimation = 0;
        m_currentAnimationTime = 0;
        m_currentAnimationDuration = GetAnimationLength(animation);
        m_animator.Play(m_animationHashes[animation], 0, 0);
    }

    public void PauseAnimation()
    {
        m_animator.speed = 0;
        m_isPaused = true;
    }

    public void UnpauseAnimation()
    {
        m_animator.speed = 1;
        m_isPaused = false;
    }

    public void QueueAnimation(FighterAnimations animation)
    {
        m_nextAnimation = animation;
    }

    public float GetAnimationLength(FighterAnimations animation)
    {
        return m_animationLengths[m_animationHashes[animation]];
    }

    private void Awake()
    {
        foreach (var clip in m_animator.runtimeAnimatorController.animationClips)
        {
            m_animationLengths[Animator.StringToHash(LAYER_NAME + clip.name)] = clip.length;
        }
    }

    private void FixedUpdate()
    {
        if (AnimationFinished)
        {
            if (m_nextAnimation != 0)
            {
                PlayAnimation(m_nextAnimation);
            }
        }
        else
        {
            if (!m_isPaused)
            {
                m_currentAnimationTime += Time.fixedDeltaTime;
            }
        }
    }
}
