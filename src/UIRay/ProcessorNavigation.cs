using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace UI.Ray
{
  public class ProcessorNavigation : UI.ProcessorNavigation
  {
    public ProcessorNavigation(StorageSelected storageSelected) : base(storageSelected)
    {
    }

    public void Update()
    {
      if (IsKeyReleased(KeyboardKey.KEY_S))
      {
        Selected.Reset();
        SetSelected(GetNextDown());
      }

      // if (IsKeyDown(KeyboardKey.KEY_SPACE))
      // {
      //   Selected.SetDowned();
      // }

      // if (IsKeyReleased(KeyboardKey.KEY_SPACE))
      // {
      //   Selected.SetReleased();
      //   Selected.Reset();
      //   Selected.SetFocused();
      // }
    }

    public static UI.GuiControlState Check(UI.Ray.Button element)
    {
      UI.GuiControlState result = UI.GuiControlState.GUI_STATE_NORMAL;

      if (Selected == element)
      {
        result = UI.GuiControlState.GUI_STATE_FOCUSED;

        if (IsKeyDown(KeyboardKey.KEY_SPACE))
        {
          result = UI.GuiControlState.GUI_STATE_PRESSED;
        }

        if (IsKeyReleased(KeyboardKey.KEY_SPACE))
        {
          result = UI.GuiControlState.GUI_STATE_RELEASED;
        }
      }

      return result;
    }
  }
}