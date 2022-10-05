using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

using PhasePart.RNA.DNA;

namespace PhasePart.Destination{
    public class DestinationManager : PhaseManagerMono
    {
        [Space] [Header("Destination Manager Variables")] [Space]
        [SerializeField] CellAnimator cellReference = default; //Used for the single purpose of animation
        
        [SerializeField] GameObject destinationDescriptionObject = default; 
        [SerializeField] Button endDestinationButton = default;

        void Start(){
            cellReference.SetAnimatorStatus(true);
            PlayAMNQueueTransformation();
        }

        private async void PlayAMNQueueTransformation(){
            float time = cellReference.AMNTransformation();

            await Task.Delay(Util.ConvertToMili(time/ 0.5f));

            endDestinationButton.onClick.AddListener(delegate {destinationDescriptionObject.SetActive(false);});
            endDestinationButton.onClick.AddListener(delegate {EndPhase();});
        }

        public new void EndPhase()
        {
            base.EndPhase();
        }

    }
}