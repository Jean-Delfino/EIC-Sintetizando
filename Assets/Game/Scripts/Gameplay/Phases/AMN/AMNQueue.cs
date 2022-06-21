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

        //private int actualAMNNumber = 0; //Correct one
        private int actualAMNNumber = 1;


        public async Task NewAMNInLine(bool lastOne, bool newRb, string amnNumber, string amnName){
            Task queueTask = PushNewAMN(transporterList.GetSinthetizingFromQueue(), amnName);

            actualAMNNumber++;
            actualColor = Util.CreateNewDifferentColor(actualColor);

            await transporterList.RibossomeExit(!lastOne, actualColor, actualAMNNumber + 1, queueTask);

            /*
            if(newRb){
                await transporterList.RibossomeEnter(actualColor, amnNumber);
            } */
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
            Color amnColor = transporterList.GetColorOfSinthetizingRibossome();

            RectTransform fatherPosition = this.transform.parent.GetComponent<RectTransform>();
            Vector3 saveInitial = fatherPosition.anchoredPosition;
            Transform newAMN = sinthetizing.GetChild(sinthetizing.childCount - 1);
            
            SetVisibleGroupName(newAMN.GetComponent<AMNLetter>(), amnName, animationTime);
            
            newAMN.SetParent(this.transform);
            // LeanTween.move(newAMN.GetComponent<RectTransform>(), 
            //     saveInitial, animationTime).
            //     setEase(transporterList.GetAnimationCurve());
            
            await Task.Delay(Util.ConvertToMili(animationTime));    
            /*
            Instantiate<Letter>(amnPrefab, sinthetizing).gameObject.SetActive(false);
            
            LeanTween.move(fatherPosition, 
                saveInitial, animationTime).
                setEase(transporterList.GetAnimationCurve());
            
            await Task.Delay(Util.ConvertToMili(animationTime));
            */
        }

        private void SetVisibleGroupName(AMNLetter amnLetter, string amnName, float time){
            CanvasGroup cG = amnLetter.GetAMNGroupName();
            amnLetter.SetupAMNName(amnName);            

            Util.ChangeAlphaCanvasImageAnimation(cG, 1f, time);
        }

        public void PushAMN(string amnName){ //Old, not needed anymore, see RibossomeExit
            Letter hold;

            hold = Instantiate<Letter>(amnPrefab, this.transform);
            hold.gameObject.GetComponent<Image>().color = actualColor;
            hold.Setup(amnName);
        }

        public void SetFirstOne(bool state){
            firstOne = state;
        }
    }
}
