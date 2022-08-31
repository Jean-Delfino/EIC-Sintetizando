using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

using PhasePart.RNA.DNA;

namespace PhasePart.Destination{
    public class DestinationManager : PhaseManagerMono
    {
        [SerializeField] CellAnimator cellReference = default; //Used for the single purpose of animation
        
        void Start(){
            PlayAMNQueueTransformation();
        }

        private async void PlayAMNQueueTransformation(){
            float time = cellReference.AMNTransformation();

            await Task.Delay(Util.ConvertToMili(time));

            base.EndPhase();
        }

    }
}