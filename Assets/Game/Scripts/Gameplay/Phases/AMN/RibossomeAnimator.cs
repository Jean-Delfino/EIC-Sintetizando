using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EditorUtility;

using GameUserInterface.Text;
using GameUserInterface.Animation;


//http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
//https://learn.unity.com/tutorial/waypoints?uv=2019.4&projectId=5e0b6dd4edbc2a00200e3641#
//https://www.raywenderlich.com/27209746-tweening-animations-in-unity-with-leantween


//https://stackoverflow.com/questions/14015319/where-do-i-mark-a-lambda-expression-async
//https://docs.microsoft.com/pt-br/dotnet/csharp/language-reference/operators/lambda-expressions
namespace PhasePart.AMN{
    /*
        Control all the animations of the Ribossome, self explanatory

        Use pooling
        
        
        AMNQueue values
        
        //[SerializeField] Transform amnQueue = default;
        //[SerializeField] GameObject amnPrefab = default;

    */
    public class RibossomeAnimator : AnimatorUser{

        [Space]
        [Header("Animation-Transform Atributes")]
        [Space]

        [SerializeField] RibossomeQueueAnimator ribossomeQueue = default;

        [SerializeField] Transform holdRibossome = default; //Used on enter

        [SerializeField] AnimationCurve animationCurve = default;
        [SerializeField] float animationsTime = default; 


        [Space]
        [Header("Ribossome Atributes")]
        [Space]

        [SerializeField] GameObject ribossomePrefab = default;

        private Color saveColor;

        //Pooling
        private Queue<GameObject> pool = new Queue<GameObject>();
        [SerializeField] RectTransform rectTransformValuesReference;
        
        private GameObject childPrefab;
        
        //Only use for tests, nothing more, don't delete
        /*
        //[SerializeField] Transform amnQueue = default;
        //[SerializeField] GameObject amnPrefab = default;

        private void Start() {
            SetPool(3);
            Tests();
        }

        private async void Tests(){
            Color firstColor = Util.RandomSolidColor();
            Func<int, Task> actionOnItem = async item => {
                await amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSinthetizingFromQueue(), "Gly");
            };

            Func<int, Task> nullAMN = async item => {
                await Task.Yield();
            };

            childPrefab = amnPrefab;

            await RibossomeEnter(firstColor, "1", false);
            
            await RibossomeExit(true, Util.RandomSolidColor(), 2, actionOnItem);
            await RibossomeExit(true, Util.RandomSolidColor(), 3, actionOnItem);
            
            await RibossomeExit(true, Util.RandomSolidColor(), 4, actionOnItem);
            await RibossomeExit(true, Util.RandomSolidColor(), 5, actionOnItem);
            await RibossomeExit(true, Util.RandomSolidColor(), 6, actionOnItem);
            await RibossomeExit(true, Util.RandomSolidColor(), 7, actionOnItem);

            await RibossomeExit(false, Util.RandomSolidColor(), 8, actionOnItem); 
            
            await RibossomeExit(false, Util.RandomSolidColor(), 9, nullAMN);
            await RibossomeExit(false, Util.RandomSolidColor(), 10, nullAMN);
            
        }*/

        //Second part of pooling, the reset of a pooled object
        private void PoolObjectReset(Transform newElement, GameObject childZero){
            Util.CopyRectTransform(newElement.GetComponent<RectTransform>(),
                rectTransformValuesReference);
            
            GameObject child = Instantiate<GameObject>(childZero, newElement);
            child.transform.SetAsLastSibling();
            newElement.gameObject.SetActive(false);
        }

        public void SetPool(int poolCapacity){
            int i;
            GameObject hold;

            for(i = 0; i < poolCapacity; i++){
                hold = Instantiate<GameObject>(ribossomePrefab, holdRibossome);
                hold.SetActive(false);
                pool.Enqueue(hold);
            }
        }

        //Every object in the queue has a AMN (except the last one)
        //So we need to animate this too
        //Waste destination is the AMN queue
        public async Task RibossomeExit(bool newAMN, 
            Color newRibossomeColor, int numberAMN, 
            Func<int,Task> externalActions){

            await RibossomeExit(externalActions, ribossomeQueue.GetRibossomeSinthetizing());

            if(newAMN){
                await RibossomeEnter(newRibossomeColor, numberAMN.ToString(), false);
            }

            await Task.Yield();
        }

        private async Task RibossomeExit(Func<int,Task> externalActions, RibossomeLetter rlSint){
            Task[] taskAnimation = new Task[2];

            taskAnimation[0] = ribossomeQueue.MoveAllRibossome(new Vector3(0, 0, 0), animationsTime, animationCurve);

            saveColor = rlSint.GetRibossomeColor();
            rlSint.SetAMNPresence(false);

            taskAnimation[1] = externalActions(0);

            await Task.WhenAll(taskAnimation);
        }

        public async Task RibossomeEnter(Color newRibossomeColor, string numberAMN, bool move){
            GameObject hold = pool.Dequeue();
            RibossomeLetter rl = hold.GetComponent<RibossomeLetter>();
            AMNLetter moveObjectAmn;

            pool.Enqueue(hold);

            RibossomeSetup(rl, newRibossomeColor, numberAMN);
            
            if(!rl.GetAMNPresence()){
                PoolObjectReset(hold.transform, childPrefab);
                rl.SetAMNPresence();
            }

            moveObjectAmn = hold.transform.
                GetChild(hold.transform.childCount - 1).GetComponent<AMNLetter>();

            moveObjectAmn.SetAMNColor(newRibossomeColor);
            moveObjectAmn.Setup(numberAMN);

            hold.SetActive(true);
            
            await ribossomeQueue.MoveNewRibossome(move, rl, 
                new Vector3(0, 0, 0), 2.5f, animationsTime, animationCurve);
            await Task.Delay(500);
        }

        private void RibossomeSetup(RibossomeLetter rl, Color newRibossomeColor, string numberAMN){
            rl.SetRibossomeColor(newRibossomeColor);
            rl.Setup(numberAMN);
            rl.SetStateRib(0);
        }

        public void SetChildPrefab(GameObject childPrefab){
            this.childPrefab = childPrefab;
        }

        public float GetAnimationsTime(){
            return this.animationsTime;
        }

        public AnimationCurve GetAnimationCurve(){
            return this.animationCurve;
        }

        public Transform GetSinthetizingFromQueue(){
            return ribossomeQueue.GetRibossomeSinthetizing().transform;
        }

        public Transform GetAMNBallFromQueue(){
            return ribossomeQueue.GetBallRibossomeSinthetizing();
        }

        public Color GetColorOfSinthetizingRibossome(){
            return ribossomeQueue.GetRibossomeSinthetizing().GetRibossomeColor();
        }
    }
}
