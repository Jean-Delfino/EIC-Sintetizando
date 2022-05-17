using System.Collections;
using System.Collections.Generic;
using System;

<<<<<<< Updated upstream
=======
using GameUserInterface.Animation;
>>>>>>> Stashed changes
using UnityEngine;

namespace PhasePart.RNA.DNA{

    /*
        Control all the animations of the Cell, self explanatory
    */

<<<<<<< Updated upstream
    public class CellAnimator : MonoBehaviour{

        Animator myAnimator;
        [SerializeField] GameObject nucleus;
        [SerializeField] GameObject notNuclues;

        //Action myAction;

        void Start(){
            myAnimator = gameObject.GetComponent<Animator>(); 
        }
        
        //Expand cell nucleus
=======
    public class CellAnimator : AnimatorUser{
        [SerializeField] GameObject nucleus;
        [SerializeField] GameObject notNuclues;
        
>>>>>>> Stashed changes
        public void ExpandCellNucleus(){
            NotNucleusChange();

            myAnimator.SetBool("Shrink", false);
            myAnimator.SetBool("Expand", true);
        }
<<<<<<< Updated upstream

        //Shrink cell nucleus
=======
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
        public void RNAEscapeNucleus(){
=======
        public void RNAEscapeNucleus(){ //The RNA escape from a hole in the DNA
>>>>>>> Stashed changes
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
