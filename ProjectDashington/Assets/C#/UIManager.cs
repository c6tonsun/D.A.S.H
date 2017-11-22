using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {

    private GameManager _gameManager;
    private MenuHeader[] _menuHeaders;
    public Text _worldText;
    
    public Text enemyText;
    public Text dashText;
    public Text starText;
    private int _enemyCount;
    private int _dashCount;
    private int _starCount;

    public Text endScreenText;

    private void Start()
    {
        if (FindObjectsOfType<UIManager>().Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _gameManager = FindObjectOfType<GameManager>();
        _menuHeaders = transform.GetComponentsInChildren<MenuHeader>(true);

        DontDestroyOnLoad(FindObjectOfType<EventSystem>().gameObject);

        UpdateMenu();
    }

    // menu

    public void UpdateMenu()
    {
        foreach (MenuHeader menuHeader in _menuHeaders)
        {
            menuHeader.gameObject.SetActive(false);
        }

        foreach (MenuHeader menuHeader in _menuHeaders)
        {
            if (menuHeader.gameObject.name.Contains(_gameManager.menuMode))
            {
                menuHeader.gameObject.SetActive(true);
                break;
            }
        }

        _worldText.text = "world " + _gameManager.world;
    }

    // game and level
    
    public void StartLevel()
    {
        ResetGameUIValues();
    }

    public void PauseLevel()
    {

    }

    public void EndLevel()
    {
        _gameManager.SetMenuMode(GameManager.END_UI);
        UpdateMenu();
    }

    private void ResetGameUIValues()
    {
        _enemyCount = GameObject.FindGameObjectsWithTag(GameManager.ENEMY_TAG).Length;
        _dashCount = 0;
        _starCount = _gameManager.GetStarValue();

        UpdateGameUI();
    }

    private void UpdateGameUI()
    {
        enemyText.text = ": " + _enemyCount.ToString();
        dashText.text = ": " + _dashCount.ToString();
        starText.text = _starCount.ToString();
    }

    public void DecreaseEnemyCount()
    {
        _enemyCount--;
        UpdateGameUI();

        if (_enemyCount <= 0)
        {
            PlayerWon();
        }
    }

    public void IncreaseDashCount()
    {
        _dashCount++;
        UpdateGameUI();
    }

    public void PlayerLost(GameObject killer)
    {
        endScreenText.text = "You died to " + killer.tag;
        EndLevel();
    }

    private void PlayerWon()
    {
        if (_dashCount <= _starCount)
        {
            endScreenText.text = "You are the best!";
        }
        else
        {
            endScreenText.text = "You win!";
        }

        EndLevel();
    }
}
