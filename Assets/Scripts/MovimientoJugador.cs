using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
internal sealed class MovimientoJugador : MonoBehaviour {

    [SerializeField] private float _velocidad;
    [SerializeField] private float _tiempoDeColorizado;

    private CharacterController _controller;
    private Color _colorInicial;
    private Renderer _renderer;

    private void Awake() {
        _controller = GetComponent<CharacterController>();
        _renderer = GetComponent<Renderer>();
        _colorInicial = _renderer.material.color;
    }

    internal void RecibirDaño() {
        StartCoroutine(ColorizarDaño());
    }

    private IEnumerator ColorizarDaño() {
        _renderer.material.color = Color.red;
        yield return new WaitForSecondsRealtime(_tiempoDeColorizado);
        _renderer.material.color = _colorInicial;
    }
    
    private void Update() {
        var inputVertical   = Input.GetAxis("Vertical");
        var inputHorizontal = Input.GetAxis("Horizontal");

        var direcciónX = Vector3.right   * inputHorizontal * _velocidad;
        var direcciónZ = Vector3.forward * inputVertical * _velocidad;

        var dirección = (direcciónX + direcciónZ) * Time.deltaTime;

        _controller.Move(dirección);
    }

}
