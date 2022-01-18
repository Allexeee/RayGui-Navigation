using Raylib_CsLo;
using static Raylib_CsLo.RayGui;
using static Raylib_CsLo.Raylib;

namespace UI.Ray
{
  public class Button : UI.IElement
  {
    public event Action? Submited;

    GuiControlState _stateShow;

    // public InputType InputType { get; set; }

    public Button(Rectangle rectangle)
    {
      Rectangle = rectangle;
    }

    public Rectangle Rectangle { get; set; }

    void IElement.UpdateState()
    {
      var stateInputNext = Program.GetInputState(this);

      if (stateInputNext == ElementInputState.Pressed)
        ProcessorNavigation.Selected = this;

      if (stateInputNext == ElementInputState.Released)
        OnSubmit();
      _stateShow = GetControlState(stateInputNext, false);
    }

    void IElement.Draw()
    {
      Draw(_stateShow);
    }

    void OnSubmit()
    {
      Submited?.Invoke();
    }

    void Draw(GuiControlState state)
    {
      Raylib.DrawRectangleRec(Rectangle, Fade(GetColor((uint)GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.BASE_COLOR_NORMAL + ((int)state * 3))), 1f));
      Raylib.DrawRectangleLinesEx(Rectangle, GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.BORDER_WIDTH), Fade(GetColor((uint)GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.BORDER_COLOR_NORMAL + ((int)state * 3))), 1f));
    }

    GuiControlState GetControlState(ElementInputState input, bool isDissabled)
    {
      var result = GuiControlState.GUI_STATE_NORMAL;
      if (isDissabled)
        result = GuiControlState.GUI_STATE_DISABLED;
      else switch (input)
        {
          case ElementInputState.Focused:
            result = GuiControlState.GUI_STATE_FOCUSED;
            break;
          case ElementInputState.Pressed:
            result = GuiControlState.GUI_STATE_PRESSED;
            break;
        }

      return result;
    }
  }
}