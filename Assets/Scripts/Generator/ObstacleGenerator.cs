using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private float sendTimer = 0;
    [SerializeField] private float frequency = 5f;
    public GameObject myObstacle;
    public GameObject mainCharacter;
    
    void Update()
    {
        sendTimer -= Time.deltaTime;
        if (sendTimer <= 0)
        {
            createObstacle();
            sendTimer = frequency;
        }
        
        if (mainCharacter != null) Time.timeScale = 1;
        else Time.timeScale = 0;
    }

    private void createObstacle()
    {
        var yPos = Random.Range(50f, 60f);
        transform.position = new Vector3(0, yPos, 40);
        Instantiate(myObstacle, transform.position, transform.rotation);
    }
}
