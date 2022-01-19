namespace UI.Ray
{
  public abstract class ProcessorInput<TElement> where TElement : IElement
  {
    public abstract UI.ElementInputState Check(TElement element, out InputType input);
  }
}