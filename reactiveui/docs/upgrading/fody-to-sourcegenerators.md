# Migration Guide: Fody to SourceGenerators

This guide helps you migrate from **ReactiveUI.Fody** to **ReactiveUI.SourceGenerators** for a modern, maintainable codebase.

## Why Migrate?

### Benefits of SourceGenerators

✅ **Compile-Time Safety**: Errors caught during compilation, not at runtime  
✅ **No IL Weaving**: Simpler build process and better debugging  
✅ **IDE Support**: Full IntelliSense and code navigation  
✅ **Visible Code**: Generated code is accessible in your IDE  
✅ **Better Performance**: No runtime overhead from IL weaving  
✅ **Future-Proof**: Microsoft's recommended approach for code generation  

### Fody Limitations

❌ **Post-Build Processing**: Adds complexity to build pipeline  
❌ **Debugging Complexity**: Hard to debug woven code  
❌ **Black Box**: Generated code is invisible  
❌ **Build Time**: IL weaving adds overhead  
❌ **Tooling Issues**: Can conflict with other analyzers  

## Prerequisites

- **Minimum Requirements**:
  - C# 12.0+
  - Visual Studio 17.8.0+ / Rider 2023.3+ / VS Code with C# Dev Kit
  - ReactiveUI 19.5.31+

## Step 1: Install Source Generators

### Remove Fody Packages

```xml
<!-- Remove these -->
<PackageReference Include="ReactiveUI.Fody" Version="*" />
<PackageReference Include="Fody" Version="*" />
```

### Install SourceGenerators

```xml
<!-- Add these -->
<PackageReference Include="ReactiveUI.SourceGenerators" Version="*" PrivateAssets="all" />
<PackageReference Include="ReactiveMarbles.ObservableEvents.SourceGenerator" Version="*" PrivateAssets="all" />
```

### Remove FodyWeavers.xml

Delete the `FodyWeavers.xml` file from your project root.

## Step 2: Migrate Reactive Properties

### Fody Pattern

```csharp
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

public class MyViewModel : ReactiveObject
{
    [Reactive]
    public string Name { get; set; }
    
    [Reactive]
    public int Age { get; set; }
}
```

### SourceGenerators Pattern

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class MyViewModel : ReactiveObject
{
    // Note: Class must be partial
    [Reactive]
    private string _name = string.Empty;
    
    [Reactive]
    private int _age;

    [Reactive]
    public partial string Address { get; set; }
}
```

### Key Differences

| Aspect | Fody | SourceGenerators |
|--------|------|------------------|
| Class modifier | Not required | `partial` required |
| Property/Field | Property | Private field |
| Initialization | `{ get; set; }` | `= defaultValue;` |
| Access | Public property | Generated public property |

## Step 3: Migrate ObservableAsPropertyHelper

### Fody Pattern

```csharp
public class MyViewModel : ReactiveObject
{
    [ObservableAsProperty]
    public string FullName { get; }
    
    public MyViewModel()
    {
        this.WhenAnyValue(x => x.FirstName, x => x.LastName,
                (f, l) => $"{f} {l}")
            .ToPropertyEx(this, x => x.FullName);
    }
}
```

### SourceGenerators Pattern

```csharp
public partial class MyViewModel : ReactiveObject
{
    [Reactive]
    private string _firstName = string.Empty;
    
    [Reactive]
    private string _lastName = string.Empty;
    
    [ObservableAsProperty]
    private string _fullName = string.Empty;
    
    public MyViewModel()
    {
        _fullNameHelper = this.WhenAnyValue(x => x.FirstName, x => x.LastName,
                (f, l) => $"{f} {l}")
            .ToProperty(this, nameof(FullName));
    }
}
```

### Alternative: Observable Property (New Feature)

```csharp
public partial class MyViewModel : ReactiveObject
{
    [Reactive]
    private string _firstName = string.Empty;
    
    [Reactive]
    private string _lastName = string.Empty;
    
    public MyViewModel()
    {
        // Simpler - InitializeOAPH() called automatically
        InitializeOAPH();
    }
    
