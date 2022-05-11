using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Env")
        {
            Destroy(gameObject);
            Debug.Log("Collision and object destroyed");
            GameManager.Instance.UpdateGameState(State.GAMEOVER);
        }
    }
}
