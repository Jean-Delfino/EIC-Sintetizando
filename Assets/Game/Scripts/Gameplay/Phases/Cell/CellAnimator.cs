using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

namespace PhasePart.RNA.DNA{

    /*
        Control all the animations of the Cell, self explanatory
    */

    public class CellAnimator : MonoBehaviour{

        Animator myAnimator;
        [SerializeField] GameObject nucleus;
        [SerializeField] GameObject notNuclues;

        //Action myAction;

        void Start(){
            myAnimator = gameObject.GetComponent<Animator>(); 
        }
        
        //Expand cell nucleus
        public void ExpandCellNucleus(){
            NotNucleusChange();

            myAnimator.SetBool("Shrink", false);
            myAnimator.SetBool("Expand", true);
        }

        //Shrink cell nucleus
        public void ShrinkCellNucleus(){
            if(!notNuclues.activeSelf) NotNucleusChange();

            myAnimator.SetBool("Expand", false);
            myAnimator.SetBool("Shrink", true);
        }

        public void Revert(){ //Not used, but could be
            myAnimator.SetBool("Shrink", false);
            myAnimator.SetBool("Expand", false);
            myAnimator.SetBool("Revert", true);
        }

        public void RNAEscapeNucleus(){
            if(!notNuclues.activeSelf) NotNucleusChange();

            myAnimator.SetBool("RNAEscape", true);
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
