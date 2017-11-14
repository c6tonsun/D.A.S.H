using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour {

    private GameManager _gameManager;
    private MenuHeader[] _menuHeaders;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        _menuHeaders = transform.GetComponentsInChildren<MenuHeader>(true);

        UpdateMenu();
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
            }
        }
    }
}
