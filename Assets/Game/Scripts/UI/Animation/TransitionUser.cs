using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading;
using System.Threading.Tasks;

/*
    This make the SelectionManager shorter, so it's a good idea
*/

namespace GameUserInterface.Animation{
    public class TransitionUser : MonoBehaviour{
        [Space]
        [Header("   Transition User Variables")]
        [Space]

        [SerializeField] TransitionController tC = default;
        [SerializeField] TransitionType transition = default;

        public int PlayTransitionIn(){
            return Util.ConvertToMili(tC.PlayTransitionIn(transition));
        }

        public int PlayTransitionOut(){
            return Util.ConvertToMili(tC.PlayTransitionOut(transition));
        }

        public int GetBetweenTransitionTime(){
            return Util.ConvertToMili(tC.GetBetweenTransition());
        }
    }
}
