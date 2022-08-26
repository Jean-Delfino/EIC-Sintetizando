using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Used in the BowAndArrowMinigame, serves to pass the phase
*/
public class Card3D : MonoBehaviour{
    //Each card has a number attached to the phase
    //This will be handled in the "gameplayManager"
    [SerializeField] private float targetVelocity = 30.0f;
    [SerializeField] int numberPhase;

    public Card3D(){}

    public void OnEnable(){ //Used in the start of every start of game

    }
    private void OnDestroy() {
        //this.transform.parent.GetComponent<BodyCard>().CheckEnd();
    }

    public int GetValue(){
        return numberPhase;
    }

    void Update() {

        transform.Translate(Vector3.left*Time.deltaTime*targetVelocity);
        inbound();
    }
    public void inbound(){
        System.Random rnd = new System.Random();
        System.Random rnd1 = new System.Random();
        if(transform.position.x < -100){
            transform.position = new Vector3(150,rnd.Next(-16,18)+transform.position.y,rnd1.Next(-10,10)+transform.position.z);
        }
    }
}
