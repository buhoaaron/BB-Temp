using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.MainScene
{
    public class GameRoom : MonoBehaviour
    {
        public Animator[] animators;

        private void Awake()
        {
            animators[0].SetTrigger("press");
        }

        public void SetPressed(Animator animator)
        {
            foreach (Animator anim in animators)
            {
                if (anim == animator)
                    continue;
                anim.SetTrigger("exit");
            }
            animator.SetTrigger("press");
        }
    }
}