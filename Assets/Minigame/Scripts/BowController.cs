using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BowController : MonoBehaviour
{
    [SerializeField] float bowUpRotationLimit = default;
    [SerializeField] float bowDownRotationLimit = default;
    [SerializeField] float bowLeftRotationLimit = default;
    [SerializeField] float bowRightRotationLimit = default;
    [SerializeField] float angularSpeed = default;
    Rigidbody bow;
    // Start is called before the first frame update
    void Start()
    {
       // bow = this.GetComponent<Rigidbody>();
       // bow.constraints = RigidbodyConstraints.FreezeRotationZ;
        //comentario
    }

    // Update is called once per frame
    void Update()
    {
       OnDragAiming(); 
    }

     private async void OnDragAiming(){
            //play animatiom
            Quaternion myRotation = Quaternion.identity;
            Quaternion copyRotation;


            while(Input.GetMouseButton(0)){
                print("While");
                //Rotate object
                this.transform.Rotate(Vector3.left * Input.GetAxisRaw("Mouse Y") * angularSpeed);
                this.transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * angularSpeed);

                copyRotation = this.transform.rotation;
                
                //print(copyRotation.eulerAngles.y);
                
                myRotation.eulerAngles = new Vector3(Mathf.Clamp(copyRotation.eulerAngles.x, bowUpRotationLimit, bowDownRotationLimit),
                                                   Mathf.Clamp(copyRotation.eulerAngles.y, bowLeftRotationLimit, bowRightRotationLimit), 
                                                   90);

                //myRotation.eulerAngles = new Vector3(Mathf.Clamp(copyRotation.eulerAngles.x, bowUpRotationLimit, bowDownRotationLimit), copyRotation.eulerAngles.y,  
                                                   //copyRotation.eulerAngles.z);
                this.transform.rotation = myRotation;
                await Task.Yield();
            }
            
        //if(!animation.isPlaying) ShootArrow();
        } 
}
