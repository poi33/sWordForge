using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public AnimationCurve curve;
    public float shakeTime;
    public float magnitude;
    public float frequency;

    private float elapsedTime = 0;
    private Vector3 startPos;
    bool shake = false;

    private void Start()
    {
        startPos = Camera.main.transform.position;
    }

    void Update()
    {
        if (shake)
        {
            Vector2 newPos = (Vector2)startPos + (PerlinShake(magnitude, frequency) * curve.Evaluate(shakeTime - elapsedTime));
            Camera.main.transform.position = new Vector3(newPos.x, newPos.y, startPos.z);
            elapsedTime += Time.deltaTime;
            //Debug.Log(elapsedTime);
            if (elapsedTime >= shakeTime)
            {
                shake = false;
                transform.position = startPos;
                elapsedTime = 0;
            }
        }
    }

    //Cant handle multiple shakes at the moment.
    public void ShakeScreen(float time)
    {
        if (shake == false) { 
            shake = true;
            shakeTime = time;
        }
    }

    public static Vector2 PerlinShake(float magnitude, float frequency)
    {
        Vector2 result;
        float seed = Time.time * frequency;
        result.x = Mathf.Clamp01(Mathf.PerlinNoise(seed, 0f)) - 0.5f;
        result.y = Mathf.Clamp01(Mathf.PerlinNoise(0f, seed)) - 0.5f;
        result = result * magnitude;
        return result;
    }
}
