using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAI : MonoBehaviour
{

    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }
    public static Vector3 GetRandomDir
        return new Vector3(Unityengine.Random.range(-1f,1f ),UnityEngine.Random.range(-1f,1f))normalized;

    private Vector3 GetRoamingPosition()
    {

        return startingPosition + GetRandomDir() * Random.Range(10F, 70f);
    }
}