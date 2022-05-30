using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

using System;
using System.Threading;
using System.Threading.Tasks;

using GameUserInterface.Animation;

/*
    This code pick one VideoChoice and set a videoPlayer, its also set some video options
    Like pause, play and skip.
    The video is linked with a subtitle
*/
namespace ProteinPart{
    public class VideoChoice : MonoBehaviour{
        [SerializeField] List<VideoClip> videoClips = new List<VideoClip>();
        [SerializeField] Transform screens;

        [SerializeField] TransitionController myTransition = default;

        private VideoPlayer videoPlayer;
        private int actualVideoClip;

        //private Task videoTask;
        bool pause;

        private void Start(){
            Protein.Setup(this);
            videoPlayer = GetComponent<VideoPlayer>();
            /*
            Action<object> action = (object obj) =>
                                    {
                                        FinishCheck();
                                    };

            videoTask = new Task(action, null);*/
        }

        public void ChooseProtein(int index){
            videoPlayer.clip = videoClips[index];
            actualVideoClip = index;
            this.transform.GetChild(0).gameObject.SetActive(true); //The buttons
            PlayVideo();
        }

        private IEnumerator FinishCheck(){
            while(videoPlayer.isPlaying){
                yield return null;
            }

            if(!pause){
                ShowScreen();
            }
        }

        private async void ShowScreen(){
            print("Entrou aqui Show Screen");
            this.gameObject.SetActive(false); //Instanteneous stop all coroutine

            await PlayTransitionIn(); //Don't get finished in SetActive(false)
        }

        private async Task PlayTransitionIn(){
            myTransition.EnableTransition();

            screens.GetChild(actualVideoClip).gameObject.SetActive(true);
            await Task.Delay(myTransition.PlayTransitionFadeIn());
            
            myTransition.DisableTransition(); //Just to be sure, not needed
        }   

        public void StopVideo(){
            if(!videoPlayer.isPlaying) return;
            pause = true;

            StopCoroutine(FinishCheck());
            videoPlayer.Pause();
        }

        public void PlayVideo(){
            if(videoPlayer.isPlaying) return;
            pause = false;

            videoPlayer.Play();
            StartCoroutine(FinishCheck());
            //videoTask.Start();
        }

        public void SkipVideo(){
            ShowScreen();
        }
    }
}
