using System.Numerics;
using Raylib_CsLo;
using UI;

namespace UI.Ray
{

  public class ProcessorMouse
  {
    StorageSelected _storageSelected;
    ProcessorUI _processorUI;

    public ProcessorMouse(ProcessorUI processorUI, StorageSelected storageSelected)
    {
      _processorUI = processorUI;
      _storageSelected = storageSelected;
    }

    public void Update()
    {
      Vector2 mousePoint = Raylib.GetMousePosition();

      foreach (UI.Ray.Button element in _processorUI.Buttons)
      {
        // Check button state

        if (element != _storageSelected.Selected)
          element.Reset();
        if (Raylib.CheckCollisionPointRec(mousePoint, element.Rectangle))
        {
          if (Raylib.IsMouseButtonDown(Raylib.MOUSE_LEFT_BUTTON))
            element.SetDowned();
          else
          {
            _storageSelected.Selected = element;
            element.SetFocused();
          }
          if (Raylib.IsMouseButtonReleased(Raylib.MOUSE_LEFT_BUTTON))
          {
            element.SetReleased();
            // element.Reset();
          }
        }
      }
    }

    public static UI.GuiControlState Check(UI.Ray.Button element)
    {
      Vector2 mousePoint = Raylib.GetMousePosition();

      UI.GuiControlState result = UI.GuiControlState.GUI_STATE_NORMAL;

      if (Raylib.CheckCollisionPointRec(mousePoint, element.Rectangle))
      {
        if (Raylib.IsMouseButtonDown(Raylib.MOUSE_LEFT_BUTTON))
          result = GuiControlState.GUI_STATE_PRESSED;
        else
          result = GuiControlState.GUI_STATE_FOCUSED;

        if (Raylib.IsMouseButtonReleased(Raylib.MOUSE_LEFT_BUTTON))
        {
          result = GuiControlState.GUI_STATE_RELEASED;
        }
      }

      return result;
    }
  }
}