using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace UI.Ray
{
  public class ProcessorNavigation : ProcessorInput<UI.IElement>
  {
    InputType _inputType;

    UI.ProcessorNavigation _processorNavigation;

    public ProcessorNavigation(InputType inputType, UI.ProcessorNavigation processorNavigation)
    {
      _inputType = inputType;
      _processorNavigation = processorNavigation;
    }

    public void Update()
    {
      if (IsKeyReleased(KeyboardKey.KEY_S))
      {
        _processorNavigation.Selected = _processorNavigation.GetNextDown();
      }
    }

    public override UI.ElementInputState Check(UI.IElement element, out InputType inputType)
    {
      UI.ElementInputState result = UI.ElementInputState.Normal;

      inputType = _inputType;

      if (_processorNavigation.Selected == element)
      {
        result = UI.ElementInputState.Focused;

        if (IsKeyDown(KeyboardKey.KEY_SPACE))
        {
          result = UI.ElementInputState.Pressed;
        }

        if (IsKeyReleased(KeyboardKey.KEY_SPACE))
        {
          result = UI.ElementInputState.Released;
        }
      }

      return result;
    }
  }
}