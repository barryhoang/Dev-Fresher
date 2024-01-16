using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

namespace Minh
{
    public class HeroViewer : MonoBehaviour
    {

        public void MoveHero(Transform position, Vector3 EndPosition, float Speed)
        {
            Tween.Position(position,EndPosition, Speed);
            //Animation Di Chuyen
        
        }

        public void HeroAttack(Transform position,Vector3 EndPosition, float attackRate)
        {
            //Ap dung tween tan cong va animation tan cong
            Tween.Position(position, EndPosition, attackRate, Ease.Default, 2,
                CycleMode.Yoyo);
        }
    }

}
