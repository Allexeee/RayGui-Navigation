using Raylib_CsLo;
using static Raylib_CsLo.RayGui;
using static Raylib_CsLo.Raylib;

namespace UI.Ray
{
  public class Button : UI.Button
  {
    public Rectangle Rectangle { get; set; }

    InfoElement _info;
    GuiControlState state => _info.State;

    public event Action? OnClick;

    public Button(Rectangle rectangle)
    {
      Rectangle = rectangle;
    }

    public override void UpdateAndDraw()
    {
      // state = GetState();
      _info = UpdateState();
      if (_info.IsPressed)
        OnClick?.Invoke();
      Raylib.DrawRectangleRec(Rectangle, Fade(GetColor((uint)GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.BASE_COLOR_NORMAL + ((int)state * 3))), 1f));
      Raylib.DrawRectangleLinesEx(Rectangle, GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.BORDER_WIDTH), Fade(GetColor((uint)GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.BORDER_COLOR_NORMAL + ((int)state * 3))), 1f));
    }

    // public override void SetFocused()
    // {
    //   state = GuiControlState.GUI_STATE_FOCUSED;
    // }

    // public override void Reset()
    // {
    //   state = GuiControlState.GUI_STATE_NORMAL;
    // }
    // public override void SetDowned()
    // {
    //   state = GuiControlState.GUI_STATE_PRESSED;
    // }

    public override string? ToString()
    {
      return base.ToString();
    }

    public override void SetReleased()
    {
      OnClick?.Invoke();
    }

    InfoElement UpdateState()
    {
      var stateMouse = ProcessorMouse.Check(this);
      var stateNav = ProcessorNavigation.Check(this);

      InfoElement result = new InfoElement
      {
        State = GuiControlState.GUI_STATE_NORMAL,
        IsPressed = stateMouse.IsPressed || stateNav.IsPressed,
      };

      if (stateNav.State == GuiControlState.GUI_STATE_PRESSED || stateMouse.State == GuiControlState.GUI_STATE_PRESSED)
      {
        ProcessorNavigation.Selected = this;

        result.State = GuiControlState.GUI_STATE_PRESSED;
      }
      else
      if (stateNav.State == GuiControlState.GUI_STATE_FOCUSED || stateMouse.State == GuiControlState.GUI_STATE_FOCUSED)
        result.State = GuiControlState.GUI_STATE_FOCUSED;

      return result;
    }
  }
}