using UnityEngine.Events;
using UnityEngine;

internal sealed class EventoColision : MonoBehaviour {

    [SerializeField] private string _tagObjetivo;
    [SerializeField] private UnityEvent _evento;

    private void OnTriggerEnter(Collider objetivo) {
        if (objetivo.CompareTag(_tagObjetivo)) {
            _evento?.Invoke();
        }
    }

}
