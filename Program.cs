using System.Numerics;
using Raylib_CsLo;
using UI.Ray;
using static Raylib_CsLo.RayGui;
using static Raylib_CsLo.Raylib;
public static class Program
{
  static ProcessorMouse processorMouse;
  static ProcessorNavigation processorNavigation;
  static UI.ProcessorUI processorUI;

  public static unsafe void Main(params string[] args)
  {
    Raylib.InitWindow(420, 420, "RayGUI");
    var q = new Vector2(5, 20);
    var req1 = new Rectangle(50, 50, 400, 400);
    var req2 = new Rectangle(50, 50, 100, 100);
    Vector2* r = &q;

    UI.StorageSelected storageSelected = new UI.StorageSelected();
    processorUI = new UI.ProcessorUI();
    var inputTypeMouse = new InputTypeMouse();
    var inputTypeNavigation = new InputTypeNavigation();
    processorMouse = new ProcessorMouse(inputTypeMouse, storageSelected);
    processorNavigation = new ProcessorNavigation(inputTypeNavigation, storageSelected);
    Button button1 = new Button(new Rectangle(50, 50, 200, 50));
    Button button2 = new Button(new Rectangle(50, 100, 200, 50));
    Button button3 = new Button(new Rectangle(50, 150, 200, 50));
    processorUI.Add(button1);
    processorUI.Add(button2);
    processorUI.Add(button3);

    processorNavigation.RegisterDown(button1, button2);
    processorNavigation.RegisterDown(button2, button3);
    processorNavigation.RegisterDown(button3, button1);

    processorNavigation.SetSelected(button1);

    button1.Submited += () => OnClick($"Button 1 click!");
    button2.Submited += () => OnClick($"Button 2 click!");
    button3.Submited += () => OnClick($"Button 3 click!");

    while (!Raylib.WindowShouldClose())
    {
      Raylib.BeginDrawing();
      Raylib.ClearBackground(Raylib.SKYBLUE);

      processorNavigation.Update();
      // processorMouse.Update();
      processorUI.Draw();

      // if (button.IsPressed())
      // {
      //   Console.WriteLine($"Button 1 click!");
      // }

      // if (GuiButtonT(new Rectangle(50, 50, 200, 50), "Button 1"))
      // {
      //   Console.WriteLine($"Button 1 click!");
      // }
      // if (RayGui.GuiButton(new Rectangle(50, 100, 200, 50), "Button 2"))
      // {
      //   Console.WriteLine($"Button 2 click!");
      // }

      // if (GuiButton(new Rectangle(50, 150, 200, 50), GuiIconText(5, "Open Image")))
      // {
      // }
      // RayGui.GuiScrollPanel(req2, req1, r);

      Raylib.EndDrawing();
    }

    Raylib.CloseWindow();
  }

  static void OnClick(string text)
  {
    Console.WriteLine(text);
  }

