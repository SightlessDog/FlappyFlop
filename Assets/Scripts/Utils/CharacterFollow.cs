using UnityEngine;

public class CharacterFollow : MonoBehaviour
{
    public GameObject mainCharacter;
    
    void Update()
    {
        transform.position = mainCharacter.transform.position;
    }
}