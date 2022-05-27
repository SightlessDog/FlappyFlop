using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject mainCharacter;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed = 30.0f;
    [SerializeField] private float zRot = 0.0f;
    [SerializeField] private bool spinMode;

    private bool started;
    private bool paused;

    private void Awake()
    {
        spinMode = false;
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

    // To make sure that the camera movement is gonna happen last
    void LateUpdate()
    {
        if (mainCharacter != null)
        {
            if (spinMode && started && !paused)
            {
                SetZRotation();
            }
            SetCamPosition();
        }
    }

    /// <summary>
    /// Set camera position based on the character current position
    /// </summary>
    private void SetCamPosition()
    {
        Vector3 desiredPosition = mainCharacter.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(mainCharacter.transform);
        transform.Rotate(0, 0, zRot);
    }

    /// <summary>
    /// Adjust z rotation during the time 
    /// </summary>
    private void SetZRotation()
    {
        if (zRot > 360f)
        {
            zRot = 0f;
        }

        float delta = Time.deltaTime * speed;
        zRot += delta;
    }

    /// <summary>
    /// Toggle Spin mode via Configuration
    /// </summary>
    public void ToggleSpinMode()
    {
        spinMode = !spinMode;
    }
    
    /// <summary>
    /// get current game state
    /// </summary>
    /// <param name="state"></param>
    private void GameManagerOnGameStateChanged(State state)
    {
        started = state == State.PLAY;
        paused = state == State.PAUSE;
    }
}