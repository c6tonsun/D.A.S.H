using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour {

    [SerializeField]
    private Object _playerPrefab;
    [SerializeField]
    private Object _enemyPrefab;

    [SerializeField]
    private int _widthFrom0;
    [SerializeField]
    private int _heightFrom0;

    private int _playerX;
    private int _playerY;

    
    private void Start () {

        // Check prefabs
		if(_enemyPrefab == null || _playerPrefab == null)
        {
            Debug.LogError("LevelStarter does not have prefabs.");
            return;
        }

        LotsOfEnemies();
	}

    // Makes enemies and random spot for player to spawn.
    private void LotsOfEnemies()
    {
        // select random spot for player
        _playerX = Random.Range(-_widthFrom0, _widthFrom0);
        _playerY = Random.Range(-_heightFrom0, _heightFrom0);

        // make player and enemies
        for (int x = -_widthFrom0; x <= _widthFrom0; x++)
        {
            for (int y = -_heightFrom0; y <= _heightFrom0; y++)
            {
                if (x == _playerX && y == _playerY)
                {
                    Instantiate(_playerPrefab, new Vector3(x, y), Quaternion.identity);
                }
                else
                {
                    Instantiate(_enemyPrefab, new Vector3(x, y), Quaternion.identity);
                }
            }
        }
    }
}
