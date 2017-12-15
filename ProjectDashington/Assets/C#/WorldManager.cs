using UnityEngine;

public class WorldManager : MonoBehaviour {

    // Level stuff
    private Level[] _levels;
    private Level _currentLevel;
    private int _maxLevelNumber;

    private GameManager _gameManager;
    private AudioSource _levelStart;

    public GameObject POW;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.SetWorldManager(this);
        _levelStart = GetComponent<AudioSource>();

        InitializeLevels();
        FindNextLevel();
        ActivateLevel();

        _gameManager.InitializeGameUI();
        _gameManager.FindCamera();
    }

    // Level methods

    private void InitializeLevels()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            child.gameObject.SetActive(true);
        }

        _levels = GetComponentsInChildren<Level>(true);
        _maxLevelNumber = _gameManager.GetCurrentLevelCount();
    }

    private void FindNextLevel()
    {
        foreach (Level level in _levels)
        {
            if (level.GetLevelNumber() == _gameManager.GetLevelValue())
            {
                _currentLevel = level;
            }
        }
    }

    private void ActivateLevel()
    {
        DeactivateAllLevels();
        _currentLevel.gameObject.SetActive(true);

        RemoveProjectiles();

        _gameManager.SetMenuMode(GameManager.GAME_UI);

        Time.timeScale = 1f;
//        _levelStart.volume = Settings.Volume;
 //       _levelStart.Play();
    }

    private void DeactivateAllLevels()
    {
        foreach (Level level in _levels)
        {
            level.gameObject.SetActive(false);
        }
    }

    private void RemoveProjectiles()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject projetctile in projectiles)
        {
            Destroy(projetctile);
        }
    }

    // public methods

    public void RestartLevel()
    {
        if (_gameManager.GetLevelValue() > _maxLevelNumber)
        {
            ReturnToMenu();
            return;
        }

        FindNextLevel();
        ActivateLevel();

        _gameManager.InitializeGameUI();
    }

    public void ReturnToMenu()
    {
        if (_gameManager.world < 3) {
            _gameManager.world++;
            _gameManager.LoadMenu(GameManager.WORLD_MENU);
        }
        else
        {
            _gameManager.LoadMenu(GameManager.CREDIT_MENU);
        }
    }

    // Getters and setters

    public int GetStarValue()
    {
        return _currentLevel.GetStarValue();
    }

    public GameObject GetPOW()
    {
        return POW;
    }
}
