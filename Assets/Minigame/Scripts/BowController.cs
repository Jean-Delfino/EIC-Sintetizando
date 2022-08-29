using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
[RequireComponent(typeof(Rigidbody))]
public class BowController : MonoBehaviour
{
    [SerializeField] float bowUpRotationLimit = default;
    [SerializeField] float bowDownRotationLimit = default;
    [SerializeField] float bowLeftRotationLimit = default;
    [SerializeField] float bowRightRotationLimit = default;
    [SerializeField] float angularSpeed = default;
    public GameObject arrow;
    public float ForcaDoRigidbody = 100;
    public float PontoDeInicio;
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
    
    void FixedUpdate(){
    /*if(Input.GetMouseButtonDown(0)){
    PontoDeInicio = Time.time;
    }*/
    if(Input.GetMouseButtonUp(0)){
    float ForcaAcumulada = ForcaDoRigidbody - PontoDeInicio;
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(new Vector3(0,0,-70));
    if (Physics.Raycast(ray, out hit)){
    arrow.GetComponent<Rigidbody>().AddForceAtPosition(ray.direction * (ForcaAcumulada*ForcaDoRigidbody), hit.point);
            }
        }
    }
   
     private async void OnDragAiming(){
            //play animatiom
            Quaternion myRotation = Quaternion.identity;
            Quaternion copyRotation;


            while(Input.GetMouseButton(0)){
                print("While");
                //Rotate object
                this.transform.Rotate(Vector3.left * Input.GetAxisRaw("Mouse X") * Time.deltaTime*angularSpeed);
                //this.transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse Y") *angularSpeed);

                copyRotation = this.transform.rotation;
                
                //print(copyRotation.eulerAngles.y);
                
                myRotation.eulerAngles = new Vector3(Mathf.Clamp(copyRotation.eulerAngles.x, bowDownRotationLimit, bowUpRotationLimit),
                                                   Mathf.Clamp(copyRotation.eulerAngles.y, bowLeftRotationLimit, bowRightRotationLimit), 
                                                   90);

                //myRotation.eulerAngles = new Vector3(Mathf.Clamp(copyRotation.eulerAngles.x, bowUpRotationLimit, bowDownRotationLimit), copyRotation.eulerAngles.y,  
                                                   //copyRotation.eulerAngles.z);
                this.transform.rotation = myRotation;
                arrow.transform.rotation = myRotation;
                await Task.Yield();
            }
            FixedUpdate();
        } 
}
