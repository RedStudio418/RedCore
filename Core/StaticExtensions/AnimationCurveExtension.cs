using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AnimationCurveExtension
{
    public static IEnumerable<(float time, float value)> GradualKeys(this AnimationCurve @this, float step = 0.05f)
    {
        float currentTime = @this.keys.First().time;
        float lastTime = @this.keys.Last().time;

        while (currentTime <= lastTime)
        {
            yield return (currentTime, @this.Evaluate(currentTime));
            currentTime += step;
        }

        yield break;
    }

    public static float Duration(this AnimationCurve @this) => @this.keys[^1].time;

    public static float Evaluate(this AnimationCurve @this, float time, float defaultValueOutOfTime)
    {
        if (@this.Duration() < time) return defaultValueOutOfTime;
        else return @this.Evaluate(time);
    }

}
