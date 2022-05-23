using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

using System;
using System.Threading;
using System.Threading.Tasks;

/*
    This code pick one VideoChoice and set a videoPlayer, its also set some video options
    Like pause, play and skip.
    The video is linked with a subtitle
*/
namespace ProteinPart{
    public class VideoChoice : MonoBehaviour{
        [SerializeField] List<VideoClip> videoClips = new List<VideoClip>();
        [SerializeField] Transform screens;

        [SerializeField] GameObject transition; //for now it should be enough

        private float animationsTime = 1f;

        private VideoPlayer videoPlayer;
        private int actualVideoClip;

        //public RawImage rawImage;
        //private Task videoTask;
    

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
            //rawImage.texture = videoPlayer.texture;//----------
            PlayVideo();
        }

        private IEnumerator FinishCheck(){
            print("Entrou aqui");
            while(videoPlayer.isPlaying){
                yield return null;
            }

            ShowScreen();
        }

        private async void ShowScreen(){
            this.gameObject.SetActive(false); //Instanteneous stop all coroutine

            await PlayTransitionIn();
        }

        private async Task PlayTransitionIn(){
            transition.SetActive(true);
            
            //Black to invisible
            Util.ChangeAlphaImageAnimation(transition.GetComponent<RectTransform>(), 0f, animationsTime);

            screens.GetChild(actualVideoClip).gameObject.SetActive(true);

            await Task.Delay(Util.ConvertToMili(animationsTime));
            transition.SetActive(false); //Just to be sure, not needed
        }   

        public void StopVideo(){
            if(!videoPlayer.isPlaying) return;

            //videoTask.Wait();
            StopCoroutine(FinishCheck());
            videoPlayer.Pause();
        }

        public void PlayVideo(){
            if(videoPlayer.isPlaying) return;

            videoPlayer.Play();
            StartCoroutine(FinishCheck());
            //videoTask.Start();
        }

        public void SkipVideo(){
            ShowScreen();
        }
    }
}
