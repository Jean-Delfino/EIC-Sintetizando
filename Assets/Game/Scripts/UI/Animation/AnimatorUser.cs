using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUserInterface.Animation{
    public class AnimatorUser : MonoBehaviour
    {
        // Start is called before the first frame update
        protected Animator myAnimator;
        void Start(){
            myAnimator = gameObject.GetComponent<Animator>(); 
        }
    }
}