    [ObservableAsProperty]
    private IObservable<string> FullName => 
        this.WhenAnyValue(x => x.FirstName, x => x.LastName,
            (f, l) => $"{f} {l}");
}
```

## Step 4: Migrate Reactive Commands

### Fody Pattern

```csharp
public class MyViewModel : ReactiveObject
{
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    
    public MyViewModel()
    {
        SaveCommand = ReactiveCommand.CreateFromTask(SaveAsync);
    }
    
    private async Task SaveAsync()
    {
        // Implementation
    }
}
```

### SourceGenerators Pattern

```csharp
public partial class MyViewModel : ReactiveObject
{
    [ReactiveCommand]
    private async Task Save()
    {
        // Implementation
    }
    
    // Generated: public ReactiveCommand<Unit, Unit> SaveCommand { get; }
}
```

### Command with CanExecute

```csharp
public partial class MyViewModel : ReactiveObject
{
    [Reactive]
    private bool _isValid;
    
    private IObservable<bool> _canSave;
    
    public MyViewModel()
    {
        _canSave = this.WhenAnyValue(x => x.IsValid);
    }
    
    [ReactiveCommand(CanExecute = nameof(_canSave))]
    private async Task Save()
    {
        // Implementation
    }
}
```

## Step 5: Handle Complex Scenarios

### Property with Initial Value

```csharp
// Fody
[Reactive]
public string Name { get; set; } = "Default";

// SourceGenerators ✅
[Reactive]
private string _name = "Default";
```

### Property with Attributes

```csharp
using System.Text.Json.Serialization;

// Fody
[Reactive]
[JsonPropertyName("user_name")]
public string UserName { get; set; }

// SourceGenerators ✅
[Reactive]
[property: JsonPropertyName("user_name")]
private string _userName = string.Empty;
```

### Read-Only OAPH

```csharp
// Fody
[ObservableAsProperty]
public string Status { get; }

// SourceGenerators ✅
[ObservableAsProperty]
private string _status = string.Empty;

public MyViewModel()
{
    _statusHelper = SomeObservable
        .ToProperty(this, nameof(Status));
}
```

### Command with Parameters

```csharp
// Fody
public ReactiveCommand<string, Unit> ProcessCommand { get; }

public MyViewModel()
{
    ProcessCommand = ReactiveCommand.Create<string>(Process);
}

private void Process(string parameter) { }

// SourceGenerators ✅
[ReactiveCommand]
private void Process(string parameter)
{
    // Implementation
}
```

## Step 6: Update Generic Types

### Fody Limitation

Fody has issues with inline property initializers in generic types.

```csharp
// Problematic with Fody ❌
public class MyViewModel<T> : ReactiveObject
{
    [Reactive]
    public string Name { get; set; } = "Default"; // May cause issues
}
```

### SourceGenerators Solution

```csharp
// Works perfectly ✅
public partial class MyViewModel<T> : ReactiveObject
{
    [Reactive]
    private string _name = "Default"; // No issues
}
```

## Step 7: Rebuild and Test

### 1. Clean Solution

```bash
dotnet clean
```

### 2. Delete bin/obj Directories

```bash
# PowerShell
Get-ChildItem -Path . -Include bin,obj -Recurse -Directory | Remove-Item -Recurse -Force

# Bash
find . -type d \( -name bin -o -name obj \) -exec rm -rf {} +
```

### 3. Rebuild

```bash
dotnet build
```

### 4. View Generated Code

In Visual Studio:
1. Expand **Dependencies** → **Analyzers** → **ReactiveUI.SourceGenerators**
2. Browse generated files

## Common Migration Issues

### Issue 1: Class Not Partial

**Error**: `The type 'MyViewModel' must be declared as partial`

**Solution**: Add `partial` keyword to class declaration

```csharp
// Before ❌
public class MyViewModel : ReactiveObject

// After ✅
public partial class MyViewModel : ReactiveObject
```

### Issue 2: Property Instead of Field

**Error**: `[Reactive] can only be applied to fields`

**Solution**: Change property to field

```csharp
// Before ❌
[Reactive]
public string Name { get; set; }

