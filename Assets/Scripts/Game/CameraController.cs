using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private ControllerKeys playerController;
    public float leftLimit, rightLimit, upLimit, downLimit;

    private Vector3 posicao, player;

    void Start()
    {
        playerController = FindObjectOfType<ControllerKeys>();
    }

    void LateUpdate()
    {
        player = playerController.LoadPlayerPosition();
        if (player.x > leftLimit)
        {
            if (player.x < rightLimit)
                posicao.x = player.x;
            else
                posicao.x = rightLimit;
        }
        else
            posicao.x = leftLimit;

        if (player.y > downLimit)
        {
            if (player.y < upLimit)
                posicao.y = player.y;
            else
                posicao.y = upLimit;
        }
        else
            posicao.y = downLimit;

        posicao.z = -10;
        transform.position = posicao;
    }
}