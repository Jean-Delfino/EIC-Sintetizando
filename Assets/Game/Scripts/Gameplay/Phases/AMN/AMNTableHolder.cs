using UnityEngine;

using GameUserInterface.Animation;

public class AMNTableHolder : MonoBehaviour
{
    private SpriteAnimator tableAnimator;

    private void Awake()
    {
        tableAnimator = transform.GetComponentInChildren<FlipAnimation>();
    }

    public void ChangeTable(Sprite table){
        tableAnimator.DoSpriteChangeAnimation(table);
    }
}
