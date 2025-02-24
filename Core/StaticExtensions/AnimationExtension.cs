using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationExtension
{
    public static IEnumerator PlayAndWait(this Animation @this, AnimationClip clip) => PlayAndWait(@this, clip.name);
    public static IEnumerator PlayAndWait(this Animation @this, string name, Action onCompletion=null)
    {
        if (@this.GetClip(name).isLooping)
        {
            Debug.LogError("Looping clip, can't wait", @this);
            yield break;
        }

        @this.Play(name);
        yield return null;
        yield return new WaitWhile(() => @this.IsPlaying(name));
        onCompletion?.Invoke();
    }

    public static IEnumerator PlayUnscaledTime(this Animation @this, string clipName, Action onCompletion=null)
    {
        AnimationState _currState = @this[clipName];
        bool isPlaying = true;
        float _progressTime = 0F;
        float _timeAtLastFrame = 0F;
        float _timeAtCurrentFrame = 0F;
        float deltaTime = 0F;

        @this.Play(clipName);
        _timeAtLastFrame = Time.realtimeSinceStartup;
        while (isPlaying)
        {
            _timeAtCurrentFrame = Time.realtimeSinceStartup;
            deltaTime = _timeAtCurrentFrame - _timeAtLastFrame;
            _timeAtLastFrame = _timeAtCurrentFrame;

            _progressTime += deltaTime;
            _currState.normalizedTime = _progressTime / _currState.length;
            @this.Sample();

            if (_progressTime >= _currState.length)
            {
                if (_currState.wrapMode != WrapMode.Loop)
                {
                    isPlaying = false;
                }
                else
                {
                    _progressTime = 0.0f;
                }
            }
            yield return new WaitForEndOfFrame();
        }
        yield return null;
        onCompletion?.Invoke();
    }

    public static IEnumerator WaitForClipRunning(this Animator @this, AnimationClip clip)
    {
        var s = Animator.StringToHash(clip.name);
        var state = @this.GetCurrentAnimatorStateInfo(0);
        while (state.shortNameHash != s)
        {
            yield return null;
            state = @this.GetCurrentAnimatorStateInfo(0);
        }
        yield break;
    }
    public static bool IsRunningClip(this Animator @this, AnimationClip clip) 
        =>  @this.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash(clip.name);
    
}
