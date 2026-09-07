using System;
using UnityEngine;

public class SyncTestObject : Mox.SyncBehaviour
{
    [Serializable]
    private struct ImportantSyncData
    {
        [SerializeField]
        public int foobar;
        [SerializeField]
        public float foobarSpeed;
        [SerializeField]
        public float foobarTime;
        [SerializeField]
        public string foobarName;
    }

    private ImportantSyncData _syncData = new ImportantSyncData();

    private MeshRenderer _meshRenderer = null;

    [SerializeField]
    private float cubeSpeed = 100;
    [SerializeField]
    private float cubeDistance = 10.0f;

    private Vector3 _startPosition = Vector3.zero;

    float _t = 0.0f;

    void Start()
    {
        _syncData.foobar = 0;
        _syncData.foobarSpeed = 1.0f;
        _syncData.foobarTime = 66.6f;
        _syncData.foobarName = "Bob";

        _startPosition = transform.position;
    }

    void Update()
    {
        _t += Time.deltaTime * cubeSpeed;
        transform.position = _startPosition + (Vector3.up * Mathf.Sin(_t) * cubeDistance);
    }

    override protected object GetUserData()
    {
        return _syncData;
    }

    override protected System.Type GetUserDataType()
    {
        return typeof(ImportantSyncData);
    }

    override protected void SetUserData(object userData)
    {
        ImportantSyncData syncData = (ImportantSyncData)userData;
    }

    override protected void SyncInitDone()
    {
        _meshRenderer = PresentationObject.GetComponent<MeshRenderer>();
    }
}
