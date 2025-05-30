using UnityEngine;

public class GunAimControl : MonoBehaviour
{
    [SerializeField] Transform gunFirepoint;
    [SerializeField] float rayDistance = 10f;
    private IDamageable nullDmageable= new BoatParts();

    [Header("References")]
    public Joystick joystick;           // Joystick for input
    public Transform gunPivot;          // The pivot (gun base or holder)

    [Header("Rotation Settings")]
    public float rotationSpeed = 100f;

    [Header("Field of View Limits")]
    public float maxYaw = 60f;   // Y axis (horizontal rotation limit)
    public float maxTilt = 45f;  // Z axis (up/down tilt limit)

    private Vector2 currentRotation; // Y = yaw, Z = tilt

    void Update()
    {

        JoysticInput();

        CheckForDamageable();
    }

    private void JoysticInput()
    {
        if (joystick == null || gunPivot == null)
            return;

        Vector2 input = joystick.Direction;

        // Calculate rotation deltas
        float yaw = input.x * rotationSpeed * Time.deltaTime;    // Y axis
        float tilt = -input.y * rotationSpeed * Time.deltaTime;  // Z axis (inverted)

        // Apply changes to current rotation
        currentRotation.x += yaw;  // Yaw around Y
        currentRotation.y += tilt; // Tilt around Z

        // Clamp rotation
        currentRotation.x = Mathf.Clamp(currentRotation.x, -maxYaw, maxYaw);     // Yaw
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxTilt, maxTilt);   // Tilt

        // Apply rotation: Yaw (Y axis), Tilt (Z axis)
        gunPivot.localRotation = Quaternion.Euler(0f, currentRotation.x, currentRotation.y);
    }

    

    private void CheckForDamageable()
    {
        if (gunFirepoint == null) return;

        Ray ray = new Ray(gunFirepoint.position, gunFirepoint.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, ~_GameAssets.Instance.gunAimIgnoreLayermask))
        {
            var damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                _GameAssets.Instance.OnGunAimAtAction?.Invoke(damageable);
            }
            else
            {
                _GameAssets.Instance.OnGunAimAtAction?.Invoke(nullDmageable);
            }
        }
        else
        {
            _GameAssets.Instance.OnGunAimAtAction?.Invoke(nullDmageable);
        }
    }
}
