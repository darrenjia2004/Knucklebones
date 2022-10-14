using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : MonoBehaviour
{
    // Start is called before the first frame update
    private bool coroutineStarted = false;
    [SerializeField] GameObject board;
    private int[,] playerTwoBoard;
    void Start()
    {
        playerTwoBoard = new int[,]{
            {0,0,0},
            {0,0,0},
            {0,0,0}
        };
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                playerTwoBoard[i,j] = board.GetComponent<Board>().playerTwoBoard[i,j];
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Board.currentPlayer && !coroutineStarted && Board.gameOver == false)
        {
            StartCoroutine(ChooseColumn());
        }
    }

    IEnumerator ChooseColumn()
    {

        coroutineStarted = true;
        playerTwoBoard = new int[,]{
            {0,0,0},
            {0,0,0},
            {0,0,0}
        };
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                playerTwoBoard[i, j] = board.GetComponent<Board>().playerTwoBoard[i, j];
            }
        }
        yield return new WaitForSeconds(1.0f);
        int column = 0;
        int filled = 3;
        while (filled==3)
        {
            column = Random.Range(0, 3);
            filled = 0;
            for (int i = 0; i < 3; i++)
            {
                if (playerTwoBoard[i, column] != 0)
                {
                    filled++;
                }
            }
        }
        board.GetComponent<Board>().placeDice(column);
        
        coroutineStarted = false;
    }
}
