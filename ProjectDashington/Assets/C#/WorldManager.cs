﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldManager : MonoBehaviour {

    // World UI
    private Canvas _canvas;
    private Text _dashText;
    private Text _parText;
    private Text _enemyText;
    private Text _levelResultText;
    // UI variables
    private int _dashCount;
    private int _parCount;
    private int _enemyCount;
    private string _playerKiller;

    [SerializeField]
    private float _readTime;

    private Level[] _levels;
    private Level _currentLevel;
    private int _levelNumber = 1;
    private int _maxLevelNumber;

    public const string TAG_PLAYER = "Player";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_SHIELD= "Shield";
    public const string TAG_LAVA = "Lava";

    private void Awake()
    {
        // DontDestroyOnLoad(gameObject);
        
        InitializeUI();
        InGameUI(true);
        LevelResultUI(false);
    }

    private void Start()
    {
        InitializeLevels();
        FindNextLevel();
        ActivateLevel();
        
        ResetUIValues();
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
        _maxLevelNumber = _levels.Length;
    }

    private void FindNextLevel()
    {
        if (_levelNumber > _maxLevelNumber)
        {
            _levelNumber = 1;
        }

        foreach (Level level in _levels)
        {
            if (level.GetLevelNumber() == _levelNumber)
            {
                _currentLevel = level;
            }
        }
    }

    private void ActivateLevel()
    {
        DeactivateAllLevels();
        _currentLevel.gameObject.SetActive(true);

    }

    private void DeactivateAllLevels()
    {
        foreach (Level level in _levels)
        {
            level.gameObject.SetActive(false);
        }
    }

    IEnumerator EndLevel(bool win)
    {
        if (win)
        {
            _levelResultText.text = string.Concat("You win.\n", "Loading next level.");
        }
        else
        {
            _levelResultText.text = string.Concat("You died to\n", _playerKiller);
        }

        LevelResultUI(true);

        yield return new WaitForSeconds(_readTime);

        if (win)
        {
            _levelNumber++;
            FindNextLevel();
        }

        ActivateLevel();

        LevelResultUI(false);
        ResetUIValues();
    }

    // UI methods.

    private void InitializeUI()
    {
        _canvas = GetComponentInChildren<Canvas>();

        Text[] texts = _canvas.GetComponentsInChildren<Text>();
        foreach (Text text in texts)
        {
            if (text.name.Contains("Dash"))
            {
                _dashText = text;
            }
            else if (text.gameObject.name.Contains("Par"))
            {
                _parText = text;
            }
            else if (text.gameObject.name.Contains("Enem"))
            {
                _enemyText = text;
            }
            else if (text.gameObject.name.Contains("Level"))
            {
                _levelResultText = text;
            }
        }
    }

    private void InGameUI(bool enable)
    {
        _dashText.enabled = enable;
        _parText.enabled = enable;
        _enemyText.enabled = enable;
    }

    private void LevelResultUI(bool enable)
    {
        _levelResultText.enabled = enable;
    }

    private void ResetUIValues()
    {
        _dashCount = 0;
        _parCount = _currentLevel.GetParValue();
        _enemyCount = GameObject.FindGameObjectsWithTag(TAG_ENEMY).Length;
        UpdateInGameUI();
    }

    private void UpdateInGameUI()
    {
        _dashText.text = string.Concat("Dash : " + _dashCount.ToString());
        _parText.text = string.Concat("Par : " + _parCount.ToString());
        _enemyText.text = string.Concat("Enemies : " + _enemyCount.ToString());
    }

    // Public UI methods.

    public void IncreaseDashCount()
    {
        _dashCount++;
        UpdateInGameUI();
    }

    public void DecreaseEnemyCount()
    {
        _enemyCount--;
        UpdateInGameUI();

        if (_enemyCount <= 0)
        {
            if (GameObject.FindGameObjectWithTag(TAG_ENEMY) != null)
            {
                StartCoroutine(EndLevel(true));
            }
        }
    }

    public void SetPlayerKiller(GameObject killer)
    {
        _playerKiller = killer.tag.ToString();
        StartCoroutine(EndLevel(false));
    }
}