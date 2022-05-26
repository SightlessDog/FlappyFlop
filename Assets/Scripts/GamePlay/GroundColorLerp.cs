using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColorLerp : MonoBehaviour
{
    MeshRenderer renderer;
    [SerializeField] [Range(0f, 1f)] private float lerpTime;
    [SerializeField] private Color[] colors;
    private int colorIndex = 0;
    private float t = 0f;
    
    // TODO this component is very very very weird, it doesn't get applied to the renderer
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.material.color = Color.Lerp(renderer.material.color, colors[colorIndex], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = colorIndex >= colors.Length ? 0 : colorIndex;
        }
    }
}
