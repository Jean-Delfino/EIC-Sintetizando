using UnityEngine;
using TMPro;

namespace ProteinPart.InfoProtein{
    public class ButtonProteinExtra : MonoBehaviour{
        [SerializeField] TextMeshProUGUI nameExtra = default;
        private SynthesizingProteinShow owner;
        private int index;

        public void OnClick(){
            owner.ShowDescriptionExtraData(index);
        }
        
        public void Setup(SynthesizingProteinShow owner, string name, int index){
            this.owner = owner;
            this.nameExtra.text = name;
            this.index = index;
        }

    }
}
