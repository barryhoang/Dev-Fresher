using JetBrains.Annotations;
using UnityEngine;

namespace PrimeTween
{
    public partial struct Tween
    {
        public static Tween Bezier([NotNull] Transform target, Vector3 startValue, Vector3 controlValue, Vector3 endValue, float duration, Ease ease = Ease.Default, int cycles = 1, CycleMode cycleMode = CycleMode.Restart, float startDelay = 0, float endDelay = 0, bool useUnscaledTime = false)
            => Custom(target, 0f, 1f, duration, (t, x) => target.position = Bezier(startValue, controlValue, endValue, x), ease, cycles, cycleMode, startDelay, endDelay, useUnscaledTime);
        public static Tween Bezier([NotNull] Transform target, Vector3 startValue, Vector3 controlValue, Vector3 endValue, TweenSettings settings) 
            => Custom(target, 0f, 1f, settings.duration, (t, x) => target.position = Bezier(startValue, controlValue, endValue, x), settings.ease, settings.cycles, settings.cycleMode, settings.startDelay, settings.endDelay, settings.useUnscaledTime);
        private static Vector3 Bezier(Vector3 start, Vector3 control, Vector3 end, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 p = uu * start;
            p += 2 * u * t * control;
            p += tt * end;

            return p;
        }
    }
}