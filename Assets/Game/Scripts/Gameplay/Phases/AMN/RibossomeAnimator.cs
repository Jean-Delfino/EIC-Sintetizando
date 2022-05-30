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

        [SerializeField] Transform amnQueue = default;
        [SerializeField] GameObject amnPrefab = default;

        [SerializeField] float howMuchSpace = -43;

        
        private void Start() {
            SetPool(3);
            Tests();
        }

        private async void Tests(){
            await RibossomeEnter(Util.RandomSolidColor(),"1");
            await RibossomeEnter(Util.RandomSolidColor(),"2");
            await RibossomeEnter(Util.RandomSolidColor(),"3");
            await RibossomeExit(amnQueue,amnPrefab);
            await RibossomeEnter(Util.RandomSolidColor(),"4");
            await RibossomeExit(amnQueue,amnPrefab);
            await RibossomeEnter(Util.RandomSolidColor(),"5");
        }

        private void PoolObjectReset(Transform newElement, GameObject childZero){
            pool.Enqueue(newElement.gameObject);

            Util.CopyRectTransform(newElement.GetComponent<RectTransform>(),
                rectTransformValuesReference);
            
            GameObject child = Instantiate<GameObject>(childZero, newElement);
            child.transform.SetSiblingIndex(0);
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

        private void ChangeColorSimbolRibossome(Color newColor){
            AMNLetter moveObjectAmn = simbolRibossome.GetChild(1).GetComponent<AMNLetter>();

            LeanTween.color(simbolRibossome.GetChild(0).GetComponent<RectTransform>(), newColor, fadeColorTime);
            LeanTween.color(moveObjectAmn.ReturnAMNLetterImage().transform.GetComponent<RectTransform>(), newColor, fadeColorTime);
        }   

        //Every object in the queue has a AMN (except the last one)
        //So we need to animate this too
        //Waste destination is the AMN queue
        public async Task RibossomeExit(Transform wasteDestination, GameObject childPrefab){
            Transform moveObject = queue.GetChild(0); //Takes out the first ribossome of the list
            Transform moveObjectAmn = moveObject.GetChild(0); //Take the AMNLetter

            moveObject.SetParent(holdRibossome);//Deparenting, so there is no problem with layout group
            moveObjectAmn.SetParent(holdRibossome); //Deparenting it from moveObject
            
            Task[] taskAnimation = new Task[2];
            //(Transform moveObject, Transform target,float scaleMultiplier, float time)
            taskAnimation[0] = MoveTowardsEnter(moveObjectAmn, wasteDestination.GetChild(0), 0f, animationsTime);
            taskAnimation[1] = MoveTowardsExit(moveObject, holdRibossome, endTarget, animationsTime);

            await Task.WhenAll(taskAnimation);

            PoolObjectReset(moveObject, childPrefab); //Enqueue and setting it correctly
            DestroyImmediate(moveObjectAmn.gameObject);
            //moveObjectAmn.SetParent(wasteDestination); //Don't need to change the Sinbling Index
            //moveObjectAmn.SetSiblingIndex(wasteDestination.childCount-2);
            LeanTween.moveY(wasteDestination.GetComponent<RectTransform>(), wasteDestination.position.y + howMuchSpace, 1f);
            
            await Task.Delay(Util.ConvertToMili(1f));

            simbolRibossome.GetChild(1).SetParent(wasteDestination);
            Instantiate<GameObject>(childPrefab, simbolRibossome);

            LeanTween.moveY(wasteDestination.GetComponent<RectTransform>(), wasteDestination.position.y - howMuchSpace, 1f);

            await Task.Delay(Util.ConvertToMili(1f));

            if(queue.childCount > 1) {
                ChangeColorSimbolRibossome(queue.GetChild(0).GetComponent<Image>().color);
                
                await Task.Delay(Util.ConvertToMili(fadeColorTime));
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
            AMNLetter moveObjectAmn = hold.transform.GetChild(0).GetComponent<AMNLetter>();

            pool.Enqueue(hold);
            
            hold.GetComponent<Image>().color = newRibossomeColor;
            
            moveObjectAmn.SetAMNColor(newRibossomeColor);
            moveObjectAmn.Setup(numberAMN);
            
            hold.SetActive(true);
            
            //Terrible, i know, i don't like it too
            if(queue.childCount == 2 && firtsOne == null){
                firtsOne = hold.transform;
                await MoveTowardsEnter(false, hold.transform, queue, queue.GetChild(0), 2.5f, animationsTime);
                ChangeColorSimbolRibossome(hold.GetComponent<Image>().color);
            
                await Task.Delay(Util.ConvertToMili(fadeColorTime));
                return;
            }

            await MoveTowardsEnter(true, hold.transform, queue, queue.GetChild(queue.childCount-1), 2.5f, animationsTime);
            //(Transform moveObject, Transform newParent, Transform target,float scaleMultiplier, float time)
        }
    }
}
