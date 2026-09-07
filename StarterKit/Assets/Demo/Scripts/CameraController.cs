using Mox;
using UnityEngine;

public class CameraController : SyncBehaviour
{
    [SerializeField]
    private float _movementSpeed = 5.0f;

    [SerializeField]
    private float _rotationSpeed = 2800.0f;

    new void Start()
    {
        
    }

    new void Update()
    {
        // keyboard movement
        {
            if (Mox.Sync.Input.KeyDown(Mox.Sync.Input.KEYCODE.W))
            {
                transform.position += transform.forward * Time.deltaTime * _movementSpeed;
            }
            if (Mox.Sync.Input.KeyDown(Mox.Sync.Input.KEYCODE.A))
            {
                transform.position -= transform.right * Time.deltaTime * _movementSpeed;
            }
            if (Mox.Sync.Input.KeyDown(Mox.Sync.Input.KEYCODE.S))
            {
                transform.position -= transform.forward * Time.deltaTime * _movementSpeed;
            }
            if (Mox.Sync.Input.KeyDown(Mox.Sync.Input.KEYCODE.D))
            {
                transform.position += transform.right * Time.deltaTime * _movementSpeed;
            }
            if (Mox.Sync.Input.KeyDown(Mox.Sync.Input.KEYCODE.Q))
            {
                transform.position -= transform.up * Time.deltaTime * _movementSpeed;
            }
            if (Mox.Sync.Input.KeyDown(Mox.Sync.Input.KEYCODE.E))
            {
                transform.position += transform.up * Time.deltaTime * _movementSpeed;
            }
        }

        // mouse rotation
        {
            Vector2 mouseDelta = Mox.Sync.Input.MouseDelta();

            Vector2 delta = mouseDelta;

            transform.Rotate(Vector3.up, mouseDelta.x * Time.deltaTime * _rotationSpeed, Space.World);
            transform.Rotate(Vector3.left, mouseDelta.y * Time.deltaTime * _rotationSpeed, Space.Self);
        }
    }
}
