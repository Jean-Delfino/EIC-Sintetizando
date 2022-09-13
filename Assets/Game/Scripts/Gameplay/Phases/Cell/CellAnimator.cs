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
            return animationTime / 0.5f; //Actual speed of this animation
        }

        public float AMNTransformation(){
            myAnimator.SetBool("AMNTransformation", true);
            return animationTime;
        }

        public void NotNucleusChange(){
            int valueScale = Convert.ToInt32(!notNuclues.activeSelf);

            Util.ChangeAlphaCanvasImageAnimation(notNuclues.
                GetComponent<CanvasGroup>(),valueScale,animationTime);
            
            notNuclues.SetActive(!notNuclues.activeSelf);
        }

        public void SetAnimatorStatus(bool state)
        {
            myAnimator.enabled = state;
        }
    }
}
