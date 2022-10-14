using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    private SpriteRenderer sprite;
    public GameObject board;
    public int columnNum=1;
    public bool boardSide;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    
    private void OnMouseOver()
    {
        if (Board.currentPlayer == boardSide && Board.gameOver == false)
        {
            sprite.enabled = true;
        }
        
    }
    private void OnMouseExit()
    {
        if (Board.currentPlayer == boardSide && Board.gameOver == false)
        {
            sprite.enabled = false;
        }
            
    }
    private void OnMouseDown()
    {
        if (Board.currentPlayer == boardSide && Board.gameOver == false)
        {
            board.GetComponent<Board>().placeDice(columnNum);
            sprite.enabled = false;
        }
            
    }
}
