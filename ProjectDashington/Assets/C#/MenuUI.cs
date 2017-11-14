using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour {

    private GameManager _gameManager;
    private MenuHeader[] _menuHeaders;
    private Text _worldText;

    private void Start()
    {
        if (FindObjectsOfType<MenuUI>().Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _gameManager = FindObjectOfType<GameManager>();
        _menuHeaders = transform.GetComponentsInChildren<MenuHeader>(true);

        Text[] texts = transform.GetComponentsInChildren<Text>(true);
        foreach (Text text in texts)
        {
            if (text.name.Contains("World"))
            {
                _worldText = text;
                break;
            }
        }

        UpdateMenu();
    }

    private void Update()
    {
        
    }

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
}
