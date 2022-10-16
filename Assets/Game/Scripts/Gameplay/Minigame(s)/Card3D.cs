using UnityEngine;

/*
    Used in the BowAndArrowMinigame, serves to pass the phase
*/
public class Card3D : MonoBehaviour{
    //Each card has a number attached to the phase
    //This will be handled in the "gameplayManager"
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
}
