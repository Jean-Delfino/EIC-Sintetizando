using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

using System.Threading;
using System.Threading.Tasks;

namespace GameUserInterface.Animation{
    public class TransitionController : MonoBehaviour{

        public enum TransitionType{
            OverTheScreen,
            CloseOfCurtains
        }

        [System.Serializable]
        private class Transition{
            public VideoClip animationIn = default;
            public VideoClip animationOut = default;
            public float animationTime = default;
        }

        [SerializeField] List<Transition> videoClips = default;
        [SerializeField] GameObject painelClickBlock = default;

        //[SerializeField] Animator myAnimator = default;
        [SerializeField] RectTransform transitionScreen = default;
        [SerializeField] VideoPlayer videoPlayer = default;

        [SerializeField] float animationTime = default;

        //This object do not await, the other object will await
        //Black to invisible
        public int PlayTransitionFadeIn(){
            Util.ChangeAlphaImageAnimation(transitionScreen, 0f, animationTime);
            return Util.ConvertToMili(animationTime);
        }

        public void DisableTransition(){
            transitionScreen.gameObject.SetActive(false);
        }

        public void EnableTransition(){
            transitionScreen.gameObject.SetActive(true);
        }

        /*
            print(type);
            print((int) type);
        */
        public int PlayTransitionIn(TransitionType type){
            painelClickBlock.SetActive(true);
            videoPlayer.clip = videoClips[(int) type].animationIn;
            videoPlayer.Play();
            return Util.ConvertToMili(videoClips[(int) type].animationTime);
        }

        public int PlayTransitionOut(TransitionType type){
            painelClickBlock.SetActive(false);
            videoPlayer.clip = videoClips[(int) type].animationOut;
            videoPlayer.Play();
            return Util.ConvertToMili(videoClips[(int) type].animationTime);
        }

        public void RemoveTransitionComplete(){
            videoPlayer.clip = null;
        }

    }
}
