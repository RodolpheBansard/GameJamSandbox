using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    public GameObject cellPrefab;

    private bool isplayerTurn = true;

    [SerializeField]
    private TicTacToeCell[][] ticTacToeCells = new TicTacToeCell[3][];

    private void Start()
    {
        int compteur = 0;
        
        for (int i = 9; i > 0; i-=3)
        {
            ticTacToeCells[compteur] = new TicTacToeCell[3];
            for (int j = 0; j < 9; j+=3)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector2(j-3, i-6), Quaternion.identity);
                cell.transform.parent = transform;
                ticTacToeCells[compteur][j / 3] = cell.GetComponent<TicTacToeCell>();
            }
            compteur++;
        }
        
        
    }

    public bool IsPlayerTurn()
    {
        return isplayerTurn;
    }

    public void EndPlayerTurn()
    {
        isplayerTurn = false;
        print(isWin());
        StartCoroutine(BotTurn());
    }

    IEnumerator BotTurn()
    {
        yield return new WaitForSeconds(1);

        List<TicTacToeCell> liste = new List<TicTacToeCell>();

        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (ticTacToeCells[i][j].GetState() == CellState.None)
                {
                    liste.Add(ticTacToeCells[i][j]);
                }
            }
            
        }
        if(liste.Count > 0)
        {
            int index = Random.Range(0, liste.Count);
            liste[index].BotTurn();
            isplayerTurn = true;
        }
        print(isWin());
    }


    private bool isWin()
    {
        //row
        for(int i=0; i<3; i++)
        {
            if(ticTacToeCells[i][0].GetState() != CellState.None && ticTacToeCells[i][0].GetState() == ticTacToeCells[i][1].GetState() && ticTacToeCells[i][1].GetState() == ticTacToeCells[i][2].GetState())
            {
                return true;
            }
        }
        // column
        for (int i = 0; i < 3; i++)
        {
            if (ticTacToeCells[0][i].GetState() != CellState.None && ticTacToeCells[0][i].GetState() == ticTacToeCells[1][i].GetState() && ticTacToeCells[1][i].GetState() == ticTacToeCells[2][i].GetState())
            {
                return true;
            }
        }
        if (ticTacToeCells[0][0].GetState() != CellState.None && ticTacToeCells[0][0].GetState() == ticTacToeCells[1][1].GetState() && ticTacToeCells[1][1].GetState() == ticTacToeCells[2][2].GetState())
            return true;
        if (ticTacToeCells[2][0].GetState() != CellState.None && ticTacToeCells[2][0].GetState() == ticTacToeCells[1][1].GetState() && ticTacToeCells[1][1].GetState() == ticTacToeCells[0][2].GetState())
            return true;


        return false;
    }
}
