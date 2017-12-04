using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private GameManager _gameManager;
    private MenuHeader[] _menuHeaders;

    public Text _worldText;
    public GameObject worldButtonParent;
    private Button[] _worldButtons;
    public GameObject levelButtonParent;
    private Button[] _levelButtons;
    
    public Text enemyText;
    public Text dashText;
    public Text starText;
    private int _enemyCount;
    private int _dashCount;
    private int _starCount;

    public GameObject winStar;
    public GameObject winNoStar;

    public GameObject muteButton;
    public GameObject unmuteButton;

    public Animator starAnimator;

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

        _worldButtons = worldButtonParent.GetComponentsInChildren<Button>();
        _levelButtons = levelButtonParent.GetComponentsInChildren<Button>();

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

        SelectionButtonsFromSaveFile();
    }

    public void SelectionButtonsFromSaveFile()
    {
        foreach (Button button in _levelButtons)
        {
            button.transform.GetChild(1).gameObject.SetActive(false);
            button.transform.GetChild(0).gameObject.SetActive(false);

            button.interactable = false;
            button.gameObject.SetActive(false);
        }

        int[,] savefile = _gameManager.GetSaveFile();

        // world selection
        for (int i = 0; i < savefile.Length / 4; i++)
        {
            if (savefile[i, SaveLoad.LEVEL] == 0)
            {
                int index = savefile[i, SaveLoad.WORLD] - 1;
                _worldButtons[index].interactable = false;
                _worldButtons[index].transform.GetChild(1).gameObject.SetActive(false);

                if (savefile[i, SaveLoad.OPEN] == SaveLoad.TRUE)
                {
                    _worldButtons[index].interactable = true;
                }

                if (savefile[i, SaveLoad.STAR] == SaveLoad.TRUE)
                {
                    _worldButtons[index].transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }

        // level selection
        for (int i = 0; i < savefile.Length / 4; i++)
        {
            if (savefile[i, SaveLoad.WORLD] == _gameManager.world &&
                savefile[i, SaveLoad.LEVEL] != 0)
            {
                int index = savefile[i, SaveLoad.LEVEL] - 1;
                _levelButtons[index].gameObject.SetActive(true);

                if (savefile[i, SaveLoad.OPEN] == SaveLoad.TRUE)
                {
                    _levelButtons[index].interactable = true;
                    _levelButtons[index].transform.GetChild(0).gameObject.SetActive(true);
                }

                if (savefile[i, SaveLoad.STAR] == SaveLoad.TRUE)
                {
                    _levelButtons[index].transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
    }

    // game and level
    
    public void StartLevel()
    {
        ResetGameUIValues();
    }

    public void PauseLevel()
    {
        _gameManager.OnApplicationPause(true);
    }

    public void UnpauseLevel()
    {
        _gameManager.OnApplicationPause(false);
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
        _gameManager.menuMode = GameManager.LOSE_UI;
        UpdateMenu();
    }

    private void PlayerWon()
    {
        _gameManager.menuMode = GameManager.WIN_UI;
        UpdateMenu();

        winStar.SetActive(false);
        winNoStar.SetActive(false);
        
        if (_gameManager.level == _gameManager.GetCurrentLevelCount())
        {
            if (_gameManager.world < 3)
            {
                _gameManager.OpenWorldLevel(_gameManager.world + 1, 0);
                _gameManager.OpenWorldLevel(_gameManager.world + 1, 1);
            }
        }
        else
        {
            _gameManager.OpenWorldLevel(_gameManager.world, _gameManager.level + 1);
        }

        if (_dashCount <= _starCount)
        {
            winStar.SetActive(true);
            _gameManager.StarWorldLevel(_gameManager.world, _gameManager.level);

            // reset animation
            starAnimator.runtimeAnimatorController =
                Resources.Load("Star") as RuntimeAnimatorController;
            starAnimator.Play("Star animation", -1, 0f);
        }
        else
        {
            winNoStar.SetActive(true);
        }
    }
}
