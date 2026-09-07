using System;
using UnityEngine;

public class UserDataSyncDemo : Mox.SyncBehaviour
{
    [Serializable]
    struct MyUserData
    {
        [SerializeField]
        public Vector3 Color;
    }

    private MyUserData _userData = new MyUserData();

    private float _colorAngle = 0.0f;
    private float _colorRotationSpeed = 10.0f;

    private MeshRenderer _meshRenderer = null;

    public void Start()
    {
        _userData.Color = new Vector3(1.0f, 0.0f, 0.0f);
    }

    public void Update()
    {
        // Debug.Log("A: " + _userData.Color);

        if(_meshRenderer != null)
        {
            Material mat = _meshRenderer.material;

            Color color = new Color(_userData.Color.x, _userData.Color.y, _userData.Color.z);

            mat.SetColor("_Color", color);
        }

        if(Mox.Sync.SceneSyncManager.IsPrimary)
        {
            // _userData.Color = Quaternion.AngleAxis((_colorAngle / 180.0f * 3.14159f), new Vector3(0.0f, 1.0f, 0.0f)) * _userData.Color;
            _userData.Color = Quaternion.AngleAxis(_colorAngle, new Vector3(0.0f, 1.0f, 0.0f)) * new Vector3(1.0f, 0.0f, 0.0f);

            // Debug.Log("B: " + _userData.Color);

            _colorAngle += _colorRotationSpeed * Time.deltaTime;

            if (_colorAngle > 360.0f)
            {
                _colorAngle -= 360.0f;
            }
        }
    }

    override protected object GetUserData()
    {
        // Debug.Log("A: " + _userData.Color);

        return _userData;
    }

    override protected System.Type GetUserDataType()
    {
        return typeof(MyUserData);
    }

    override protected void SetUserData(object userData)
    {
        // Debug.Log("B: " + _userData.Color);

        _userData = (MyUserData)userData;

        //if(_userData.Color.sqrMagnitude <= 0.0f)
        //{
        //    int foo = 0;
        //}

        // Debug.Log("C: " + _userData.Color);
    }

    override protected void SyncInitDone()
    {
        _meshRenderer = PresentationObject.GetComponent<MeshRenderer>();
    }
}
