using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float leftLimit, rightLimit, upLimit, downLimit;

    private Vector3 posicao; 
    
    void Start()
    {
    }

    void LateUpdate()
    {
        if (player.transform.position.x > leftLimit)
        {
            if (player.transform.position.x < rightLimit)
                posicao.x = player.transform.position.x;
            else
                posicao.x = rightLimit;
        }
        else
            posicao.x = leftLimit;

        if (player.transform.position.y > downLimit)
        {
            if (player.transform.position.y < upLimit)
                posicao.y = player.transform.position.y;
            else
                posicao.y = upLimit;
        }
        else
            posicao.y = downLimit;

        posicao.z = player.transform.position.z;
        transform.position = posicao;
    }
}