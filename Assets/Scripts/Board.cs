using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{    
    public List<Image> imgItems;

    public Image[,] imgBoards;

    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    public float boxSize;
    
    private void Start()
    {
        imgBoards = new Image[8, 8];
        int col = 0;
        int row = 0;
        for (int i = 0; i < imgItems.Count; i++)
        {
            row = i / 8;
            col = i % 8;

            imgBoards[row, col] = imgItems[i];           
        }
    }

    public void CheckScore()
    {
        List<Image> imgEat = new List<Image>();

        List<Image> imgRowTemp = new List<Image>();
        List<Image> imgColTemp = new List<Image>();
        for (int row = 0; row < 8; row++)
        {
            imgRowTemp.Clear();
            imgColTemp.Clear();
            for (int col = 0; col < 8; col++)
            {
                if (imgBoards[row, col].color == Color.white) imgRowTemp.Add(imgBoards[row, col]);
                if (imgBoards[col, row].color == Color.white) imgColTemp.Add(imgBoards[col, row]);
            }

            if (imgRowTemp.Count == 8)
            {
                imgEat.AddRange(imgRowTemp);
                ScoreTxt.scoreValue += 100;
            }

            if (imgColTemp.Count == 8)
            {
                imgEat.AddRange(imgColTemp);
                ScoreTxt.scoreValue += 100;
            }
        }

        foreach (var img in imgEat)
        {
            img.color = Color.green;
        }

    }
    public Vector2Int GetIndex(float x, float y)
    {
        int indexX = (int)((x + -xMin) / boxSize);
        int indexY = (int)((-y + yMin) / boxSize);

        if (indexX < 0 || indexX > 7 || indexY < 0 || indexY > 7)
        {
            return Vector2Int.down;
        }
        else
        {
            return new Vector2Int(indexX, indexY);
        }
    }

    public void BoardtoGreen()
    {
        imgItems.ForEach(a => a.color = Color.green);
    }
    
}


//[ContextMenu("Test")]
//public void Test()
//{
//    // trắng hết côt 1
//    for(int i = 0; i < 8; i++)
//    {
//        for (int j = 0; j < 8; j++)
//        {
//            imgBoards[i, j].color = Color.white;
//        }
//    }
//}








