namespace UI
{
  public class ProcessorNavigation
  {
    static StorageSelected _storageSelected;

    public static UI.IElement Selected { get => _storageSelected.Selected; set => _storageSelected.Selected = value; }

    Dictionary<UI.IElement, UI.IElement> _dictionaryDown = new Dictionary<UI.IElement, UI.IElement>();

    public ProcessorNavigation(StorageSelected storageSelected)
    {
      _storageSelected = storageSelected;
    }

    public void RegisterDown(UI.IElement self, UI.IElement next)
    {
      _dictionaryDown.Add(self, next);
    }

    public void SetSelected(UI.IElement self)
    {
      Selected = self;
      // Selected.SetFocused();
    }

    public UI.IElement GetNextDown()
    {
      return _dictionaryDown[Selected];
    }
  }
}