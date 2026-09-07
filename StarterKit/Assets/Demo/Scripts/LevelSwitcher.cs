using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField]
    List<string> _scenesFullSwitch = new List<string>();

    [SerializeField]
    List<string> _scenesAdditive = new List<string>();

    private static int _currentSceneIndex = 0;

    private static int _nextAdditiveSceneIndex = 0;

    private bool _loadFullTriggered = false;
    private bool _loadAdditiveTriggered = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Mox.Sync.Input.KeyDown(Mox.Sync.Input.KEYCODE.N)
            && _loadFullTriggered == false)
        {
            if (_scenesFullSwitch.Count > 1)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                int startIndex = _currentSceneIndex;

                do
                {
                    _currentSceneIndex = (_currentSceneIndex + 1) % _scenesFullSwitch.Count;
                }
                while (_scenesFullSwitch[_currentSceneIndex] == currentSceneName && _currentSceneIndex != startIndex);

                Mox.Sync.SceneSyncManager.LoadScene(_scenesFullSwitch[_currentSceneIndex], false);

                _loadFullTriggered = true;
            }
        }
        else if(Mox.Sync.Input.KeyDown(Mox.Sync.Input.KEYCODE.B)
            && _loadAdditiveTriggered == false)
        {
            if (_scenesAdditive.Count > 0)
            {
                _nextAdditiveSceneIndex = (_nextAdditiveSceneIndex + 1) % _scenesAdditive.Count;

                Mox.Sync.SceneSyncManager.LoadScene(_scenesAdditive[_nextAdditiveSceneIndex], true);

                _loadAdditiveTriggered = true;
            }
        }
        else if(Mox.Sync.Input.KeyDown(Mox.Sync.Input.KEYCODE.B) == false)
        {
            _loadAdditiveTriggered = false;
        }
    }
}
