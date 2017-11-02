using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]

public class LevelHandler : MonoBehaviour {

    // UI elements
    [SerializeField]
    private Canvas _UICanvas;
    [SerializeField]
    private Text _dashCountText;
    [SerializeField]
    private Text _parText;
    [SerializeField]
    private int _parCount;
    [SerializeField]
    private Text _enemyCountText;
    [SerializeField]
    private Text _levelEndText;
    // Changing values on UI
    private int _dashCount;
    private int _enemyCount;

    [SerializeField]
    private float _levelEndDisplayTime;
    private bool _levelEndDisplay;

    [SerializeField]
    private GameObject _player;
    private Health _playerHealth;

    [SerializeField]
    private string _nextSceneName;
    
	private void Start () {
        _playerHealth = _player.GetComponent<Health>();
        _enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateUI();
	}
	
	private void Update () {
        CheckLevelEnd();
	}

    private void CheckLevelEnd()
    {
        if (!_levelEndDisplay)
        {
            // Player lose.
            if (_playerHealth.GetIsDead())
            {
                StartCoroutine(LevelEndDisplay(false));
            }

            // Player win.
            if (_enemyCount <= 0)
            {
                StartCoroutine(LevelEndDisplay(true));
            }
        }
    }
    IEnumerator LevelEndDisplay(bool win)
    {
        _levelEndDisplay = true;

        _levelEndText.gameObject.SetActive(true);

        if (win)
        {
            _levelEndText.text = "You win!\n" +
                "Loading next level.";
        }
        else
        {
            string killerName = FormatKillerName(_playerHealth.GetKiller().name);
            _levelEndText.text = string.Concat(_levelEndText.text, "\n", killerName);
        }

        yield return new WaitForSeconds(_levelEndDisplayTime);

        if (win)
        {
            SceneManager.LoadScene(_nextSceneName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private string FormatKillerName(string str)
    {
        string result = "";
        char[] chars = str.ToCharArray();

        foreach (char c in chars)
        {
            if (c == '(')
            {
                break;
            }
            result += c;
        }

        return result;
    }

    // Public UI methods.

    public void IncreaseDashCount(int amount)
    {
        _dashCount += amount;
        UpdateUI();
    }

    public void DecreaseEnemyCount(int amount)
    {
        _enemyCount -= amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _dashCountText.text = string.Concat("Dash : ", _dashCount.ToString());
        _parText.text = string.Concat("Par: ", _parCount.ToString());
        _enemyCountText.text = string.Concat("Enemies : ", _enemyCount.ToString());
    }
}
