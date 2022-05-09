using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using Animation.Basic;

namespace PhasePart.RNA.DNA{
    public class CellAnimator : MonoBehaviour, AnimationController{

        Animator myAnimator;
        [SerializeField] GameObject nucleus;
        [SerializeField] GameObject notNuclues;

        Action myAction;

        void Start(){
            myAnimator = gameObject.GetComponent<Animator>(); 
        }
        
        //Expand cell nucleus
        public void ExpandCellNucleus(){
            myAction = NotNucleusChange;
            myAnimator.Play("ExpandNucleus");
            //await PlayAnimationUntilEnd(myAnimator, "ExpandNucleus");
        }

        //Shrink cell nucleus
        public void ShrinkCellNucleus(){
            //await PlayAnimationUntilEnd(myAnimator, "ShrinkNucleus");
            myAnimator.Play("ShrinkNucleus");
            myAction = NotNucleusChange;
        }

        public void AnimationColector(){
            myAction();
        }

        public void NotNucleusChange(){
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
