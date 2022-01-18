using System.Numerics;
using Raylib_CsLo;
using UI;

namespace UI.Ray
{
  public class ProcessorMouse
  {
    StorageSelected _storageSelected;
    InputType _inputType;

    public ProcessorMouse(InputType inputType, StorageSelected storageSelected)
    {
      _inputType = inputType;
      _storageSelected = storageSelected;
    }

    public UI.ElementInputState Check(UI.Ray.Button element, out InputType input)
    {
      Vector2 mousePoint = Raylib.GetMousePosition();

      UI.ElementInputState result = UI.ElementInputState.Normal;
      input = _inputType;

      if (Raylib.CheckCollisionPointRec(mousePoint, element.Rectangle))
      {
        if (Raylib.IsMouseButtonDown(Raylib.MOUSE_LEFT_BUTTON))
          result = UI.ElementInputState.Pressed;
        else
          result = UI.ElementInputState.Focused;

        if (Raylib.IsMouseButtonReleased(Raylib.MOUSE_LEFT_BUTTON))
        {
          result = UI.ElementInputState.Released;
        }
      }

      return result;
    }
  }
}