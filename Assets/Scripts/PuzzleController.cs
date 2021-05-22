using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
    [SerializeField]
    private Sprite DefSprite;

    [SerializeField]
    public static TMP_Text VictoryText, BestScore, Score;

    public Image DefStar;

    public Sprite CorrectAnswerSprite;

    public Sprite[] Sprites;

    public GameObject WinScr;

    

    public GameObject[] Stars;

    public List<Sprite> TileSprites = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    public static int CurretScore;

    private bool FirstTile, SecondTile;
    private int SelectedTileCount;
    private int CorrectGuesesCount;
    private int CorrectAnswerCount;
    private int GuesesCount;
    

    private int FirstTileIndex, SecondTileIndex;

    private string FirstTileName, SecondTileName;
    private void Awake()
    {
        Sprites = Resources.LoadAll<Sprite>("Ventspils");
    }
    private void Start()
    {
        Getbuttons();
        AddListeners();
        AddSprites();
        SpriteShuffle(TileSprites);
        WinScr.SetActive(false);


        CurretScore = GuesesCount * 100;
        CorrectAnswerCount = btns.Count / 2;
    }

    public void PickAPuzzle()
    {
        string TileIndex = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("Button Nr: " + TileIndex);

        if (!FirstTile)
        {
            FirstTile = true;

            FirstTileIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            FirstTileName = TileSprites[FirstTileIndex].name;

            btns[FirstTileIndex].image.sprite = TileSprites[FirstTileIndex];

            btns[FirstTileIndex].interactable = false;
        }
        else if (!SecondTile)
        {
            SecondTile = true;

            SecondTileIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            SecondTileName = TileSprites[SecondTileIndex].name;

            btns[SecondTileIndex].image.sprite = TileSprites[SecondTileIndex];

            btns[SecondTileIndex].interactable = false;

            GuesesCount++;

            StartCoroutine(MatchChecker());

        }
    }

    IEnumerator MatchChecker()
    {
        yield return new WaitForSeconds(1f);

        if (btns[FirstTileIndex].image.sprite == btns[SecondTileIndex].image.sprite)
        {
            yield return new WaitForSeconds(.5f);

            btns[FirstTileIndex].interactable = false;
            btns[SecondTileIndex].interactable = false;

            btns[FirstTileIndex].image.sprite = CorrectAnswerSprite;
            btns[SecondTileIndex].image.sprite = CorrectAnswerSprite;

            GameFinishing();
        }
        else
        {
            btns[FirstTileIndex].image.sprite = DefSprite;
            btns[SecondTileIndex].image.sprite = DefSprite;

            btns[FirstTileIndex].interactable = true;
            btns[SecondTileIndex].interactable = true;
        }

        yield return new WaitForSeconds(.5f);

        FirstTile = SecondTile = false;


    }

    void SpriteShuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int RandNum = Random.Range(0, list.Count);
            list[i] = list[RandNum];
            list[RandNum] = temp;
        }
    }

    void GameFinishing()
    {
        CorrectGuesesCount++;
        if (CorrectGuesesCount == CorrectAnswerCount)
        {

            PuzzleSaveController.ScoreUpdate();
            Score.text = CurretScore.ToString();
            WinScr.gameObject.SetActive(true);

            if (GuesesCount <= 16)
            {
                foreach (GameObject gm in Stars)
                {
                    gameObject.SetActive(true);
                }

                VictoryText.text = "Awesome";
            }
            else if (GuesesCount <= 20)
            {
                VictoryText.text = "Great";
                Stars[0].SetActive(true);
                Stars[1].SetActive(true);
            }
            else
            {
                VictoryText.text = "Not bad";
                Stars[0].SetActive(true);
            }


        }

        
    }

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }
    void Getbuttons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleTile");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = DefSprite;
        }
    }

    void AddSprites()
    {
        int loop = btns.Count;
        int index = 0;
        for (int i = 0; i < loop; i++)
        {
            if (index == loop / 2)
            {
                index = 0;
            }

            TileSprites.Add(Sprites[index]);
            index++;
        }
    }
}
