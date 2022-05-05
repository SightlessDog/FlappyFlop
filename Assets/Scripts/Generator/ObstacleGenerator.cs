using DefaultNamespace;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private float sendTimer = 4;
    private float frequency = 4;
    public GameObject myObstacle;
    public GameObject mainCharacter;
    
    [SerializeField] private PlayingLineType playingLineType;
    void Update()
    {
        sendTimer -= Time.deltaTime;
        if (sendTimer <= 0)
        {
            createObstacle(PlayingLineType.CENTER);
            sendTimer = frequency;
        }
        
        if (mainCharacter != null) Time.timeScale = 1;
        else Time.timeScale = 0;
    }

    private void createObstacle(PlayingLineType pos)
    {
        var yPos = Random.Range(50f, 60f);
        switch (pos)
        {
            case PlayingLineType.LEFT :
                transform.position = new Vector3(30, yPos, 40);
                break;
            case PlayingLineType.CENTER:
                transform.position = new Vector3(0, yPos, 40);
                break;
            case PlayingLineType.RIGHT:
                transform.position = new Vector3(-30, yPos, 40);
                break;
        }
        
        Instantiate(myObstacle, transform.position, transform.rotation);
    }
}
