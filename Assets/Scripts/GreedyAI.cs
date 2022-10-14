using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyAI : MonoBehaviour
{
    // Start is called before the first frame update
    private bool coroutineStarted = false;
    [SerializeField] GameObject board;
    private int[,] playerTwoBoard;
    private int[,] playerOneBoard;
    private Board boardComponent;
    void Start()
    {
        StartCoroutine(DelayedStart());

    }
    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(.02f);
        boardComponent = board.GetComponent<Board>();
        playerTwoBoard = new int[,]{
            {0,0,0},
            {0,0,0},
            {0,0,0}
        };
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                playerTwoBoard[i, j] = boardComponent.playerTwoBoard[i, j];
            }
        }
        playerOneBoard = new int[,]{
            {0,0,0},
            {0,0,0},
            {0,0,0}
        };
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                playerOneBoard[i, j] = boardComponent.playerOneBoard[i, j];
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
        yield return new WaitForSeconds(.1f);
        int max = 0;
        int dice = boardComponent.dice;
        int[] playerOneScores = { 0, 0, 0 };
        int[] playerTwoScores = { 0, 0, 0 };
        int[] playerOneScoresOriginal = { 0, 0, 0 };
        int[] playerTwoScoresOriginal = { 0, 0, 0 };
        int[] scoreDifference = { 0, 0, 0 };
        
        for (int column =0; column<3; column++)
        {
            StartCoroutine(CopyBoard());
            yield return new WaitForSeconds(.1f);

            
            int filled = 0;

            for (int i = 0; i < 3; i++)
            {
                playerOneScoresOriginal[i] = boardComponent.playerOneScores[i];
                playerTwoScoresOriginal[i] = boardComponent.playerTwoScores[i];
            }

            for (int i = 0; i < 3; i++)
            {
                if (playerTwoBoard[i, column] == 0)
                {
                    playerTwoBoard[i, column] = dice;
                    break;
                }
                else
                {
                    filled++;
                }
            }
            if (filled != 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (playerOneBoard[j, column] == dice)
                    {
                        playerOneBoard[j, column] = 0;
                    }
                }
            }
            

            int[] playerOneOccurences = { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 3; i++)
            {
                if (playerOneBoard[i, column] != 0)
                {
                    playerOneOccurences[playerOneBoard[i, column] - 1]++;
                }
            }
            for (int k = 0; k < 6; k++)
            {
                playerOneScores[column] += playerOneOccurences[k] * playerOneOccurences[k] * (k + 1);
            }
            int[] playerTwoOccurences ={ 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 3; i++)
            {
                if (playerTwoBoard[i, column] != 0)
                {
                    playerTwoOccurences[playerTwoBoard[i, column] - 1]++;
                }
            }
            for (int k = 0; k < 6; k++)
            {
                playerTwoScores[column] += playerTwoOccurences[k] * playerTwoOccurences[k] * (k + 1);
            }
        }
        for (int i=0; i < 3; i++)
        {
            scoreDifference[i] = playerTwoScores[i]-playerTwoScoresOriginal[i]+playerOneScoresOriginal[i] - playerOneScores[i];
            if (max < scoreDifference[i])
            {
                max = scoreDifference[i];
            }
        }
        float[] thresholds = { 0f, 0f, 0f };
        float sum = 0f;
        for (int i=0; i < 3; i++)
        {
            if (scoreDifference[i] == max)
            {
                sum += 1f;
            }
            thresholds[i] = sum;
        }
        float random = Random.Range(0f, sum);
        int columnChoice = 0;
        for (int i = 0; i < 3; i++)
        {
            if (random < thresholds[i])
            {
                columnChoice = i;
                break;
            }
        }
        

        yield return new WaitForSeconds(1.0f);
        
        
        boardComponent.placeDice(columnChoice);

        coroutineStarted = false;
    }

    IEnumerator CopyBoard()
    {
        yield return new WaitForSeconds(.05f);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                playerTwoBoard[i, j] = boardComponent.playerTwoBoard[i, j];
            }
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                playerOneBoard[i, j] = boardComponent.playerOneBoard[i, j];
            }
        }
    }
}
