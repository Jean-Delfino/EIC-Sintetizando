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

namespace PhasePart.AMN{
    /*
        Control all the animations of the Ribossome, self explanatory

        Use pooling
        
        
        AMNQueue values
        
        //[SerializeField] Transform amnQueue = default;
        //[SerializeField] GameObject amnPrefab = default;

        Simbol is the actual sinthetizing AMN
    */
    public class RibossomeAnimator : AnimatorUser{

        [Space]
        [Header("Animation-Transform Atributes")]
        [Space]

        [SerializeField] Transform queue = default; //Queue of ribossome
        [SerializeField] Transform endTarget = default; //Used on exit
        [SerializeField] Transform holdRibossome = default; //Used on enter

        [SerializeField] AnimationCurve animationCurve = default;

        [SerializeField] float animationsTime = default; 
        [SerializeField] float fadeTime = default;
        [SerializeField] float fadeColorTime = default; 


        [Space]
        [Header("Ribossome Atributes")]
        [Space]

        [SerializeField] GameObject ribossomePrefab = default;
        [SerializeField] RectTransform simbolRibossome = default;

        [Space]
        //Pooling
        private Queue<GameObject> pool = new Queue<GameObject>();
        [SerializeField] RectTransform rectTransformValuesReference;

        private Transform firtsOne = null;
        [SerializeField] float howMuchSpace = -43;
        [SerializeField] float howMuchToMove = -120;

        
        [SerializeField] Transform amnQueue = default;
        [SerializeField] GameObject amnPrefab = default;

        private GameObject childPrefab;

        [Space]
        [Header("Atributes used to animation dif")]
        [Space]

        private bool noRBSintetized = true;

        
        private void Start() {
            SetPool(3);
            Tests();
        }

        private async void Tests(){
            Color firstColor = Util.RandomSolidColor();
            //Task notFirst = ;
            childPrefab = amnPrefab;

            await RibossomeEnter(firstColor,"1");
            await RibossomeEnter(Util.RandomSolidColor(),"2");

            await RibossomeExit(false, 2, amnQueue.GetComponent<AMNQueue>().
                TurnVisibleAMNHolder(firstColor, "AMN"), amnQueue);
            await RibossomeEnter(Util.RandomSolidColor(),"3");

            await RibossomeExit(false, 3, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "MNA"), amnQueue);
            await RibossomeEnter(Util.RandomSolidColor(),"4");

            await RibossomeExit(false, 4, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "NMA"), amnQueue);
            await RibossomeEnter(Util.RandomSolidColor(),"5");

