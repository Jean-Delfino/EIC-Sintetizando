using UnityEngine;

using PhasePart.Bow;

/*
    Bow projectile for the minigame
*/

public class Arrow : MonoBehaviour{
    Rigidbody rb;

    void Start(){
        rb = this.GetComponent<Rigidbody>();
        //if(rb == null) print("Bizarre");
        ChangeGravityRigidbody(false);
    }
    void VerifyHit(int value, GameObject hitted){
        FindObjectOfType<BowAndArrowMinigame>().SetHit(value, hitted);
    }

    public void Shoot(Vector3 force){
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;

        rb.AddForce(force, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other){
       Card3D cardVerify = other.transform.GetComponent<Card3D>();

       print("Collidiu");
       
       if(cardVerify != null){
            //print("Carta");
            VerifyHit(cardVerify.GetValue(), other.gameObject);
           
            this.gameObject.SetActive(false); //Needed for the pooling
            other.gameObject.SetActive(false); //Will be used later in the pooling
       }else{
            //print("UE");
            this.rb.constraints = RigidbodyConstraints.FreezeAll; //Locks the arrow in the wall
       }

        ChangeGravityRigidbody(false);
    }

    public Rigidbody GetRigidbody(){
        return this.rb;
    }

    public void ChangeGravityRigidbody(bool state){
       rb.useGravity = state;
    }
}
