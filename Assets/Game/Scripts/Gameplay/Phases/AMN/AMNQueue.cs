using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

using GameUserInterface.Text;

/*
    Manages the AMNQueue and all its animations
*/

namespace PhasePart.AMN{
    public class AMNQueue : MonoBehaviour{
        private Color actualColor = new Color(0f, 0f, 0f);
        [SerializeField] Letter amnPrefab = default;
        bool firstOne = true;

        [Space]
        [Header ("To use exclusivily in the Ribossome Animations")]
        [Space]

        [SerializeField] RibossomeAnimator transporterList = default;

        Transform toConnect = null;

        //private int actualAMNNumber = 0; //Correct one
        private int actualAMNNumber = 1;

        Func<int, Task> moveAction;

        public async Task NewAMNInLine(bool lastOnes, bool newRb, string amnNumber, string amnName){
            if(newRb){
               moveAction = async act => {
                    await PushNewAMN(transporterList.GetSinthetizingFromQueue(), amnName);
                }; 
            }else{
                moveAction = async act => {
                    await Task.Yield();
                };
            }

            actualAMNNumber++;
            actualColor = Util.CreateNewDifferentColor(actualColor);

            await transporterList.RibossomeExit(!lastOnes, actualColor, actualAMNNumber + 1, moveAction);
        }

        public async Task SetAllRibossomeEnter(int ribossomeMaxNumber){
            int i;

            transporterList.SetPool(ribossomeMaxNumber);

            for(i = 0; i < ribossomeMaxNumber - 1; i++){ //Set the max, but in the begginning only two will be spawned
                actualColor = Util.CreateNewDifferentColor(actualColor);
                await transporterList.RibossomeEnter(actualColor, (i+1).ToString(), false);
            }

            transporterList.SetChildPrefab(amnPrefab.gameObject);

            await Task.Yield();
        }

        public async Task PushNewAMN(Transform sinthetizing, string amnName){ //Name is close to the other on purpose
            float animationTime = transporterList.GetAnimationsTime();

            RectTransform fatherPosition = this.transform.parent.GetComponent<RectTransform>();
            Vector3 saveInitial = fatherPosition.anchoredPosition;
            Transform newAMN = sinthetizing.GetChild(sinthetizing.childCount - 1);
            
            SetVisibleGroupName(newAMN.GetComponent<AMNLetter>(), amnName, animationTime);
            
            newAMN.SetParent(this.transform);
            
            await Task.Delay(Util.ConvertToMili(animationTime));    
        }

        private void SetVisibleGroupName(AMNLetter amnLetter, string amnName, float time){
            CanvasGroup cG = amnLetter.GetAMNGroupName();
            amnLetter.SetupAMNName(amnName);            

            Util.ChangeAlphaCanvasImageAnimation(cG, 1f, time);
        }

        public void SetFirstOne(bool state){
            firstOne = state;
        }
    }
}
