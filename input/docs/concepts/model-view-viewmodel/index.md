Order: 15
Title: Model, View, ViewModel
---


# Marshalling property change events from nested viewmodels

this.WhenAny makes it stupidly easy to marshal property change events from nested viewmodel instances..

i.e. 

```
// var canExecute = this.WhenAny(vm => !vm.IsBusy, vm => !vm.Child.IsSearching)

public ParentViewModel()
{
    public bool IsBusy {get;set;}
    public ChildViewModel Child {get;set;}
}

public class ChildViewModel()
{
    public bool IsSearching {get;set;}
}
```
