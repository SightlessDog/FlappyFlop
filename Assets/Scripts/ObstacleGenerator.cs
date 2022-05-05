using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private float sendTimer = 4;
    private float frequency = 4;
    public GameObject myObstacle;
    public GameObject mainCharacter;

    enum ObtaclePosition
    {
        LEFT,
        CENTER,
        RIGHT
    }
    void Update()
    {
        sendTimer -= Time.deltaTime;
        if (sendTimer <= 0)
        {
            createObstacle(ObtaclePosition.LEFT);
            createObstacle(ObtaclePosition.CENTER);
            createObstacle(ObtaclePosition.RIGHT);
            sendTimer = frequency;
        }
        
        if (mainCharacter != null) Time.timeScale = 1;
        else Time.timeScale = 0;
    }

    private void createObstacle(ObtaclePosition pos)
    {
        var yPos = Random.Range(50f, 60f);
        switch (pos)
        {
            case ObtaclePosition.LEFT :
                transform.position = new Vector3(30, yPos, 70);
                break;
            case ObtaclePosition.CENTER:
                transform.position = new Vector3(0, yPos, 70);
                break;
            case ObtaclePosition.RIGHT:
                transform.position = new Vector3(-30, yPos, 70);
                break;
        }
        
        Instantiate(myObstacle, transform.position, transform.rotation);
    }
}
