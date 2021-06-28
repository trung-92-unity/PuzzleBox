using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState 
{
    START,
    RUNNING,
    GAMEOVER
}

public class GameManager : MonoBehaviour
{
    public ScoreTxt scoreRs;
    public GameObject popupPanel;
    public Board board;
    public List<GameObject> boxFrefab;
    public Transform boxParent;

    public List<Vector3> firstPositions;


    public List<BoxItem> boxItems;

    [HideInInspector]
    public GameState gameState;

    private static GameManager instance;

    
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameState = GameState.START;
        boxItems = new List<BoxItem>();
        StartGame();
    }

    private void StartGame()
    {
        board.BoardtoGreen();
        CreateBoxItem();
    }

    private void CreateBoxItem()
    {
        for(int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, 7);
            GameObject go = Instantiate(boxFrefab[index], new Vector3(0, 0, 0), Quaternion.identity);
                           
            go.transform.SetParent(boxParent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = firstPositions[i];
            BoxItem bItem= go.GetComponent<BoxItem>();
            bItem.resetPosition = firstPositions[i];
            boxItems.Add(bItem);          
        }
    }
    
    /// <summary>
    /// Kiểm tra xem box đc thả vào bàn hay chưa
    /// chưa thì thả về vị trí cũ, thả trúng thì tô màu bàn
    /// </summary>
    /// <param name="box"></param>
    public void BoxMoveDone(BoxItem box)
    {
        Vector2Int index = board.GetIndex(box.transform.localPosition.x, box.transform.localPosition.y);
                           
        bool check = true;
        if (index == Vector2Int.down) // fail - cho box bay lại vị trí cũ
        {
            check = false;
        }
        else
        {
            foreach (Vector2Int config in box.configs)
            {
                
                int xNew = config.x + index.x;
                int yNew = config.y + index.y;

                if (xNew < 0 || xNew > 7 || yNew < 0 || yNew > 7)
                {
                    check = false;
                    break;
                }else if(board.imgBoards[index.y,index.x].color == Color.white)
                {
                    check = false;
                    break;
                }else if (board.imgBoards[yNew, xNew].color == Color.white)
                {
                    check = false;
                    break;
                }                
            }
        }
        
        if (check)
        {
            board.imgBoards[index.y, index.x].color = Color.white;
            foreach (Vector2Int config in box.configs)
            {                
                    int xNew = config.x + index.x;
                    int yNew = config.y + index.y;
                
                    board.imgBoards[yNew, xNew].color = Color.white;                                                                                
            }

            boxItems.Remove(box);
            Destroy(box.gameObject);
            
            if (boxItems.Count <= 0)
            {
                CreateBoxItem();
            }

            board.CheckScore();
            CheckColor();
        }
        else 
        {
            box.ResetPosition();
        }
        
    }
    public void CheckColor()
    {
        int row;
        int col;
        bool check = true;
        foreach (BoxItem bItem in boxItems)
        {
            for (row = 0; row < 8; row++)
            {
                for (col = 0; col < 8; col++)
                {
                    if (board.imgBoards[row, col].color == Color.white)
                    {
                        continue;
                    }
                    else
                    {
                        Vector2Int index = new Vector2Int(row, col);

                        // kiểm tra config có xếp được ko
                        check = true;
                        foreach (Vector2Int config in bItem.configs)
                        {

                            int xNew = config.x + index.x;
                            int yNew = config.y + index.y;

                            if (xNew < 0 || xNew > 7 || yNew < 0 || yNew > 7)
                            {
                                check = false;
                                break;
                            }
                            else if (board.imgBoards[index.y, index.x].color == Color.white)
                            {
                                check = false;
                                break;
                            }
                            else if (board.imgBoards[yNew, xNew].color == Color.white)
                            {
                                check = false;
                                break;
                            }
                        }
                        if(check)
                        {
                            //Game not over yet
                            return;
                        }
                    }
                }
            }
        }
       //Gameover
        popupPanel.SetActive(true);
       
    }

    /// <summary>
    /// tô lại màu bàn thành xanh, reset điểm 
    /// </summary>
    public void TryAgainBoardtoGreen()
    {
        boxItems.ForEach(a => Destroy(a.gameObject));
        boxItems.Clear();

        CreateBoxItem();
 
        board.BoardtoGreen();
        scoreRs.ScoreReset();              
    }
}

