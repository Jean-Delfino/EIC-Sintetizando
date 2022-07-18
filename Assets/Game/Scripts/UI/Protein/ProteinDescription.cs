using UnityEngine;
using UnityEngine.UI;

namespace ProteinPart.InfoProtein{
[CreateAssetMenu(fileName = "Data", menuName = "ProteinDescription")]
    public class ProteinDescription : ScriptableObject{
        [TextArea(15,20)]
        public string proteinDNA;
        public SynthesizingProtein sp;
    }
}