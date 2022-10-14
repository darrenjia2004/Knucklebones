using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    public int[,] playerOneBoard;
    public int[,] playerTwoBoard;
    public int dice=0;
    [SerializeField] private Text[] playerOneColumnsText;
    public int[] playerOneScores;
    [SerializeField] private Text[] playerTwoColumnsText;
    public int[] playerTwoScores;
    [SerializeField] private Text playerOneTotalText;
    [SerializeField] private Text playerTwoTotalText;
    private int playerOneTotal;
    private int playerTwoTotal;
    public static bool currentPlayer;
    public static bool gameOver;
    private int coinFlip;
    [SerializeField] private GameObject[] diceSprites;
    private GameObject currentDiceSprite;
    [SerializeField] private GameObject rollTabletOne;
    [SerializeField] private GameObject rollTabletTwo;
    [SerializeField] private GameObject[] playerOneTiles;
    private GameObject[,] playerOneDiceOnBoard;
    [SerializeField] private GameObject[] playerTwoTiles;
    private GameObject[,] playerTwoDiceOnBoard;
    private int[] playerOneOccurences;
    private int[] playerTwoOccurences;
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private Text startText;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private Text endText;
    void Start()
    {
        ResetGame();
    }

    // Update is called once per frame
    
    
    void rollNewDice()
    {
        dice = Random.Range(1, 7);
        if (currentPlayer)
        {
            for (int i = 0; i < 6; i++)
            {
                if (dice == i + 1)
                {
                    currentDiceSprite = Instantiate(diceSprites[i], rollTabletOne.transform.position, Quaternion.identity);
                }
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                if (dice == i + 1)
                {
                    currentDiceSprite = Instantiate(diceSprites[i], rollTabletTwo.transform.position, Quaternion.identity);
                }
            }
        }
        
        
    }
    public void placeDice(int column)
    {
        int filled = 0;
        if (currentPlayer) //checks if dice can be placed and places
        {
            for (int i = 0; i < 3; i++)
            {
                if (playerOneBoard[i, column] == 0)
                {
                    playerOneBoard[i, column] = dice;
                    break;
                }
                else
                {
                    filled++;
                }
            }
        }
        else
        {
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
        }

        //destroys opposing player dice
        if (filled != 3)
        {
            if (currentPlayer)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (playerTwoBoard[j, column] == dice)
                    {
                        playerTwoBoard[j, column] = 0;
                           
                    }
                }
            }
            else
            {
                for (int j = 0; j < 3; j++)
                {
                    if (playerOneBoard[j, column] == dice)
                    {
                        playerOneBoard[j, column] = 0;
                    }
                }
            }

            //collapses to center
            for (int i = 1; i < 3; i++)
            {
                if ((playerOneBoard[i, column] != 0) && (playerOneBoard[i - 1, column] == 0))
                {
                    playerOneBoard[i - 1, column] = playerOneBoard[i,column];
                    playerOneBoard[i, column] = 0;
                }
            }
            
            for (int i = 1; i < 3; i++)
            {
                if ((playerTwoBoard[i, column] != 0) && (playerTwoBoard[i - 1, column] == 0))
                {
                    playerTwoBoard[i - 1, column] = playerTwoBoard[i,column];
                    playerTwoBoard[i, column] = 0;
                }
            }
            Destroy(currentDiceSprite);
            CalculateScore(column);
        }
        
    }
    private void CalculateScore(int column)
    {
        playerOneTotal = 0;
        playerOneScores[column] = 0;
        playerOneOccurences = new int[] {0, 0, 0, 0, 0, 0 };
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
        playerOneColumnsText[column].text = playerOneScores[column].ToString();
        for (int j = 0; j < 3; j++)
        {
           playerOneTotal += playerOneScores[j];
        }
        playerOneTotalText.text = playerOneTotal.ToString();
        
        playerTwoTotal = 0;
        playerTwoScores[column] = 0;
        playerTwoOccurences = new int[] { 0, 0, 0, 0, 0, 0 };
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
        playerTwoColumnsText[column].text = playerTwoScores[column].ToString();
        for (int j = 0; j < 3; j++)
        {
            playerTwoTotal += playerTwoScores[j];
        }
        playerTwoTotalText.text = playerTwoTotal.ToString();

        int filledOne = 0;
        int filledTwo = 0;
        for (int i =0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (playerOneBoard[i, j] != 0)
                {
                    filledOne++;
                }
                if (playerTwoBoard[i, j] != 0)
                {
                    filledTwo++;
                }
            }
        }
        if (filledOne!=9 && filledTwo != 9)
        {
            currentPlayer = !currentPlayer;
            ClearBoard();
            AddBoard();
            rollNewDice();
        }
        else
        {
            ClearBoard();
            AddBoard();
            EndGame();
        }
    }
    private void EndGame()
    {
        gameOver = true;
        if (playerOneTotal > playerTwoTotal)
        {
            endText.text = "Player 1 Wins";
            endCanvas.SetActive(true);
        }
        else if (playerTwoTotal > playerOneTotal)
        {
            endText.text = "Player 2 Wins";
            endCanvas.SetActive(true);
        }
        else
        {
            if (currentPlayer)
            {
                endText.text = "Player 1 Wins";
                endCanvas.SetActive(true);
            }
            else
            {
                endText.text = "Player 2 Wins";
                endCanvas.SetActive(true);
            }
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClearBoard();
            ResetGame();
        }
    }
    public void ClearBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (playerOneDiceOnBoard[i, j] != null)
                {
                    Destroy(playerOneDiceOnBoard[i, j]);
                }
                if (playerTwoDiceOnBoard[i, j] != null)
                {
                    Destroy(playerTwoDiceOnBoard[i, j]);
                }
            }
        }
    }
    private void AddBoard()
    {
        for (int row = 0; row < 3; row++)
        {
            for (int column = 0; column < 3; column++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (playerOneBoard[row,column] == j + 1)
                    {
                        playerOneDiceOnBoard[row, column] = Instantiate(diceSprites[j], playerOneTiles[3 * row + column].transform.position, Quaternion.identity);
                    }
                }
            }
        }
        for (int row = 0; row < 3; row++)
        {
            for (int column = 0; column < 3; column++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (playerTwoBoard[row, column] == j + 1)
                    {
                        playerTwoDiceOnBoard[row, column] = Instantiate(diceSprites[j], playerTwoTiles[3 * row + column].transform.position, Quaternion.identity);
                    }
                }
            }
        }
        for (int column = 0; column < 3; column++)
        {
            playerOneOccurences = new int[] { 0, 0, 0, 0, 0, 0 };
            playerTwoOccurences = new int[] { 0, 0, 0, 0, 0, 0 };
            for (int row = 0; row < 3; row++)
            {
                if (playerOneBoard[row, column] != 0)
                {
                    playerOneOccurences[playerOneBoard[row, column] - 1]++;
                }
                if (playerTwoBoard[row, column] != 0)
                {
                    playerTwoOccurences[playerTwoBoard[row, column] - 1]++;
                }
            }
            for (int row = 0; row < 3; row++)
            {
                if (playerOneBoard[row, column] != 0)
                {
                    playerOneDiceOnBoard[row, column].GetComponent<Dice>().ChangeColor(playerOneOccurences[playerOneBoard[row,column]-1]);
                }
                if (playerTwoBoard[row, column] != 0)
                {
                    playerTwoDiceOnBoard[row, column].GetComponent<Dice>().ChangeColor(playerTwoOccurences[playerTwoBoard[row, column] - 1]);
                }
            }
        }
    }
    public void ResetGame()
    {
        endCanvas.SetActive(false);
        playerOneTotal = 0;
        playerTwoTotal = 0;
        gameOver = false;
        playerOneBoard = new int[,]{
            {0,0,0},
            {0,0,0},
            {0,0,0}
        };
        playerTwoBoard = new int[,]{
            {0,0,0},
            {0,0,0},
            {0,0,0}
        };

        playerOneOccurences = new int[] { 0, 0, 0, 0, 0, 0 };
        playerTwoOccurences = new int[] { 0, 0, 0, 0, 0, 0 };

        playerOneDiceOnBoard = new GameObject[,]{
            {null,null,null},
            {null,null,null},
            {null,null,null}
        };
        playerTwoDiceOnBoard = new GameObject[,]{
            {null,null,null},
            {null,null,null},
            {null,null,null}
        };
        playerOneTotalText.text = "0";
        playerTwoTotalText.text = "0";
        playerOneScores = new int[] { 0, 0, 0 };
        playerTwoScores = new int[] { 0, 0, 0 };
        coinFlip = Random.Range(1, 3);
        if (coinFlip == 1)
        {
            currentPlayer = true;
        }
        else
        {
            currentPlayer = false;
        }
        for (int i =0; i < 3; i++)
        {
            playerOneColumnsText[i].text = "0";
            playerTwoColumnsText[i].text = "0";
        }
        if (currentPlayer)
        {
            startText.text = "Player 1 Starts";
        }
        else
        {
            startText.text = "Player 2 Starts";
        }
        startCanvas.SetActive(true);
        StartCoroutine(StartCanvasOff());
        
        Destroy(currentDiceSprite);
        rollNewDice();
    }
    IEnumerator StartCanvasOff()
    {
        yield return new WaitForSeconds(1.5f);
        startCanvas.SetActive(false);
    }
}
