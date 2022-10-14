using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStage : MonoBehaviour
{
    [SerializeField] private GameObject board;
    // Start is called before the first frame update
    public void resetGame()
    {
        board.GetComponent<Board>().ClearBoard();
        board.GetComponent<Board>().ResetGame();
    }
}
