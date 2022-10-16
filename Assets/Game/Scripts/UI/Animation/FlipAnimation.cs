using System.Collections.Generic;
using UnityEngine;

namespace GameUserInterface.Animation{
    public class FlipAnimation : AnimatorUser, SpriteAnimator
    {
        [SerializeField] int qtdSides = 2; //This basically allows a dice flip animation, in a child script
        Queue<SpriteRenderer> sideSpriteRenderers = new();

        private bool flipAnimationState = true; //True to not change in the first "seek"
        protected override void Awake()
        {
            base.Awake();

            int i;
            for(i = 0; i < qtdSides; i++){
                sideSpriteRenderers.Enqueue(transform.GetChild(i).GetComponent<SpriteRenderer>());
            }
        }

        public new void DoSpriteChangeAnimation(Sprite sprite){
            SpriteRenderer renderer = sideSpriteRenderers.Dequeue();
            renderer.sprite = sprite;

            sideSpriteRenderers.Enqueue(renderer);

            flipAnimationState = !flipAnimationState;
            SetAnimationState("Flip", flipAnimationState);
        }
    }
}
