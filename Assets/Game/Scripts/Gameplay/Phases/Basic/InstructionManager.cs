using UnityEngine;
using UnityEngine.UI;
    
namespace PhasePart{
    /*
        Fade with Canvas Group
        https://forum.unity.com/threads/how-can-i-fade-in-out-a-canvas-group-alpha-color-with-duration-time.969864/
        Using almost all the code of Zer0Cool

        //Block the flow of the game while the instruction is on
    */
    public class InstructionManager : MonoBehaviour{

        [Space]
        [Header("Instruction Objects")]
        [Space]
        [SerializeField] Button instructionReminder; //Show again the instruction

        [Space]
        [Header("Fading Variables")]
        [Space]
        [SerializeField] float fadingSpeed = 1f;

        public enum Direction {FadeIn, FadeOut};

        
        //public delegate void ButtomAnimation(RectTransform rt, Vector3 pos, float time);
        public delegate void ButtomClick();

        public void SetInstructionReminder(ButtomClick bt){
            instructionReminder.onClick.AddListener(delegate{bt(); }); 
        }

        public void ButtonChangeOfScaleAnimation(Vector3 scale, float time){
            Util.ChangeScaleAnimation(instructionReminder.GetComponent<RectTransform>(), 
                    scale, time);
        }

        public float GetFadeTime(){
            return fadingSpeed;
        }
    }
}
