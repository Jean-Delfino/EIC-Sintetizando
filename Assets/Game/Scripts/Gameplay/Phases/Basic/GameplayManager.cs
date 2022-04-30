using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using PhasePart;
using PhasePart.Wait;
using GameUserInterface.Text;

//Colocar o Pooling

/*
    Component responsable for the Gameplay, it organize the flow, but not the interactions

    There's no need to do a complete pooling in this object, because i don't want to instantiate all the objects
    The objects will be in the original scene already, so i just "activate them"
*/


public sealed class GameplayManager : MonoBehaviour{
    private GameObject objRef = null;

    [SerializeField] List<Phase> gamePhases;
    
    [System.Serializable]
    private class Phase{
        public PhaseManagerMono manager;
        public GameObject instructions;
        public List<string> messages = default; //Used by InfoDisplay
        public List<string> textInstructions = default; //Used in the Marking
    } 
    private int actualPhase = -1;
    [SerializeField] Marking marking; //Denotates which is the current phase
    [SerializeField] TextMeshProUGUI totalMessages; //Information in the display
    [SerializeField] TextMeshProUGUI actualMessage;
    [SerializeField] TextMeshProUGUI messageContent;
    private int actualMessageIndex;

    [SerializeField] GameObject waitManager; //Appear between phases, phaseInstruction basically
    private bool onAwait = false;

    private void Start(){
        //SetRandomCardPosition(); //Not used anymore
        SpawnAllGoals();
        IncreacePhase(); //actualPhase always just increace, so starting with -1 is correct
    }

    public void IncreacePhase(){
        actualPhase++;
        
        if(actualPhase == gamePhases.Count){
            print("Jogo acabou");
            return;
        }
        //DestroyAllInstantiated(); //I make the null treatment already
        //In the beggining it have no instance
        PoolObject(objRef); //Just setting the object to true or false, not actually a entire poll
        
        ManagerWait(); //This will make something that wait for the player interaction
        WaitFor();
    }

    private void ManagerWait(){
        objRef = waitManager; //The first object objRef gets is waitManager, always
        PoolObject(objRef); //Set it to active

        PhaseDescription aux = gamePhases[actualPhase].manager.GetPhaseDescription();
        objRef.GetComponent<MissionDisplay>().Setup(actualPhase, aux.GetName(), 
            aux.GetDescription(), aux.GetAdditionalInfo());
    }

    private void SpawnAllGoals(){
        int i;

        for(i = 0; i < gamePhases.Count; i++){
            marking.SpawnGoal(gamePhases[i].textInstructions);
        }
    }

    public bool Check(int numberPhase){
        if(onAwait && numberPhase == actualPhase){
            PoolObject(objRef); //Make the wait manager to be inactive
            //DestroyAllInstantiated();
            //objRef = Instantiate<GameObject>(gamePhases[actualPhase].manager.gameObject , this.transform);
            objRef =  gamePhases[actualPhase].manager.gameObject;
            PoolObject(objRef); //Makes one of the PhaseManagers to be active

            RestartPhase();
            marking.ShowGoal(actualPhase); //Puts the actual phase as the goal of the gameplay
            return true;
        }   

        return false;
    }
    public void SetDescriptionPhase(){
        PhaseDescription pdSetup = gamePhases[actualPhase].manager.GetPhaseDescription();
        objRef.GetComponent<MissionDisplay>().Setup(actualPhase, 
            pdSetup.GetName(), pdSetup.GetDescription(), pdSetup.GetAdditionalInfo());
    }


    public void WaitFor(){
        onAwait = true;
    }
    private void RestartPhase(){
        actualMessageIndex = 0;
        totalMessages.text = (gamePhases[actualPhase].messages.Count).ToString();
        ShowText();
        
    }
    private void ShowText(){
        messageContent.text = gamePhases[actualPhase].messages[actualMessageIndex];
        actualMessage.text = (actualMessageIndex + 1).ToString();
    }
    public void IncreaceMessage(int increace){
        if(actualMessageIndex == gamePhases[actualPhase].messages.Count - 1){
            return;
        }

        actualMessageIndex += increace;
        ShowText();
    }
    public void DecreaceMessage(int increace){
        if(actualMessageIndex == 0){
            return;
        }

        actualMessageIndex -= increace;
        ShowText();
    }

    public void DestroyAllInstantiated(){
        if(objRef == null){ return;}

        Destroy(objRef);
    }

    private void PoolObject(GameObject pool){
        if(pool == null) return;

        pool.SetActive(!pool.activeSelf); //Will change to pool
    }
    
    
}

//https://www.jacksondunstan.com/articles/2972
    /*
    public void SetRandomCardPosition(){ //Not used anymore
        int[] availablePosition = new int[gamePhases.Count];
        int i;

        Util.SequencialFeed(availablePosition, gamePhases.Count);
        Util.ShuffleArray(availablePosition);
        //Util.PrintVector(availablePosition);

        for(i = 0 ; i < gamePhases.Count - 1; i++){
            cardContainer.GetChild(i).SetSiblingIndex(availablePosition[i]);
        }
    }*/
