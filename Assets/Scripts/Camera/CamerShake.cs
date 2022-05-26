using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerShake : MonoBehaviour
{
    // Use coroutine to make the animation run over multiple frames and not block the calling method
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 origPosition = transform.localPosition;
        
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = Vector3.Lerp(transform.localPosition,new Vector3(x, y, origPosition.z), 0.125f) ;

            elapsed = Time.deltaTime;

            yield return null;
        }

        transform.localPosition = origPosition;
    }
}
