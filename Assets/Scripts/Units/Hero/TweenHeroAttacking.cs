using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class TweenHeroAttacking : MonoBehaviour
{
    private void Update()
    {
        var endVl = transform.position.x + 0.01f;
        Tween.PositionX(transform, endValue: endVl, duration: 0.5f, cycles: -1, cycleMode: CycleMode.Yoyo);
        //Tween.PositionAtSpeed(_hero, targetPosition, spd);
    }

}
