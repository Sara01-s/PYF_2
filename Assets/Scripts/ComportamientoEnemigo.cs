using UnityEngine.AI;
using UnityEngine;

enum Estado {
    Patrullar, Perseguir, Atacar, Huir
}

[RequireComponent(typeof(NavMeshAgent))]
internal sealed class ComportamientoEnemigo : MonoBehaviour {

    [SerializeField] private Transform   _jugador;
    [SerializeField] private Transform[] _puntosDeRuta;

    private float _distanciaMínimaParaAtacar;
    private float _distanciaConJugador;
    private int _iteradorDePuntos;
    private Estado _estadoActual;
    private NavMeshAgent _agente;

    private void Awake() {
        _agente = GetComponent<NavMeshAgent>();
        DirigirseASiguientePunto();
    }

    public void DirigirseASiguientePunto() {
        if (_estadoActual != Estado.Patrullar) return;

        var siguientePosición = _puntosDeRuta[_iteradorDePuntos++ % _puntosDeRuta.Length];
        
        _agente.SetDestination(siguientePosición.position);
        Debug.Log($"Siguiente punto enemigo: ({_iteradorDePuntos}) [{siguientePosición.position}]");
    }

    public void Update() {

        switch (_estadoActual) {
            case Estado.Patrullar:
                Debug.Log("Estado: Patrullar");
            break;
            case Estado.Perseguir:
                Debug.Log("Estado: Perseguir");

                _agente.SetDestination(_jugador.position);

                if (Vector3.Distance(transform.position, _jugador.position) < _distanciaMínimaParaAtacar) {
                    
                }

            break;
            case Estado.Atacar:
                Debug.Log("Estado: Atacar");
                _agente.isStopped = true;
            break;
            case Estado.Huir:
                Debug.Log("Estado: Huir");
                
            break;
            default:
                throw new System.Exception("Fatal: Estado no encontrado");
        }
    }

}
