# Máquina de Estados para Godot

Este proyecto implementa una **máquina de estados genérica** para Godot, diseñada para gestionar la lógica de estados de un nodo. La máquina de estados permite cambiar dinámicamente entre estados, ejecutar lógica específica de cada estado y manejar eventos del ciclo de vida de Godot como `_Process`, `_PhysicsProcess` y entradas de usuario.

## Componentes Principales

### `StateMachine`

La clase `StateMachine` es el núcleo de la máquina de estados. Sus principales características son:

- **Gestión de Estados**: Permite cambiar entre estados de forma dinámica.
- **Integración con Godot**: Maneja eventos del ciclo de vida de Godot (`_Process`, `_PhysicsProcess`, `_Input`, etc.).
- **Estado por Defecto**: Permite definir un estado inicial que se activa automáticamente al inicio.

#### Métodos Clave

- `StateStart()`: Inicializa el estado actual configurando las referencias necesarias.
- `StateDefaultStart()`: Activa el estado por defecto y lo inicializa.
- `ChangeTo(string newState)`: Cambia al estado especificado por su nombre.
- `_Process(double delta)`: Delegado al método `OnProcess` del estado actual.
- `_PhysicsProcess(double delta)`: Delegado al método `OnPhysicsProcess` del estado actual.

### `StateBase`

La clase `StateBase` es una clase base para implementar estados específicos. Cada estado debe heredar de esta clase y sobrescribir los métodos necesarios para implementar su lógica.

#### Métodos Virtuales

- `AtStartState()`: Llamado al iniciar el estado. Ideal para lógica de inicialización.
- `AtEndState()`: Llamado al finalizar el estado. Ideal para lógica de limpieza.
- `OnProcess(double delta)`: Llamado en cada frame. Útil para lógica de actualización.
- `OnPhysicsProcess(double delta)`: Llamado en cada frame de física.
- `OnInput(InputEvent @event)`: Llamado para manejar eventos de entrada.
- `OnUnhandledInput(InputEvent @event)`: Llamado para manejar eventos de entrada no procesados.
- `OnUnhandledKeyInput(InputEventKey @event)`: Llamado para manejar eventos de teclado no procesados.

## Cómo Usar

1. **Configurar la Máquina de Estados**:
   - Añade un nodo de tipo `StateMachine` a tu escena.
   - Asigna el nodo que será controlado por la máquina de estados en la propiedad `nodeToControl`.
   - Define un estado por defecto heredando de `StateBase` y asígnalo a la propiedad `defaultState`.

2. **Crear Estados**:
   - Crea clases que hereden de `StateBase`.
   - Sobrescribe los métodos necesarios (`AtStartState`, `OnProcess`, etc.) para implementar la lógica específica del estado.

3. **Cambiar Estados**:
   - Usa el método `ChangeTo(string newState)` para cambiar dinámicamente entre estados.

## Ejemplo de Uso

```csharp
// Estado personalizado que hereda de StateBase
public partial class IdleState : StateBase
{
    public override void AtStartState()
    {
        GD.Print("Estado Idle iniciado");
    }

    public override void OnProcess(double delta)
    {
        GD.Print("Estado Idle procesando...");
    }
}
```