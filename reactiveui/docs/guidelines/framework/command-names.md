# Command Names

## Modern Approach (Recommended)

With **ReactiveUI.SourceGenerators**, commands are automatically generated with a `Command` suffix. This is the recommended naming convention for modern ReactiveUI applications.

### Using ReactiveUI.SourceGenerators ?

When using the `[ReactiveCommand]` attribute, the generator automatically creates a property with the `Command` suffix:

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class MyViewModel : ReactiveObject
{
    // Method name: Synchronize
    // Generated property: SynchronizeCommand
    [ReactiveCommand]
    private async Task Synchronize()
    {
        await SynchronizeImpl(mergeInsteadOfRebase: !IsAhead);
    }
    
    private async Task SynchronizeImpl(bool mergeInsteadOfRebase)
    {
        // Implementation here
    }
}

// Usage in view:
this.BindCommand(ViewModel, vm => vm.SynchronizeCommand, v => v.SyncButton);
```

**Benefits**:
- ? Consistent naming convention
- ? Clear distinction between methods and commands
- ? Matches platform conventions (WPF, MAUI)
- ? Generated code is predictable
- ? Less boilerplate code

### Standard ReactiveCommand Pattern

When creating commands manually (without source generators), **suffix command properties with `Command`**:

```csharp
using ReactiveUI;
using System.Reactive;

public class MyViewModel : ReactiveObject
{
    // Property name includes 'Command' suffix
    public ReactiveCommand<Unit, Unit> SynchronizeCommand { get; }
    
    public MyViewModel()
    {
        SynchronizeCommand = ReactiveCommand.CreateFromTask(
            () => SynchronizeImpl(mergeInsteadOfRebase: !IsAhead));
    }
    
    private async Task SynchronizeImpl(bool mergeInsteadOfRebase)
    {
        // Implementation here
    }
}

// Usage in view:
this.BindCommand(ViewModel, vm => vm.SynchronizeCommand, v => v.SyncButton);
```

## Naming Conventions

### Command Property Names

Use verbs that describe the command's action, with `Command` suffix:

```csharp
// Good ?
public ReactiveCommand<Unit, Unit> SaveCommand { get; }
public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
public ReactiveCommand<Unit, Unit> RefreshCommand { get; }
public ReactiveCommand<string, Unit> SearchCommand { get; }

// Avoid ?
public ReactiveCommand<Unit, Unit> Save { get; }  // Missing 'Command' suffix
public ReactiveCommand<Unit, Unit> PerformSave { get; }  // Unclear naming
```

### Implementation Method Names

When implementation is too complex for inline code, suffix the method with `Impl`:

```csharp
public partial class MyViewModel : ReactiveObject
{
    [ReactiveCommand]
    private async Task Save()
    {
        await SaveImpl();
    }
    
    [ReactiveCommand]
    private async Task Delete()
    {
        await DeleteImpl();
    }
    
    // Implementation methods
    private async Task SaveImpl() { /* ... */ }
    private async Task DeleteImpl() { /* ... */ }
}
```

## Complete Examples

### Example 1: Simple Command with Source Generators

```csharp
public partial class MainViewModel : ReactiveObject
{
    [Reactive]
    private string _searchText = string.Empty;
    
    // Generates: public ReactiveCommand<Unit, Unit> SearchCommand { get; }
    [ReactiveCommand]
    private async Task Search()
    {
        var results = await SearchImpl(SearchText);
        Results = results;
    }
    
    private async Task<List<SearchResult>> SearchImpl(string searchText)
    {
        // Actual search implementation
        return await _searchService.SearchAsync(searchText);
    }
}
```

### Example 2: Command with Parameter

```csharp
public partial class ItemViewModel : ReactiveObject
{
    // Generates: public ReactiveCommand<Item, Unit> DeleteItemCommand { get; }
    [ReactiveCommand]
    private async Task DeleteItem(Item item)
    {
        await DeleteItemImpl(item);
    }
    
    private async Task DeleteItemImpl(Item item)
    {
        await _repository.DeleteAsync(item);
    }
}
```

### Example 3: Command with CanExecute

```csharp
public partial class EditViewModel : ReactiveObject
{
    [Reactive]
    private bool _isValid;
    
    private IObservable<bool> _canSave;
    
    public EditViewModel()
    {
        _canSave = this.WhenAnyValue(x => x.IsValid);
    }
    
    // Generates: public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    [ReactiveCommand(CanExecute = nameof(_canSave))]
    private async Task Save()
    {
        await SaveImpl();
    }
    
    private async Task SaveImpl()
    {
        await _dataService.SaveAsync(Data);
    }
}
```

### Example 4: Manual Command Creation

```csharp
public class LegacyViewModel : ReactiveObject
{
    // When not using source generators, explicitly include 'Command' suffix
    public ReactiveCommand<Unit, Unit> LoadDataCommand { get; }
    public ReactiveCommand<string, Unit> FilterCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearCommand { get; }
    
    public LegacyViewModel()
    {
        LoadDataCommand = ReactiveCommand.CreateFromTask(LoadDataImpl);
        
        FilterCommand = ReactiveCommand.Create<string>(filter => 
            FilterImpl(filter));
        
        var canClear = this.WhenAnyValue(x => x.HasData);
        ClearCommand = ReactiveCommand.Create(ClearImpl, canClear);
    }
    
    private async Task LoadDataImpl() { /* ... */ }
    private void FilterImpl(string filter) { /* ... */ }
    private void ClearImpl() { /* ... */ }
}
```

## Legacy Code Warning

> **Note**: Older ReactiveUI code may not follow the `Command` suffix convention. When modernizing legacy code:
> 1. Update property names to include `Command` suffix
> 2. Migrate to ReactiveUI.SourceGenerators where possible
> 3. Update view bindings to reference new property names

### Migration Example

**Before (Legacy)**:
```csharp
public ReactiveCommand Synchronize { get; private set; }

Synchronize = ReactiveCommand.CreateFromObservable(
    _ => SynchronizeImpl(mergeInsteadOfRebase: !IsAhead));
```

**After (Modern)**:
```csharp
[ReactiveCommand]
private async Task Synchronize()
{
    await SynchronizeImpl(mergeInsteadOfRebase: !IsAhead);
}
```

## Best Practices

1. **Always use `Command` suffix** - Makes commands easily identifiable
2. **Use source generators** - Reduces boilerplate and ensures consistency
3. **Verb-based names** - Clearly describe the action (Save, Delete, Refresh)
4. **Suffix complex implementations with `Impl`** - Separates interface from implementation
5. **Consistent casing** - Use PascalCase for command properties

## Related Topics

- [SourceGenerators Guide](~/docs/handbook/view-models/boilerplate-code.md)
- [Commands Handbook](~/docs/handbook/commands/index.md)
- [View Models](~/docs/handbook/view-models/index.md)
- [Data Binding](~/docs/handbook/data-binding/index.md)
