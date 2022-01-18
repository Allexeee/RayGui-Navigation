namespace UI
{
  public class ProcessorUI
  {
    List<IElement> _elements;

    public IEnumerable<IElement> Elements => _elements;


    Dictionary<IElement, InputType?> _inputs = new Dictionary<IElement, InputType?>();

    public Dictionary<IElement, InputType?> Inputs => _inputs;

    public ProcessorUI()
    {
      _elements = new List<IElement>();
    }

    public void Add(IElement element)
    {
      if (_elements.Contains(element))
        throw new InvalidOperationException($"Элемент уже добавлен в список!");
      _elements.Add(element);
      _inputs.Add(element, null);
    }

    public void Draw()
    {
      foreach (var element in Elements)
      {
        element.UpdateState();
        element.Draw();
      }
    }
  }
}