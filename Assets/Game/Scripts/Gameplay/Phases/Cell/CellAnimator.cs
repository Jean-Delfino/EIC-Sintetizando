using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

using GameUserInterface.Animation;

namespace PhasePart.RNA.DNA{

    /*
        Control all the animations of the Cell, self explanatory
    */

    public class CellAnimator : AnimatorUser{
        [SerializeField] GameObject nucleus;
        [SerializeField] GameObject notNuclues;

        //Action myAction;
        private float animationTime = 1f;

        void Start(){
            myAnimator = gameObject.GetComponent<Animator>(); 
        }
        
        //Expand cell nucleus
        public float ExpandCellNucleus(){
            NotNucleusChange();

            myAnimator.SetBool("Shrink", false);
            myAnimator.SetBool("Expand", true);

            return animationTime;
        }

        public float SeparateDNA(){
            myAnimator.SetBool("Separate", true);

            return animationTime;
        }

        //Shrink cell nucleus
        public float ShrinkCellNucleus(){
            if(!notNuclues.activeSelf) NotNucleusChange();

            myAnimator.SetBool("Expand", false);
            myAnimator.SetBool("Shrink", true);
            
            return animationTime;
        }

        public float Revert(){ //Not used, but could be
            myAnimator.SetBool("Shrink", false);
            myAnimator.SetBool("Expand", false);
            myAnimator.SetBool("Revert", true);
            
            return animationTime;
        }

        public float RNAEscapeNucleus(){
            if(!notNuclues.activeSelf) NotNucleusChange();

            myAnimator.SetBool("RNAEscape", true);
            return animationTime / 0.8f; //Actual speed of this animation
        }

        public void NotNucleusChange(){
            int valueScale = Convert.ToInt32(!notNuclues.activeSelf);

            Util.ChangeAlphaCanvasImageAnimation(notNuclues.
                GetComponent<CanvasGroup>(),valueScale,animationTime);
            
            notNuclues.SetActive(!notNuclues.activeSelf);
        }
    }
}

/*
        async Task PlayAnimationUntilEnd(Animator anim, string nameFunction){
            float actualLoop = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

            myAnimator.Play(nameFunction);
            print(actualLoop + " BANANA");

            while(AnimatorIsPlaying(myAnimator, nameFunction)){
                print(anim.GetCurrentAnimatorStateInfo(0).normalizedTime + " aa");
                
                await Task.Yield();
            }

            print("SAIU");
        }

        bool AnimatorIsPlaying(Animator anim, string stateName){
        return AnimatorIsPlaying(anim) && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        //edu4hd0
        bool AnimatorIsPlaying(Animator anim){
        return anim.GetCurrentAnimatorStateInfo(0).length >
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
*/
