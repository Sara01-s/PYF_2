using UnityEngine.AI;
using UnityEngine;

enum Estado {
    Patrullar, Perseguir, Atacar, Huir
}

[RequireComponent(typeof(NavMeshAgent))]
internal sealed class ComportamientoEnemigo : MonoBehaviour {

    [SerializeField] private MovimientoJugador _jugador;
    [SerializeField] private Transform[] _puntosDeRuta;
    [SerializeField] private float _distanciaMínimaParaAtacar;
    [SerializeField] private float _distanciaMáximaDeHuida;
    [SerializeField] private Mesh _meshVisión;

    private float _distanciaConJugador;
    private bool _atacando, _huyendo;
    private int _iteradorDePuntos;
    [SerializeField] Estado _estadoActual;
    private NavMeshAgent _agente;

    private void Awake() {
        _agente = GetComponent<NavMeshAgent>();
        DirigirseASiguientePunto();
    }

    public void Perseguir() => ActualizarEstado(Estado.Perseguir);

    public void DirigirseASiguientePunto() {
        if (_estadoActual != Estado.Patrullar) return;

        var siguientePosición = _puntosDeRuta[_iteradorDePuntos++ % _puntosDeRuta.Length];
        
        _agente.SetDestination(siguientePosición.position);
        Debug.Log($"Siguiente punto enemigo: ({_iteradorDePuntos}) [{siguientePosición.position}]");
    }

    public void Update() {

        _distanciaConJugador = Vector3.Distance(transform.position, _jugador.transform.position);

        if (_distanciaConJugador < _distanciaMínimaParaAtacar && _estadoActual != Estado.Atacar) {
            ActualizarEstado(Estado.Atacar);
        }

        if (_estadoActual == Estado.Huir) {
            if (_distanciaConJugador > _distanciaMáximaDeHuida && _estadoActual != Estado.Patrullar) {
                ActualizarEstado(Estado.Patrullar);
            }
        }
        
    }

    private void ActualizarEstado(Estado nuevoEstado) {
        print(nuevoEstado.ToString());
        switch (nuevoEstado) {
            case Estado.Patrullar:
                DirigirseASiguientePunto();
            break;
            case Estado.Perseguir:
                _agente.SetDestination(_jugador.transform.position);
            break;
            case Estado.Atacar:
                _jugador.RecibirDaño();
                ActualizarEstado(Estado.Huir);
            break;
            case Estado.Huir:
                _agente.SetDestination(-_jugador.transform.position);
            break;
            default:
                throw new System.Exception("Fatal: Estado no encontrado");
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distanciaMínimaParaAtacar);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _distanciaMáximaDeHuida);
    }

}
