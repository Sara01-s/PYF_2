using UnityEngine;

[RequireComponent(typeof(CharacterController))]
internal sealed class PlayerMovimiento : MonoBehaviour {

    [SerializeField] private float _velocidad;

    private CharacterController _controller;

    private void Awake() {
        _controller = GetComponent<CharacterController>();
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
