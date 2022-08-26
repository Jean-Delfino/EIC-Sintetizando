using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    [SerializeField] private float targetVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   void Update() {

        transform.Translate(Vector3.left*Time.deltaTime*targetVelocity);
        inbound();
    }
    public void inbound(){
        System.Random rnd = new System.Random();
        System.Random rnd1 = new System.Random();
        if(transform.position.x < -20){
            transform.position = new Vector3(20,rnd.Next(-1,1)+transform.position.y,rnd1.Next(-60,0)+transform.position.z);
        }
    }
}
