using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TicTacToeCell : MonoBehaviour
{
    public TMP_Text cellText;

    private TicTacToe ticTacToe;
    private CellState cellState;

    private void Start()
    {
        ticTacToe = FindObjectOfType<TicTacToe>();
    }

    private void OnMouseDown()
    {
        if (ticTacToe.IsPlayerTurn() && cellText.text.Equals(""))
        {
            cellState = CellState.Cross;
            cellText.text = "X";
            ticTacToe.EndPlayerTurn();
        }
    }

    public void BotTurn()
    {
        cellState = CellState.Circle;
        cellText.text = "O";
    }

    public CellState GetState()
    {
        return cellState;
    }




}


public enum CellState
{
    None,
    Cross,
    Circle
}