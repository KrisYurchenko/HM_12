using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventController : MonoBehaviour
{
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private MovingPlatform _platform;

    private Renderer _renderer;

    private bool _isMoving;

    private void Start() {
        _renderer = gameObject.GetComponentInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isMoving) return;
        if (other.gameObject.tag != _playerTag) return;

        SetButtonColor(Color.red);
        StartCoroutine(Moving());
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isMoving) return;
        if (other.gameObject.tag != _playerTag) return;

        SetButtonColor(Color.blue);
        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        _isMoving = true;
        yield return new WaitForSeconds(0.3f);
        _isMoving = false;
    }

    private void OnTriggerStay(Collider other)
    {
        _platform.Move();
    }

    private void SetButtonColor(Color color)
    {
        _renderer.material.SetColor("_Color", color);
    }
}
