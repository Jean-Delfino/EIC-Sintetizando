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
        
        [SerializeField] Transform amnQueue = default;
        [SerializeField] GameObject amnPrefab = default;

        private GameObject childPrefab;
        
        private void Start() {
            SetPool(3);
            Tests();
        }

        private async void Tests(){
            Color firstColor = Util.RandomSolidColor();
            //Task notFirst = ;
            childPrefab = amnPrefab;

            await RibossomeEnter(firstColor, "1", false);
            await RibossomeExit(true, Util.RandomSolidColor(), 2, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSinthetizingFromQueue(), "Gly"));
            /*
            await RibossomeEnter(Util.RandomSolidColor(),"2");
            //await RibossomeEnter(Util.RandomSolidColor(),"3");
            await RibossomeExit(true, Util.RandomSolidColor(), 2, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSinthetizingFromQueue(), "Gly"));

            await RibossomeEnter(Util.RandomSolidColor(),"2");
            */


            
            // await RibossomeEnter(Util.RandomSolidColor(),"3");

            // await RibossomeExit(false, 3, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "MNA"), amnQueue);
            // await RibossomeEnter(Util.RandomSolidColor(),"4");

            // await RibossomeExit(false, 4, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "NMA"), amnQueue);
            // await RibossomeEnter(Util.RandomSolidColor(),"5");

            // await RibossomeExit(false, 5, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "Gly"), amnQueue);
            // await RibossomeEnter(Util.RandomSolidColor(),"6");

            // await RibossomeExit(false, 6, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "AAA"), amnQueue);
            // await RibossomeEnter(Util.RandomSolidColor(),"7");

            // await RibossomeExit(false, 7, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "BBB"), amnQueue);
            // await RibossomeExit(false, 8, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "CCC"), amnQueue);

            // print("TEST -- ANIMATION ENDING");

            // await RibossomeExit(true, 9, amnQueue.GetComponent<AMNQueue>().PushNewAMN(GetSimbolBall(), GetSimbolColor(), "Fim"), amnQueue);
            
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

        //Every object in the queue has a AMN (except the last one)
        //So we need to animate this too
        //Waste destination is the AMN queue
        public async Task RibossomeExit(bool newAMN, 
            Color newRibossomeColor, int numberAMN, 
            Task externalActions){

            await RibossomeExit(externalActions, ribossomeQueue.GetRibossomeSinthetizing());

            if(newAMN){
                await RibossomeEnter(newRibossomeColor, numberAMN.ToString(), false);
            }

            await Task.Yield();
        }

        private async Task RibossomeExit(Task externalActions, RibossomeLetter rlSint){
            await ribossomeQueue.MoveAllRibossome(new Vector3(0, 0, 0), animationsTime, animationCurve);

            saveColor = rlSint.GetRibossomeColor();
            rlSint.SetAMNPresence();
            //currentlySynthesizing.transform.GetChild(0).GetComponent<Image>().color

            await externalActions;
        }

        public async Task RibossomeEnter(Color newRibossomeColor, string numberAMN, bool move){
            GameObject hold = pool.Dequeue();
            RibossomeLetter rl = hold.GetComponent<RibossomeLetter>();
            AMNLetter moveObjectAmn = hold.transform.
                GetChild(hold.transform.childCount - 1).GetComponent<AMNLetter>();

            pool.Enqueue(hold);

            rl.SetRibossomeColor(newRibossomeColor);
            rl.Setup(numberAMN);

            moveObjectAmn.SetAMNColor(newRibossomeColor);
            moveObjectAmn.Setup(numberAMN);

            if(!rl.GetAMNPresence()){
                PoolObjectReset(hold.transform, childPrefab);
            }

            hold.SetActive(true);
            
            await ribossomeQueue.MoveNewRibossome(move, rl, 
                new Vector3(0, 0, 0), 2.5f, animationsTime, animationCurve);
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
