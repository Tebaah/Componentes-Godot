using Godot;
using System;

/// <summary>
/// Clase base para los estados de la máquina de estados (`StateMachine`).
/// Proporciona una estructura común para implementar la lógica específica de cada estado.
/// </summary>
public partial class StateBase : Node
{
    #region Variables

    /// <summary>
    /// Nodo que será controlado por este estado.
    /// Este nodo es asignado por la máquina de estados (`StateMachine`) al iniciar el estado.
    /// </summary>
    public Node nodeToControl;

    /// <summary>
    /// Referencia a la máquina de estados que gestiona este estado.
    /// </summary>
    public StateMachine stateMachine;

    #endregion

    #region Godot Methods

    // Métodos de Godot pueden ser sobrescritos aquí si es necesario.

    #endregion

    #region Methods

    /// <summary>
    /// Método virtual llamado al iniciar el estado.
    /// Se puede sobrescribir en clases derivadas para implementar lógica de inicialización.
    /// </summary>
    public virtual void AtStartState() { }

    /// <summary>
    /// Método virtual llamado al finalizar el estado.
    /// Se puede sobrescribir en clases derivadas para implementar lógica de limpieza.
    /// </summary>
    public virtual void AtEndState() { }

    /// <summary>
    /// Método virtual llamado en cada frame.
    /// Se puede sobrescribir en clases derivadas para implementar lógica de actualización.
    /// </summary>
    /// <param name="delta">Tiempo transcurrido desde el último frame.</param>
    public virtual void OnProcess(double delta) { }

    /// <summary>
    /// Método virtual llamado en cada frame de física.
    /// Se puede sobrescribir en clases derivadas para implementar lógica de actualización física.
    /// </summary>
    /// <param name="delta">Tiempo transcurrido desde el último frame de física.</param>
    public virtual void OnPhysicsProcess(double delta) { }

    /// <summary>
    /// Método virtual llamado para manejar eventos de entrada.
    /// Se puede sobrescribir en clases derivadas para implementar lógica de manejo de entradas.
    /// </summary>
    /// <param name="event">Evento de entrada.</param>
    public virtual void OnInput(InputEvent @event) { }

    /// <summary>
    /// Método virtual llamado para manejar eventos de entrada no procesados.
    /// Se puede sobrescribir en clases derivadas para implementar lógica de manejo de entradas no procesadas.
    /// </summary>
    /// <param name="event">Evento de entrada no procesado.</param>
    public virtual void OnUnhandledInput(InputEvent @event) { }

    /// <summary>
    /// Método virtual llamado para manejar eventos de entrada de teclado no procesados.
    /// Se puede sobrescribir en clases derivadas para implementar lógica de manejo de entradas de teclado no procesadas.
    /// </summary>
    /// <param name="event">Evento de entrada de teclado no procesado.</param>
    public virtual void OnUnhandledKeyInput(InputEvent @event) { }

    #endregion
}