// After ✅
[Reactive]
private string _name = string.Empty;
```

### Issue 3: Missing InitializeOAPH Call

**Error**: `ObservableAsPropertyHelper not initialized`

**Solution**: Call `InitializeOAPH()` in constructor

```csharp
public MyViewModel()
{
    InitializeOAPH(); // Add this line
}
```

### Issue 4: Namespace Conflicts

**Error**: `'Reactive' is ambiguous`

**Solution**: Remove old Fody namespaces

```csharp
// Remove ❌
using ReactiveUI.Fody.Helpers;

// Keep ✅
using ReactiveUI.SourceGenerators;
```

## Migration Checklist

- [ ] Install ReactiveUI.SourceGenerators package
- [ ] Remove ReactiveUI.Fody package
- [ ] Remove Fody package
- [ ] Delete FodyWeavers.xml
- [ ] Add `partial` keyword to all reactive classes
- [ ] Convert `[Reactive]` properties to fields or use `partial` properties
- [ ] Update `[ObservableAsProperty]` properties
- [ ] Update `.ToPropertyEx` calls to `.ToProperty`
- [ ] Migrate reactive commands using `[ReactiveCommand]`
- [ ] Update using statements
- [ ] Clean and rebuild solution
- [ ] Run all unit tests
- [ ] Test application functionality

## Side-by-Side Comparison

### Complete Example: Fody

```csharp
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

public class PersonViewModel : ReactiveObject
{
    [Reactive]
    public string FirstName { get; set; }
    
    [Reactive]
    public string LastName { get; set; }
    
    [ObservableAsProperty]
    public string FullName { get; }
    
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    
    public PersonViewModel()
    {
        this.WhenAnyValue(x => x.FirstName, x => x.LastName,
                (f, l) => $"{f} {l}")
            .ToPropertyEx(this, x => x.FullName);
        
        SaveCommand = ReactiveCommand.CreateFromTask(SaveAsync);
    }
    
    private async Task SaveAsync()
    {
        // Save logic
    }
}
```

### Complete Example: SourceGenerators

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class PersonViewModel : ReactiveObject
{
    [Reactive]
    private string _firstName = string.Empty;
    
    [Reactive]
    private string _lastName = string.Empty;
    
    [ObservableAsProperty]
    private string _fullName = string.Empty;
    
    public PersonViewModel()
    {
        _fullNameHelper = this.WhenAnyValue(x => x.FirstName, x => x.LastName,
                (f, l) => $"{f} {l}")
            .ToProperty(this, nameof(FullName));
    }
    
    [ReactiveCommand]
    private async Task Save()
    {
        // Save logic
    }
}
```

## Benefits After Migration

### Code Quality
- ✅ More explicit and readable
- ✅ Better IDE support and IntelliSense
- ✅ Easier code review
- ✅ Visible generated code

### Build Process
- ✅ Faster builds (no IL weaving)
- ✅ Simpler CI/CD pipeline
- ✅ Better compatibility with other tools
- ✅ Easier troubleshooting

### Debugging
- ✅ Step through generated code
- ✅ Set breakpoints in properties
- ✅ Inspect generated members
- ✅ No black-box behavior

## Additional Resources

- [ReactiveUI.SourceGenerators GitHub](https://github.com/reactiveui/ReactiveUI.SourceGenerators)
- [Source Generators Documentation](~/docs/handbook/view-models/boilerplate-code.md)
- [Guidelines](~/docs/guidelines/index.md)
- [Sample Applications](~/docs/resources/samples.md)

## Getting Help

If you encounter issues during migration:

1. Check the [GitHub Issues](https://github.com/reactiveui/ReactiveUI.SourceGenerators/issues)
2. Ask on [Slack](https://join.slack.com/t/reactivex/shared_invite/zt-lt48skpz-G5WDYOAuzA80_MByZrLT0g)
3. Review [Sample Code](https://github.com/reactiveui/ReactiveUI.Samples)

---

**Migration Time Estimate**: 1-4 hours for a typical application
**Difficulty**: Easy to Moderate
**Recommended**: Yes - Modern tooling with long-term support
