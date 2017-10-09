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
    private Text _enemyCountText;
    [SerializeField]
    private Text _deathInfo;
    // Changing values on UI
    private int _dashCount;
    private int _enemyCount;

    [SerializeField]
    private float _deathDisplayTime;
    private bool _deathDisplay = false;

    [SerializeField]
    private GameObject _player;
    private Health _playerHealth;

	// Use this for initialization
	void Start () {
        _playerHealth = _player.GetComponent<Health>();
        _enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateUI();
	}
	
	// Update is called once per frame
	void Update () {
        // Player lose.
		if (_playerHealth.GetIsDead() && !_deathDisplay)
        {
            StartCoroutine(DeathDisplay(false));
            _deathDisplay = true;
        }
        
        // Player win.
        if (_enemyCount <= 0)
        {
            StartCoroutine(DeathDisplay(true));
        }
	}

    IEnumerator DeathDisplay(bool win)
    {
        _deathInfo.gameObject.SetActive(true);

        if (win)
        {
            _deathInfo.text = "You win!";
        }
        else
        {
            string killerName = _playerHealth.GetKiller().name;
            _deathInfo.text = string.Concat(_deathInfo.text, "\n", FormatKillerName(killerName));
        }

        yield return new WaitForSeconds(_deathDisplayTime);

        ReloadThisScene();
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

    private void ReloadThisScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Public UI methods.

    public void IncreaseDashCount(int amount)
    {
        _dashCount += amount;
    }

    public void DecreaseEnemyCount(int amount)
    {
        _enemyCount -= amount;
    }

    public void UpdateUI()
    {
        _dashCountText.text = string.Concat("Dash : ", _dashCount.ToString());
        _enemyCountText.text = string.Concat("Enemies : ", _enemyCount.ToString());
    }
}
