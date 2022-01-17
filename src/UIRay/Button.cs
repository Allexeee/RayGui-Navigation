using Raylib_CsLo;
using static Raylib_CsLo.RayGui;
using static Raylib_CsLo.Raylib;

namespace UI.Ray
{
  public class Button : UI.Button
  {
    public Rectangle Rectangle { get; set; }

    GuiControlState state;

    public event Action? OnClick;

    public Button(Rectangle rectangle)
    {
      Rectangle = rectangle;
    }

    public override void Draw()
    {
      state = GetState();
      Raylib.DrawRectangleRec(Rectangle, Fade(GetColor((uint)GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.BASE_COLOR_NORMAL + ((int)state * 3))), 1f));
      Raylib.DrawRectangleLinesEx(Rectangle, GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.BORDER_WIDTH), Fade(GetColor((uint)GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.BORDER_COLOR_NORMAL + ((int)state * 3))), 1f));
    }

    public override void SetFocused()
    {
      state = GuiControlState.GUI_STATE_FOCUSED;
    }

    public override void Reset()
    {
      state = GuiControlState.GUI_STATE_NORMAL;
    }
    public override void SetDowned()
    {
      state = GuiControlState.GUI_STATE_PRESSED;
    }

    public override string? ToString()
    {
      return base.ToString();
    }

    public override void SetReleased()
    {
      OnClick?.Invoke();
    }

    UI.GuiControlState GetState()
    {
      var stateMouse = ProcessorMouse.Check(this);
      var stateNav = ProcessorNavigation.Check(this);

      UI.GuiControlState result = UI.GuiControlState.GUI_STATE_NORMAL;

      if (stateNav == UI.GuiControlState.GUI_STATE_RELEASED || stateMouse == UI.GuiControlState.GUI_STATE_RELEASED)
      {
        result = UI.GuiControlState.GUI_STATE_PRESSED;
      }
      else
      if (stateNav == UI.GuiControlState.GUI_STATE_PRESSED || stateMouse == UI.GuiControlState.GUI_STATE_PRESSED)
      {
        ProcessorNavigation.Selected = this;

        result = UI.GuiControlState.GUI_STATE_PRESSED;
      }
      else
      if (stateNav == UI.GuiControlState.GUI_STATE_FOCUSED || stateMouse == UI.GuiControlState.GUI_STATE_FOCUSED)
        result = UI.GuiControlState.GUI_STATE_FOCUSED;

      return result;
    }
  }
}