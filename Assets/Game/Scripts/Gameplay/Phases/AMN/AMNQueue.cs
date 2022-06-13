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
        [SerializeField] RibossomeLetter ribossomeHolder = default;

        [SerializeField] float howMuchY = -88;
        [SerializeField] float howMuchX;

        //private int actualAMNNumber = 0; //Correct one
        private int actualAMNNumber = 1;


        public async Task NewAMNInLine(bool lastOne, bool newRb, string amnNumber, string amnName){
            Task queueTask = PushNewAMN(transporterList.GetSimbolBall(),transporterList.GetSimbolColor(), amnName);
            
            actualAMNNumber++;
            actualColor = Util.CreateNewDifferentColor(actualColor);

            if(firstOne){
                queueTask = TurnVisibleAMNHolder(actualColor, amnName);
                firstOne = false;
            }         

            await transporterList.RibossomeExit(lastOne, actualAMNNumber + 1, queueTask, this.transform);

            if(newRb){
                await transporterList.RibossomeEnter(actualColor, amnNumber);
            } 
        }

        public async Task SetAllRibossomeEnter(int ribossomeMaxNumber){
            int i;

            transporterList.SetPool(ribossomeMaxNumber);

            for(i = 0; i < ribossomeMaxNumber - 1; i++){ //Set the max, but in the begginning only two will be spawned
                actualColor = Util.CreateNewDifferentColor(actualColor);
                await transporterList.RibossomeEnter(actualColor, (i+1).ToString());
            }

            transporterList.SetChildPrefab(amnPrefab.gameObject);

            await Task.Yield();
        }

        public async Task TurnVisibleAMNHolder(Color colorValue, string amnName){
            int rHChildCount = ribossomeHolder.transform.childCount - 1;
            float animationTime = transporterList.GetAnimationsTime();

            ribossomeHolder.SetRibossomeColor(colorValue);
            ribossomeHolder.transform.GetChild(rHChildCount).
                GetComponent<AMNLetter>().SetAMNColor(colorValue);
            
            ribossomeHolder.Setup(actualAMNNumber.ToString());

            ribossomeHolder.gameObject.SetActive(true);

            float xTarget = this.gameObject.transform.parent.GetComponent<RectTransform>().
                            anchoredPosition.x + howMuchX;
            float yTarget = this.gameObject.transform.parent.GetComponent<RectTransform>().
                            anchoredPosition.y + howMuchY;   

            SetVisibleGroupName(ribossomeHolder.transform.GetChild(ribossomeHolder.transform.childCount-1).
                                    GetComponent<AMNLetter>(), amnName, animationTime);
                        
            LeanTween.move(ribossomeHolder.transform.GetComponent<RectTransform>(), 
                new Vector3(xTarget, yTarget, 0), animationTime).
                setEase(transporterList.GetAnimationCurve());

            await Task.Delay(Util.ConvertToMili(animationTime));

            Transform ribossomeHolderBall = ribossomeHolder.transform.
                                            GetChild(rHChildCount); 
            ribossomeHolderBall.SetParent(this.transform); //Parenting the animation
        }

        public async Task PushNewAMN(Transform sinthetizing, Color simbolColor, string amnName){ //Name is close to the other on purpose
            float animationTime = transporterList.GetAnimationsTime();
            int rHChildCount = ribossomeHolder.transform.childCount - 1;

            RectTransform fatherPosition = this.transform.parent.GetComponent<RectTransform>();
            Vector3 saveInitial = fatherPosition.anchoredPosition;
            Transform newAMN = sinthetizing.GetChild(sinthetizing.childCount - 1);

            ribossomeHolder.SetRibossomeColor(simbolColor);
            ribossomeHolder.Setup(actualAMNNumber.ToString());
            
            SetVisibleGroupName(newAMN.GetComponent<AMNLetter>(), amnName, animationTime);

            LeanTween.move(fatherPosition.gameObject, 
                newAMN, animationTime).
                setEase(transporterList.GetAnimationCurve());
            
            await Task.Delay(Util.ConvertToMili(animationTime));    

            newAMN.SetParent(this.transform);

            Instantiate<Letter>(amnPrefab, sinthetizing).gameObject.SetActive(false);

            LeanTween.move(fatherPosition, 
                saveInitial, animationTime).
                setEase(transporterList.GetAnimationCurve());
            
            await Task.Delay(Util.ConvertToMili(animationTime));
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
