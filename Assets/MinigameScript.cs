using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

//Use Pooling

/*
    Rotates a Bow and Shoots a arrow on the player click
    If the Card is correct this phases end and a GameplayManager phase is called

    Here there will be some pooling
    The rotation of the bow will be realized in the Y and Z, always
*/
namespace PhasePart.Bow{
    public class MinigameScript : PhaseManagerMono{
        [SerializeField] GameObject originalScene; //It will make the original scene active or inactive

        [SerializeField] GameObject target = default;
        [SerializeField] GameObject bow = default; //Reference to the bow, so this object didn't need to be fixed on the bow
        [SerializeField] Vector3 thrownSpeed = default; //Speed that the arrow will be thrown

        [SerializeField] float speedBowRotationHorizontal = default;
        [SerializeField] float speedBowRotationVertical = default;

        [SerializeField] float bowLeftRotationLimit = default;
        [SerializeField] float bowRightRotationLimit = default;

        [SerializeField] float bowUpRotationLimit = default;
        [SerializeField] float bowDownRotationLimit = default;
        
        [SerializeField] Arrow arrowPrefab = default; //The prefab that will be used after the first shot
        [SerializeField] int quiverCapacity = default;
        private Queue<Arrow> quiver; //All the arrow inactive in the scene

        [SerializeField] Transform arrowSpawn = default; //Actual position of the arrow when the game starts
        private Arrow reference;

        private int phaseNumber = -1;
        private bool arrowFlying = false;
        private bool endPhase = false;
        Vector3 positionp = new Vector3(32f,0,-1f);
 
        void OnEnable(){
            originalScene.SetActive(false); //"Takes control" of the actual scene of the game, visually
        }
        void Start(){
            StarQuiver();
            reference = RechargeBow (new Vector3(30f,0,-1f), new Quaternion());
            BeginGame();

            print("ROTACAO = " + bow.transform.rotation.eulerAngles.y);
        }

        private void StarQuiver(){
            quiver = new Queue<Arrow>();
            int i;

            for(i = 0; i < quiverCapacity; i++){
                Arrow obj = Instantiate<Arrow>(arrowPrefab, arrowSpawn);
                obj.gameObject.SetActive(false);
                quiver.Enqueue(obj);
            }
        }

        private Arrow  RechargeBow(Vector3 positionp, Quaternion rotation){
            Arrow newObj = quiver.Dequeue();
            newObj.gameObject.SetActive(true);
            newObj.gameObject.transform.position = positionp;
            //newObj.gameObject.transform.rotation = rotation;
            quiver.Enqueue(newObj);
            return newObj;
        }

        void Setup(int phaseNumber){
            this.phaseNumber = phaseNumber;
        }

        private async void BeginGame(){
            do{
                await ShootingStance();
                await OnDragAiming();
            }while(await WaitForArrow());

            this.gameObject.SetActive(false); //Makes this object to be false, returning to the original scene
            EndPhase();
        }

        private async Task ShootingStance(){
            print("Entered");
            while(!Input.GetButtonDown("Fire1")){
                await Task.Yield();
            }
            print("Exit");
        }

        private async Task OnDragAiming(){
            //play animatiom
            Quaternion myRotation = Quaternion.identity;
            Quaternion copyRotation;


            while(Input.GetMouseButton(0)){
                print("While");
                //Rotate object
                bow.transform.Rotate(Vector3.left * Input.GetAxis("Mouse X") * Time.deltaTime * speedBowRotationHorizontal);
                //bow.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * speedBowRotationVertical);

                copyRotation = bow.transform.rotation;
                
                print(copyRotation.eulerAngles.y);
                
                myRotation.eulerAngles = new Vector3(Mathf.Clamp(copyRotation.eulerAngles.x, bowUpRotationLimit, bowDownRotationLimit),
                                                   Mathf.Clamp(copyRotation.eulerAngles.y, bowLeftRotationLimit, bowRightRotationLimit), 
                                                   90);

                //myRotation.eulerAngles = new Vector3(Mathf.Clamp(copyRotation.eulerAngles.x, bowUpRotationLimit, bowDownRotationLimit), copyRotation.eulerAngles.y,  
                                                   //copyRotation.eulerAngles.z);

                bow.transform.rotation = myRotation;

                await Task.Yield();
            }
            ShootArrow();
            //await ShootingStance();
            //if(!animation.isPlaying) ShootArrow();
        } 

       
        
        private async Task<bool> WaitForArrow(){
            while(arrowFlying){
                await Task.Yield();
            }
            return !endPhase;
        }

        private void ShootArrow(){
            arrowFlying = true;
            reference.Shoot(thrownSpeed);
        }

        public bool SetHit(int wantedTarget, GameObject objectHitted){
            arrowFlying = false;

            if(wantedTarget == phaseNumber){
                //print("ENTROU");
                endPhase = true;
                return true;
            }
            else{
                RechargeBow(positionp, new Quaternion());
            }
            return false;
        }
    }
}
