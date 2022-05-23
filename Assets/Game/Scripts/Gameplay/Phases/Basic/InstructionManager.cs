using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    
namespace PhasePart{
    /*
        Fade with Canvas Group
        https://forum.unity.com/threads/how-can-i-fade-in-out-a-canvas-group-alpha-color-with-duration-time.969864/
        Using almost all the code of Zer0Cool

        //Block the flow of the game while the instruction is on
    */
    public class InstructionManager : MonoBehaviour{

        [Space]
        [Header("Instruction Objects")]
        [Space]
        [SerializeField] Button instructionReminder; //Show again the instruction

        [Space]
        [Header("Fading Variables")]
        [Space]
        [SerializeField] AnimationCurve animationCurve;
        [SerializeField] float fadingSpeed = 1f;

        public enum Direction {FadeIn, FadeOut};

        
        //public delegate void ButtomAnimation(RectTransform rt, Vector3 pos, float time);
        public delegate void ButtomClick();

        public void SetInstructionReminder(ButtomClick bt){
            instructionReminder.onClick.AddListener(delegate{bt(); }); 
        }

        public void ButtonChangeOfScaleAnimation(Vector3 scale, float time){
            Util.ChangeScaleAnimation(instructionReminder.GetComponent<RectTransform>(), 
                    scale, time);
        }

        public float FadeInAnimationInstruction(){ //Same thing, but readable
            if(this.gameObject.activeSelf) return 0f;
            this.gameObject.SetActive(true);

            StartCoroutine(FadeCanvas(this.transform.GetComponent<CanvasGroup>(), 
                Direction.FadeIn, fadingSpeed));
            return fadingSpeed;
        }
        public float FadeOutAnimationInstruction(){
            if(!this.gameObject.activeSelf) return 0f;
            
            StartCoroutine(FadeCanvas(this.transform.GetComponent<CanvasGroup>(), 
                Direction.FadeOut, fadingSpeed));
            return fadingSpeed;
        }
        private IEnumerator FadeCanvas(CanvasGroup canvasGroup, Direction direction, float duration){
        // keep track of when the fading started, when it should finish, and how long it has been running
            var startTime = Time.time;
            var endTime = Time.time + duration;
            var elapsedTime = 0f;
    
            // set the canvas to the start alpha – this ensures that the canvas is ‘reset’ if you fade it multiple times
            if (direction == Direction.FadeIn) canvasGroup.alpha = animationCurve.Evaluate(0f);
            else canvasGroup.alpha = animationCurve.Evaluate(1f);
    
            // loop repeatedly until the previously calculated end time
            while (Time.time <= endTime)
            {
                elapsedTime = Time.time - startTime; // update the elapsed time
                var percentage = 1 / (duration / elapsedTime); // calculate how far along the timeline we are
                if ((direction == Direction.FadeOut)) // if we are fading out
                {
                    canvasGroup.alpha = animationCurve.Evaluate(1f - percentage);
                }
                else // if we are fading in/up
                {
                    canvasGroup.alpha = animationCurve.Evaluate(percentage);
                }
    
                yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
            }
    
            // force the alpha to the end alpha before finishing – this is here to mitigate any rounding errors, e.g. leaving the alpha at 0.01 instead of 0
            if (direction == Direction.FadeIn) canvasGroup.alpha = animationCurve.Evaluate(1f);
            else canvasGroup.alpha = animationCurve.Evaluate(0f);
        }

        public float GetFadeTime(){
            return fadingSpeed;
        }
    }
}
