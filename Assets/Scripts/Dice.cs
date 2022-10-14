using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    // Start is called before the first frame update
    
    // Update is called once per frame
    public void ChangeColor(int color)
    {
        if (color == 1)
        {
            sprite.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (color == 2)
        {
            sprite.color = new Color(1f, 0.898f, 0.306f, 1f);
        }
        else
        {
            sprite.color = new Color(0.224f, 0.745f, 1f, 1f);
        }
    }
}
