using Godot;
using System;

/// <summary>
/// Clase `StateMachine` que gestiona los estados de un nodo en Godot.
/// Permite cambiar entre estados, ejecutar lógica específica de cada estado,
/// y manejar eventos del ciclo de vida de Godot como `_Process`, `_PhysicsProcess` y entradas de usuario.
/// </summary>
public partial class StateMachine : Node
{
    #region Variables

    /// <summary>
    /// Nodo que será controlado por la máquina de estados.
    /// Este nodo debe ser asignado desde el editor de Godot.
    /// </summary>
    [Export] public Node nodeToControl;

    /// <summary>
    /// Estado inicial por defecto que se activará al inicio.
    /// Este estado debe heredar de `StateBase` y ser asignado desde el editor de Godot.
    /// </summary>
    [Export] public StateBase defaultState;

    /// <summary>
    /// Estado actual que está activo en la máquina de estados.
    /// </summary>
    public StateBase currentState = null;

    #endregion

    #region Godot Methods

    /// <summary>
    /// Método llamado cuando el nodo está listo.
    /// Verifica que `nodeToControl` esté asignado y, si hay un estado por defecto,
    /// lo activa de forma diferida.
    /// </summary>
    public override void _Ready()
    {
        if (nodeToControl == null)
        {
            GD.PrintErr("nodeToControl is not assigned");
            return;
        }

        if (defaultState != null)
        {
            CallDeferred(nameof(StateDefaultStart));
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Inicia el estado actual configurando las referencias necesarias
    /// y llamando al método `AtStartState` del estado.
    /// </summary>
    public void StateStart()
    {
        if (currentState == null || nodeToControl == null)
        {
            GD.PrintErr("currentState or nodeToControl is null in StateStart");
            return;
        }

        GD.Print("StateMachine ", nodeToControl.Name, " State Start ", currentState.Name);

        currentState.nodeToControl = nodeToControl;
        currentState.stateMachine = this;
        currentState.AtStartState();
    }

    /// <summary>
    /// Activa el estado por defecto y llama a `StateStart` para inicializarlo.
    /// </summary>
    public void StateDefaultStart()
    {
        currentState = defaultState;
        StateStart();
    }

    /// <summary>
    /// Cambia al estado especificado por su nombre.
    /// Si el estado actual tiene un método `AtEndState`, lo llama antes de cambiar.
    /// </summary>
    /// <param name="newState">Nombre del nodo que representa el nuevo estado.</param>
    public void ChangeTo(string newState)
    {
        if (currentState != null && currentState.HasMethod("AtEndState"))
        {
            currentState.AtEndState();
        }

        currentState = GetNode<StateBase>(newState);

        if (currentState == null)
        {
            GD.PrintErr("Failed to change to state: ", newState);
            return;
        }

        StateStart();
    }

    /// <summary>
    /// Llamado en cada frame. Delegado al método `OnProcess` del estado actual.
    /// </summary>
    /// <param name="delta">Tiempo transcurrido desde el último frame.</param>
    public override void _Process(double delta)
    {
        currentState?.OnProcess(delta);
    }

    /// <summary>
    /// Llamado en cada frame de física. Delegado al método `OnPhysicsProcess` del estado actual.
    /// </summary>
    /// <param name="delta">Tiempo transcurrido desde el último frame de física.</param>
    public override void _PhysicsProcess(double delta)
    {
        currentState?.OnPhysicsProcess(delta);
    }

    /// <summary>
    /// Maneja eventos de entrada. Delegado al método `OnInput` del estado actual.
    /// </summary>
    /// <param name="event">Evento de entrada.</param>
    public override void _Input(InputEvent @event)
    {
        currentState?.OnInput(@event);
    }

    /// <summary>
    /// Maneja eventos de entrada no procesados. Delegado al método `OnUnhandledInput` del estado actual.
    /// </summary>
    /// <param name="event">Evento de entrada no procesado.</param>
    public override void _UnhandledInput(InputEvent @event)
    {
        currentState?.OnUnhandledInput(@event);
    }

    /// <summary>
    /// Maneja eventos de entrada de teclado no procesados. Delegado al método `OnUnhandledKeyInput` del estado actual.
    /// </summary>
    /// <param name="event">Evento de entrada de teclado no procesado.</param>
    public void _UnhandledKeyInput(InputEventKey @event)
    {
        currentState?.OnUnhandledKeyInput(@event);
    }

    #endregion
}