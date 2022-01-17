namespace UI
{
  public class ProcessorNavigation
  {
    static StorageSelected _storageSelected;

    public static UI.Button Selected { get => _storageSelected.Selected; set => _storageSelected.Selected = value; }

    Dictionary<UI.Button, UI.Button> _dictionaryDown = new Dictionary<UI.Button, UI.Button>();

    public ProcessorNavigation(StorageSelected storageSelected)
    {
      _storageSelected = storageSelected;
    }

    public void RegisterDown(UI.Button self, UI.Button next)
    {
      _dictionaryDown.Add(self, next);
    }

    public void SetSelected(UI.Button self)
    {
      Selected = self;
      // Selected.SetFocused();
    }

    public UI.Button GetNextDown()
    {
      return _dictionaryDown[Selected];
    }
  }
}