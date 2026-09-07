using Unity.VisualScripting;
using UnityEngine;

public class CircleDrone : Mox.SyncBehaviour
{
    [SerializeField]
    private Vector3 _center = Vector3.zero;

    [SerializeField]
    private float _radius = 5.0f;

    float _t = 0.0f;

    new void Start()
    {
        Debug.Log("circle drone: " + SyncId);

        int foo = 0;
    }

    new void Update()
    {
        _t += Time.deltaTime;

        float x = _center.x + _radius * Mathf.Cos(_t);
        float z = _center.z + _radius * Mathf.Sin(_t);
        float y = _center.y;

        transform.position = new Vector3(x, y, z);
    }

    protected new void OnDestroy()
    {
        base.OnDestroy();
    }
}
