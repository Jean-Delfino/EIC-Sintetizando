using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

using System.Threading;
using System.Threading.Tasks;

namespace GameUserInterface.Animation{
    //This object do not await, the other object will await
    
    public enum TransitionType{
        Falling,
        CloseOfCurtains
    }


    public class TransitionController : AnimatorUser{
        [System.Serializable]
        private class Transition{
            public float animationTimeIn = default;
            public float animationTimeOut = default;
        }

        float betweenTransition = 0.05f;

        [SerializeField] List<Transition> videos = default;
        [SerializeField] GameObject painelClickBlock = default;

        //[SerializeField] Animator myAnimator = default;
        [SerializeField] RectTransform transitionScreen = default;
        [SerializeField] float animationTime = default;

        //Black to invisible
        public int PlayTransitionFadeIn(){
            Util.ChangeAlphaImageAnimation(transitionScreen, 0f, animationTime);
            return Util.ConvertToMili(animationTime);
        }

        public int PlayTransitionFadeOut(){ //Not used yet
            Util.ChangeAlphaImageAnimation(transitionScreen, 1f, animationTime);
            return Util.ConvertToMili(animationTime);
        }


        public void DisableTransition(){
            //myAnimator.SetBool("Expand", true);

            transitionScreen.gameObject.SetActive(false);
        }

        public void EnableTransition(){
            transitionScreen.gameObject.SetActive(true);
        }

        /*
            print(type);
            print((int) type);
        */
        public float PlayTransitionIn(TransitionType type){
            myAnimator.SetBool(type.ToString(),true);
            painelClickBlock.SetActive(true);

            return videos[(int) type].animationTimeIn;
        }

        public float PlayTransitionOut(TransitionType type){
            myAnimator.SetBool(type.ToString(), false);
            painelClickBlock.SetActive(false);

            return videos[(int) type].animationTimeOut;
        }

        public float GetBetweenTransition(){
            return this.betweenTransition;
        }

    }
}