            await RibossomeExit(false, 5, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "Gly"), amnQueue);
            await RibossomeEnter(Util.RandomSolidColor(),"6");

            await RibossomeExit(false, 6, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "AAA"), amnQueue);
            await RibossomeEnter(Util.RandomSolidColor(),"7");

            await RibossomeExit(false, 7, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "BBB"), amnQueue);
            await RibossomeExit(false, 8, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "CCC"), amnQueue);

            print("TEST -- ANIMATION ENDING");

            await RibossomeExit(true, 9, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "Fim"), amnQueue);
        }

        //Second part of pooling, the reset of a pooled object
        private void PoolObjectReset(Transform newElement, GameObject childZero){
            pool.Enqueue(newElement.gameObject);

            Util.CopyRectTransform(newElement.GetComponent<RectTransform>(),
                rectTransformValuesReference);
            
            GameObject child = Instantiate<GameObject>(childZero, newElement);
            child.transform.SetAsLastSibling();
            newElement.gameObject.SetActive(false);
        }

        public void SetPool(int poolCapacity){
            int i;
            GameObject hold;

            //hold = Instantiate<GameObject>(ribossomePrefab, queue);

            for(i = 0; i < poolCapacity; i++){
                hold = Instantiate<GameObject>(ribossomePrefab, holdRibossome);
                hold.SetActive(false);
                pool.Enqueue(hold);
            }
        }

        //Self explanatory
        private void ChangeColorSimbolRibossome(Color newColor){
            AMNLetter moveObjectAmn = simbolRibossome.GetChild(1).GetComponent<AMNLetter>();

            LeanTween.color(simbolRibossome.GetChild(0).GetComponent<RectTransform>(), newColor, fadeColorTime);
            LeanTween.color(moveObjectAmn.ReturnAMNLetterImage().transform.GetComponent<RectTransform>(), newColor, fadeColorTime);
        }   

        private void ChangeColorSimbolRibossome(Color newColor, Transform child){
            AMNLetter moveObjectAmn = simbolRibossome.GetChild(1).GetComponent<AMNLetter>();

            LeanTween.color(simbolRibossome.GetChild(0).GetComponent<RectTransform>(), newColor, fadeColorTime);
            LeanTween.color(child.GetComponent<RectTransform>(), newColor, fadeColorTime);
        }   

        //Sets the actual AMN so we can spawn a new one
        private void AMNRefSetup(Transform queue, GameObject childPrefab, string amnName){
            AMNLetter before = simbolRibossome.GetChild(1).GetComponent<AMNLetter>();
            before.SetupAMNName(amnName);
            
            before.transform.SetParent(queue); //Parenting the animation

            Util.ChangeAlphaCanvasImageAnimation(before.GetAMNGroupName(), 1f, fadeTime);
        }

        private void AMNRefSetup(Transform queue, GameObject childPrefab){
            AMNLetter before = simbolRibossome.GetChild(1).GetComponent<AMNLetter>();
            
            before.transform.SetParent(queue); //Parenting the animation

            Util.ChangeAlphaCanvasImageAnimation(before.GetAMNGroupName(), 1f, fadeTime);
        }

        private async Task RibossomeQueueMove(){
            RectTransform rT = queue.parent.GetComponent<RectTransform>();

            float moveDistance = rT.anchoredPosition.x + howMuchToMove;

            LeanTween.moveX(rT, moveDistance, 0.5f).setEase(animationCurve);
            
            await Task.Delay(Util.ConvertToMili(0.5f));
        }

        private async Task WasteDestinationMove(Transform wasteDestination){
            LeanTween.moveY(wasteDestination.GetComponent<RectTransform>(), 
                wasteDestination.position.y + howMuchSpace, 0.5f).setEase(animationCurve);
            
            await Task.Delay(Util.ConvertToMili(0.6f));

            AMNRefSetup(wasteDestination, childPrefab); //Set the ball used in the scene

            //await Task.Delay(Util.ConvertToMili(3f)); Testing

            LeanTween.moveY(wasteDestination.GetComponent<RectTransform>(), 
                wasteDestination.position.y - howMuchSpace, 0.6f).setEase(animationCurve);

            await Task.Delay(Util.ConvertToMili(0.8f));
        }

        private async Task ChangeSimbol(GameObject newChildPrefab){
            if(queue.childCount < 3) return; //There is no one sinthetizing

            Transform currentlySynthesizing = queue.GetChild(1); //Save time 

            //Gets the amn in the ribossome
            string refAMNnumeration = currentlySynthesizing.GetChild(currentlySynthesizing.childCount - 1)
                .GetComponent<AMNLetter>().GetValue();

            AMNLetter hold = Instantiate<AMNLetter>(newChildPrefab.GetComponent<AMNLetter>(), simbolRibossome);
            hold.Setup(refAMNnumeration);

            ChangeColorSimbolRibossome(currentlySynthesizing.transform.GetChild(0).GetComponent<Image>().color, 
                hold.ReturnAMNLetterImage().transform);

            await Task.Delay(Util.ConvertToMili(fadeColorTime));
        }

        private async Task ChangeSimbol(int numberRb){
            if(queue.childCount < 3) return; //There is no one sinthetizing

            Transform currentlySynthesizing = queue.GetChild(1); //Save time 

            //Gets the amn in the ribossome
            AMNLetter hold = simbolRibossome.GetChild(simbolRibossome.childCount - 1).GetComponent<AMNLetter>();
            CanvasGroup cG = hold.transform.GetComponent<CanvasGroup>();
            hold.Setup(numberRb.ToString());
            
            Util.ChangeAlphaCanvasImageAnimation(cG, 0f, 0f); //The ball setted as invisible by the AMNQueue
            hold.gameObject.SetActive(true);
            Util.ChangeAlphaCanvasImageAnimation(cG, 1f, fadeColorTime);

            ChangeColorSimbolRibossome(currentlySynthesizing.transform.GetChild(0).GetComponent<Image>().color, 
                hold.ReturnAMNLetterImage().transform);

            simbolRibossome.GetComponent<RibossomeLetter>().Setup(numberRb.ToString());

            await Task.Delay(Util.ConvertToMili(fadeColorTime));
        }

        private async Task EndSimbolRibossome(){
            float time = Util.ChangeAlphaCanvasImageAnimation(simbolRibossome.GetComponent<CanvasGroup>(), 0f, animationsTime);
            await Task.Delay(Util.ConvertToMili(time));
            simbolRibossome.gameObject.SetActive(false);
        }

        //Every object in the queue has a AMN (except the last one)
        //So we need to animate this too
        //Waste destination is the AMN queue
        public async Task RibossomeExit(bool lastOne, int numberRb, Task externalActions, Transform wasteDestination){
            Transform moveObject = queue.GetChild(0); //Takes out the first ribossome of the list
            Transform lastChild = queue.GetChild(queue.childCount-1);

            Transform moveObjectAmn = null; //Take the AMNLetter

            Task[] taskAnimation = new Task[2]; //All tasks of the object

            if(noRBSintetized){ //This is basically if start
                moveObjectAmn = moveObject.GetChild(moveObject.childCount - 1); //Take the AMNLetter
                moveObjectAmn.SetParent(holdRibossome); //Deparenting it from moveObject
                
                await MoveTowardsEnter(moveObjectAmn, wasteDestination.GetChild(wasteDestination.childCount - 2), 0f, animationsTime);

                await externalActions;
                await ChangeSimbol(numberRb);

                await RibossomeQueueMove();
                
                noRBSintetized = false;
                return;
            }

            if(!lastOne){ 
                int childOneChildCount = queue.GetChild(1).childCount - 1;
                moveObjectAmn = queue.GetChild(1).GetChild(childOneChildCount); //Gets the second child
                
                moveObjectAmn.SetParent(holdRibossome); //Deparenting it from moveObject
                taskAnimation[0] = MoveTowardsEnter(moveObjectAmn, wasteDestination.GetChild(0), 0f, animationsTime);
            }else{
                taskAnimation[0] = EndSimbolRibossome(); //No more ribossome, so it's ok to not use the simbol ribossome anymore
            }

            lastChild.SetAsFirstSibling();
            moveObject.SetParent(holdRibossome);//Deparenting, so there is no problem with layout group
            
            //Move to "infinity" the ribossome in the ribossome queue
            taskAnimation[1] = MoveTowardsExit(moveObject, holdRibossome, endTarget, animationsTime);

            await Task.WhenAll(taskAnimation);
            await externalActions;

            lastChild.SetAsLastSibling();

            PoolObjectReset(moveObject, childPrefab); //Enqueue and setting it correctly

            //Moving the AMNQueue
            if(!lastOne){ //One is always invisible remember
                DestroyImmediate(moveObjectAmn.gameObject); //Destroy the AMN ball used by the ribossome
                await ChangeSimbol(numberRb);
            }
        }

        private async Task MoveTowards(Transform moveObject, Transform target){
            float speed = 3f;
            while(moveObject.position != target.position){
                moveObject.position = Vector3.MoveTowards(moveObject.position, target.position, speed * Time.deltaTime);
                await Task.Yield();
            }
        }

        private async Task MoveTowardsExit(Transform moveObject, Transform newParent,Transform target, float time){
            GameObject moveO = moveObject.gameObject;
            RectTransform rt = moveO.GetComponent<RectTransform>();

            LeanTween.move(moveO, target, time).setEase(animationCurve);
            LeanTween.rotate(moveO, new Vector3(0f, 0f, 45f), time);
            LeanTween.scale(rt, rt.localScale*0, time).setEase(animationCurve);
            
            await Task.Delay(Util.ConvertToMili(time));

            if(newParent == null){
                Destroy(moveO);
                return;
            }
            
            moveObject.SetParent(newParent);
        }

        private async Task MoveTowardsEnter(bool notFirst, Transform moveObject, Transform newParent, Transform target,float scaleMultiplier, float time){
            GameObject moveO = moveObject.gameObject;
            //Various tween that can be aplied
            LeanTween.move(moveO, target, time).setEase(animationCurve);
            LeanTween.rotate(moveO, new Vector3(0f,0f,0f),time - 0.2f);
            LeanTween.scale(moveO.GetComponent<RectTransform>(), moveO.GetComponent<RectTransform>().localScale*scaleMultiplier, time - 0.2f);

            await Task.Delay(Util.ConvertToMili(time));
            int position = newParent.childCount-1;

            //I absolutely hate this if in the code
            //But i didn't find any alternative
            //I spent 4 hours in this script
            //The problem is the way horizontal layout groups and something in UI works 
            if(notFirst){
                if(newParent.childCount == 2){
                    Destroy(newParent.GetChild(0).gameObject);
                    firtsOne.SetParent(newParent);
                    firtsOne.SetSiblingIndex(0);
                }
                //Parenting it in the Layout Group
                moveObject.SetParent(newParent);
                moveObject.SetSiblingIndex(position);
            }

            await Task.Yield();
            return;
        }

        private async Task MoveTowardsEnter(Transform moveObject, Transform target,float scaleMultiplier, float time){
            GameObject moveO = moveObject.gameObject;
            float timeCut = (time - 0.2f);
            //Various tween that can be aplied
            LeanTween.rotate(moveO, new Vector3(0f,0f,-45f), timeCut);
            LeanTween.scale(moveO.GetComponent<RectTransform>(), moveO.GetComponent<RectTransform>().localScale*scaleMultiplier, timeCut);
            LeanTween.move(moveO, target, time).setEase(animationCurve);

            await Task.Delay(Util.ConvertToMili(timeCut));

            LeanTween.rotate(moveO, new Vector3(0f,0f,0f), 0.2f);

            await Task.Delay(Util.ConvertToMili(0.2f));

            return;
        }
        
        public async Task RibossomeEnter(Color newRibossomeColor, string numberAMN){
            GameObject hold = pool.Dequeue();
            RibossomeLetter rl = hold.GetComponent<RibossomeLetter>();
            AMNLetter moveObjectAmn = hold.transform.
                GetChild(hold.transform.childCount - 1).GetComponent<AMNLetter>();

            pool.Enqueue(hold);
            
            rl.SetRibossomeColor(newRibossomeColor);
            rl.Setup(numberAMN);

            moveObjectAmn.SetAMNColor(newRibossomeColor);
            moveObjectAmn.Setup(numberAMN);
            
            hold.SetActive(true);
            
            //Terrible, i know, i don't like it too
            if(queue.childCount == 2 && firtsOne == null){
                firtsOne = hold.transform;
                await MoveTowardsEnter(false, firtsOne, queue, queue.GetChild(0), 2.5f, animationsTime);
                ChangeColorSimbolRibossome(hold.GetComponent<Image>().color);
            
                //await Task.Delay(Util.ConvertToMili(fadeColorTime));
                return;
            }
            //(bool notFirst, Transform moveObject, Transform newParent, Transform target,float scaleMultiplier, float time)
            await MoveTowardsEnter(true, hold.transform, queue, queue.GetChild(queue.childCount-1), 2.5f, animationsTime);
        
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

        public Transform GetSimbolBall(){
            return simbolRibossome;
        }

        public Color GetSimbolColor(){
            return simbolRibossome.GetChild(0).GetComponent<Image>().color;
        }

        public RibossomeLetter GetSimbolRibossomeRibossomeLetter(){
            return simbolRibossome.GetComponent<RibossomeLetter>();
        }
    }
}
