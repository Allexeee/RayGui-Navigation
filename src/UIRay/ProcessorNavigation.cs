using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace UI.Ray
{
  public class ProcessorNavigation : UI.ProcessorNavigation
  {
    InputType _inputType;

    public ProcessorNavigation(InputType inputType, StorageSelected storageSelected) : base(storageSelected)
    {
      _inputType = inputType;
    }

    public void Update()
    {
      if (IsKeyReleased(KeyboardKey.KEY_S))
      {
        SetSelected(GetNextDown());
      }
    }

    public UI.ElementInputState Check(UI.Ray.Button element, out InputType inputType)
    {
      UI.ElementInputState result = UI.ElementInputState.Normal;

      inputType = _inputType;

      if (Selected == element)
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