  static bool GuiButtonT(Rectangle bounds, string text)
  {
    GuiControlState state = GuiControlState.GUI_STATE_NORMAL;
    bool pressed = false;

    // Update control
    //--------------------------------------------------------------------
    if ((state != GuiControlState.GUI_STATE_DISABLED))
    {
      Vector2 mousePoint = Raylib.GetMousePosition();

      // Check button state
      if (Raylib.CheckCollisionPointRec(mousePoint, bounds))
      {
        if (Raylib.IsMouseButtonDown(Raylib.MOUSE_LEFT_BUTTON)) state = GuiControlState.GUI_STATE_PRESSED;
        else state = GuiControlState.GUI_STATE_FOCUSED;

        if (IsMouseButtonReleased(MOUSE_LEFT_BUTTON)) pressed = true;
      }
    }
    //--------------------------------------------------------------------

    // Draw control
    //--------------------------------------------------------------------
    Raylib.DrawRectangleRec(bounds, Fade(GetColor((uint)GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.BASE_COLOR_NORMAL + ((int)state * 3))), 1f));
    Raylib.DrawRectangleLinesEx(bounds, GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.BORDER_WIDTH), Fade(GetColor((uint)GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.BORDER_COLOR_NORMAL + ((int)state * 3))), 1f));

    // Raylib.DrawText(text, GuiTextBo)
    // RayGui.GuiDrawRectangle(bounds, GuiGetStyle(BUTTON, BORDER_WIDTH), Fade(GetColor(GuiGetStyle(BUTTON, BORDER + (state * 3))), guiAlpha), Fade(GetColor(GuiGetStyle(BUTTON, BASE + (state * 3))), guiAlpha));
    GuiDrawText(text, GetTextBounds((int)GuiControl.BUTTON, bounds), GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.TEXT_ALIGNMENT), Fade(GetColor((uint)GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.TEXT_COLOR_NORMAL + ((int)state * 3))), 1f));
    var boun = GetTextBounds((int)GuiControl.BUTTON, bounds);
    // Raylib.DrawText(text, boun.x, boun.y, GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.TEXT_ALIGNMENT), Fade(GetColor((uint)GuiGetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.TEXT_COLOR_NORMAL + ((int)state * 3))), 1f));

    //------------------------------------------------------------------

    return pressed;
  }

  // Get text bounds considering control bounds
  static Rectangle GetTextBounds(int control, Rectangle bounds)
  {
    Rectangle textBounds = bounds;

    textBounds.x = bounds.x + GuiGetStyle(control, (int)GuiControlProperty.BORDER_WIDTH);
    textBounds.y = bounds.y + GuiGetStyle(control, (int)GuiControlProperty.BORDER_WIDTH);
    textBounds.width = bounds.width - 2 * GuiGetStyle(control, (int)GuiControlProperty.BORDER_WIDTH);
    textBounds.height = bounds.height - 2 * GuiGetStyle(control, (int)GuiControlProperty.BORDER_WIDTH);

    // Consider TEXT_PADDING properly, depends on control type and TEXT_ALIGNMENT
    // switch (control)
    // {
    //   case  (int)GuiControl.COMBOBOX: bounds.width -= (GuiGetStyle(control, COMBO_BUTTON_WIDTH) + GuiGetStyle(control, COMBO_BUTTON_PADDING)); break;
    //   case (int)GuiControl.VALUEBOX: break;   // NOTE: ValueBox text value always centered, text padding applies to label
    //   default:
    //     {
    //       if (GuiGetStyle(control, TEXT_ALIGNMENT) == GUI_TEXT_ALIGN_RIGHT) textBounds.x -= GuiGetStyle(control, TEXT_PADDING);
    //       else textBounds.x += GuiGetStyle(control, TEXT_PADDING);
    //     }
    //     break;
    // }

    // TODO: Special cases (no label): COMBOBOX, DROPDOWNBOX, LISTVIEW (scrollbar?)
    // More special cases (label on side): CHECKBOX, SLIDER, VALUEBOX, SPINNER

    return textBounds;
  }

  // Gui draw text using default font
  static void GuiDrawText(string text, Rectangle bounds, int alignment, Color tint)
  {
    int TEXT_VALIGN_PIXEL_OFFSET(float h)
    {
      return ((int)h % 2);     // Vertical alignment for pixel perfect
    }

    const int RAYGUI_ICON_TEXT_PADDING = 4;

    if ((text != null) && (text[0] != '\0'))
    {
      // int iconId = 0;
      // text = GuiIconText(iconId, text);              // Check text for icon and move cursor

      // Get text position depending on alignment and iconId
      //---------------------------------------------------------------------------------


      Vector2 position = new Vector2(bounds.x, bounds.y);

      // NOTE: We get text size after icon has been processed
      int textWidth = GetTextWidth(text);
      int textHeight = GuiGetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE);

      // // If text requires an icon, add size to measure
      // if (iconId >= 0)
      // {
      //   textWidth += RAYGUI_ICON_SIZE;

      //   // WARNING: If only icon provided, text could be pointing to EOF character: '\0'
      //   if ((text != NULL) && (text[0] != '\0')) textWidth += RAYGUI_ICON_TEXT_PADDING;
      // }

      // Check guiTextAlign global variables
      switch (alignment)
      {
        case (int)GuiTextAlignment.GUI_TEXT_ALIGN_LEFT:
          {
            position.X = bounds.x;
            position.Y = bounds.y + bounds.height / 2 - textHeight / 2 + TEXT_VALIGN_PIXEL_OFFSET(bounds.height);
          }
          break;
        case (int)GuiTextAlignment.GUI_TEXT_ALIGN_CENTER:
          {
            position.X = bounds.x + bounds.width / 2 - textWidth / 2;
            position.Y = bounds.y + bounds.height / 2 - textHeight / 2 + TEXT_VALIGN_PIXEL_OFFSET(bounds.height);
          }
          break;
        case (int)GuiTextAlignment.GUI_TEXT_ALIGN_RIGHT:
          {
            position.X = bounds.x + bounds.width - textWidth;
            position.Y = bounds.y + bounds.height / 2 - textHeight / 2 + TEXT_VALIGN_PIXEL_OFFSET(bounds.height);
          }
          break;
        default: break;
      }

      // NOTE: Make sure we get pixel-perfect coordinates,
      // In case of decimals we got weird text positioning
      position.X = (float)((int)position.X);
      position.Y = (float)((int)position.Y);
      //---------------------------------------------------------------------------------

      // Draw text (with icon if available)
      //---------------------------------------------------------------------------------
      // #if !defined(RAYGUI_NO_ICONS)
      //       if (iconId >= 0)
      //       {
      //         // NOTE: We consider icon height, probably different than text size
      //         GuiDrawIcon(iconId, (int)position.x, (int)(bounds.y + bounds.height / 2 - RAYGUI_ICON_SIZE / 2 + TEXT_VALIGN_PIXEL_OFFSET(bounds.height)), 1, tint);
      //         position.x += (RAYGUI_ICON_SIZE + RAYGUI_ICON_TEXT_PADDING);
      //       }
      // #endif
      DrawTextEx(GuiGetFont(), text, position, (float)GuiGetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE), (float)GuiGetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SPACING), tint);
      //---------------------------------------------------------------------------------
    }
  }

  static int GetTextWidth(string? text)
  {
    Vector2 size = new Vector2();

    if ((text != null) && (text[0] != '\0'))
    {
      size = MeasureTextEx(GuiGetFont(), text, (float)GuiGetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE), (float)GuiGetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SPACING));
    }

    return (int)size.X;
  }

  public static UI.ElementInputState GetInputState(UI.Ray.Button button)
  {
    var stateMouse = processorMouse.Check(button, out var inputTypeMouse);
    var stateNav = processorNavigation.Check(button, out var inputTypeNavigation);
    var inputTypeButton = processorUI.Inputs[button];

    UI.ElementInputState result = UI.ElementInputState.Normal;

    if ((inputTypeButton == null || inputTypeButton == inputTypeNavigation) && stateNav == UI.ElementInputState.Pressed)
    {
      result = UI.ElementInputState.Pressed;
      processorUI.Inputs[button] = inputTypeNavigation;
      // button.InputType = InputType.Navigation;
    }
    else if (inputTypeButton == inputTypeNavigation && stateNav == UI.ElementInputState.Released)
    {
      result = UI.ElementInputState.Released;
    }
    else if (stateMouse == UI.ElementInputState.Pressed)
    {
      result = UI.ElementInputState.Pressed;
      processorUI.Inputs[button] = inputTypeMouse;
      // button.InputType = InputType.Mouse;
    }
    else if (inputTypeButton == inputTypeMouse && stateMouse == UI.ElementInputState.Released)
    {
      result = UI.ElementInputState.Released;
    }
    else if (stateNav == UI.ElementInputState.Focused || stateMouse == UI.ElementInputState.Focused)
    {
      result = UI.ElementInputState.Focused;
      processorUI.Inputs[button] = null;
      // button.InputType = InputType.None;
    }

    // if ((button.InputType == InputType.None || button.InputType == InputType.Navigation) && stateNav == UI.ElementInputState.Pressed)
    // {
    //   result = UI.ElementInputState.Pressed;
    //   button.InputType = InputType.Navigation;
    // }
    // else if (button.InputType == InputType.Navigation && stateNav == UI.ElementInputState.Released)
    // {
    //   result = UI.ElementInputState.Released;
    // }
    // else if (stateMouse == UI.ElementInputState.Pressed)
    // {
    //   result = UI.ElementInputState.Pressed;
    //   button.InputType = InputType.Mouse;
    // }
    // else if (button.InputType == InputType.Mouse && stateMouse == UI.ElementInputState.Released)
    // {
    //   result = UI.ElementInputState.Released;
    // }
    // else if (stateNav == UI.ElementInputState.Focused || stateMouse == UI.ElementInputState.Focused)
    // {
    //   result = UI.ElementInputState.Focused;
    //   button.InputType = InputType.None;
    // }

    return result;
  }
}