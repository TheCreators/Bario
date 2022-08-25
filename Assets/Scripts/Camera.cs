using Cinemachine;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private Player _player;

    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _player = FindObjectOfType<Player>();

        TrackPlayer();
    }

    private void TrackPlayer()
    {
        _camera.Follow = _player.transform;
    }
}