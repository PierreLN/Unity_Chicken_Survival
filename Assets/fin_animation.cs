using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fin_animation : StateMachineBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ChangeSprite();
    }

    void ChangeSprite()
    {
        spriteRenderer.sprite = newSprite;
    }

}
