using Klak.Spout;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _spherePrefab;

    private bool _spawnCommanded = false;
    private bool _deleteCommanded = false;

    private Queue<string> _sphereIds = new Queue<string>();

    private Mutex _idsMutex = new Mutex();

    void Start()
    {
        
    }

    void Update()
    {
        if(Mox.Sync.Input.KeyDown(Mox.Sync.Input.KEYCODE.S))
        {
            if(_spawnCommanded == false)
            {
                Mox.Sync.SceneSyncManager.InstantiatePrefab(_spherePrefab, InstantiationCallback);

                _spawnCommanded = true;
            }
        }
        else if(Mox.Sync.Input.KeyDown(Mox.Sync.Input.KEYCODE.D))
        {
            if(_deleteCommanded == false)
            {
                string id = _sphereIds.Dequeue();

                Mox.Sync.SceneSyncManager.RemoveSyncObject(id);

                _deleteCommanded = true;
            }
        }
        else if(Mox.Sync.Input.KeyDown(Mox.Sync.Input.KEYCODE.A))
        {
            // MoxAudio.SendObjectWidth(1, 1.0f);

            Mox.Sync.Audio.SendObjectPositionXYZ(1, new Vector3(1.0f, 2.0f, 3.0f));
        }
        else
        {
            _spawnCommanded = false;
            _deleteCommanded = false;
        }
    }

    void InstantiationCallback(Mox.SyncBehaviour instantiatedObject)
    {
        instantiatedObject.transform.position = transform.position;

        Debug.Log("spawned: " + instantiatedObject.SyncId);

        {
            lock (_idsMutex)
            {
                _sphereIds.Enqueue(instantiatedObject.SyncId);
            }
        }
    }
}
