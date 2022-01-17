namespace UI
{
  public class ProcessorUI
  {
    List<Button> _buttons;

    public IEnumerable<Button> Buttons => _buttons;

    public ProcessorUI()
    {
      _buttons = new List<Button>();
    }

    public void Add(Button button)
    {
      if (_buttons.Contains(button))
        throw new InvalidOperationException($"Элемент уже добавлен в список!");
      _buttons.Add(button);
    }

    public void Draw()
    {
      foreach (var element in Buttons)
      {
        element.UpdateAndDraw();
      }
    }
  }
}