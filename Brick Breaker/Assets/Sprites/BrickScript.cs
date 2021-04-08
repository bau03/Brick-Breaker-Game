using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int points;
    public int hitsToBreak;
    public Sprite hitsSprite;

    public void BreakBrick() {
        hitsToBreak--;
        GetComponent<SpriteRenderer>().sprite = hitsSprite;
    }
}
