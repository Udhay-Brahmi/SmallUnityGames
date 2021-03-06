﻿using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] GameObject lineBase;
    List<GameObject> lines;
    GridState gs;

    void Start()
    {
        if (lineBase == null)
        {
            lineBase = new GameObject("error");
        }

        gs = GetComponent<GridState>();
        lines = new List<GameObject>();
    }

    public void DrawLines()
    {
        Letter[,] gridLetters = gs.GridLetters;
        ClearAllLines();
        // Checking the rows:
        for (int row = 0; row < 3; row++)
        {
            if ((gridLetters[0, row] != Letter.Blank) && (gridLetters[0, row] == gridLetters[1, row]) && (gridLetters[0, row] == gridLetters[2, row]))
            {
                CreateLine(-3.5f, (row - 1) * 2, 3.5f, (row - 1) * 2);
            }
        }
        // Checking the columns:
        for (int col = 0; col < 3; col++)
        {
            if ((gridLetters[col, 0] != Letter.Blank) && (gridLetters[col, 0] == gridLetters[col, 1]) && (gridLetters[col, 0] == gridLetters[col, 2]))
            {
                CreateLine((col - 1) * 2, -3.5f, (col - 1) * 2, 3.5f);
            }
        }
        // Checking the diagonals:
        if ((gridLetters[0, 0] != Letter.Blank) && (gridLetters[0, 0] == gridLetters[1, 1]) && (gridLetters[0, 0] == gridLetters[2, 2]))
        {
            CreateLine(-3.5f, -3.5f, 3.5f, 3.5f);
        }
        if ((gridLetters[0, 2] != Letter.Blank) && (gridLetters[0, 2] == gridLetters[1, 1]) && (gridLetters[0, 2] == gridLetters[2, 0]))
        {
            CreateLine(-3.5f, 3.5f, 3.5f, -3.5f);
        }
    }

    void ClearAllLines()
    {
        foreach (GameObject line in lines)
        {
            Destroy(line);
        }
        lines.Clear();
    }

    void CreateLine(float x0, float y0, float x1, float y1)
    {
        float baseX = gameObject.transform.position.x;
        float baseY = gameObject.transform.position.y;
        GameObject newLine = Instantiate(lineBase);
        newLine.transform.SetParent(gameObject.transform);
        newLine.name = "Line" + lines.Count.ToString();
        Vector3[] positions = new Vector3[2];
        positions[0] = new Vector3(baseX + x0, baseY + y0, -5);
        positions[1] = new Vector3(baseX + x1, baseY + y1, -5);
        newLine.GetComponent<LineRenderer>().SetPositions(positions);
        lines.Add(newLine);
    }
}
