using UnityEngine;

public class WorldManager : MonoBehaviour {

    // Level stuff
    private Level[] _levels;
    private Level _currentLevel;
    private int _maxLevelNumber;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.SetWorldManager(this);

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
        _gameManager.LoadMenu(_gameManager.GetLevelMenuString());
    }

    // Getters and setters

    public int GetStarValue()
    {
        return _currentLevel.GetStarValue();
    }
}
