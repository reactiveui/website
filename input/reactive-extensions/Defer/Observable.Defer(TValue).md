title: Observable.Defer<TValue>()
---
# Observable.Defer\<TValue\> Method

Returns an observable sequence that invokes the observable factory whenever a new observer subscribes.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Defer(Of TValue) ( _
    observableFactory As Func(Of IObservable(Of TValue)) _
) As IObservable(Of TValue)
```

```vb
'Usage
Dim observableFactory As Func(Of IObservable(Of TValue))
Dim returnValue As IObservable(Of TValue)

returnValue = Observable.Defer(observableFactory)
```

```csharp
public static IObservable<TValue> Defer<TValue>(
    Func<IObservable<TValue>> observableFactory
)
```

```c++
public:
generic<typename TValue>
static IObservable<TValue>^ Defer(
    Func<IObservable<TValue>^>^ observableFactory
)
```

```fsharp
static member Defer : 
        observableFactory:Func<IObservable<'TValue>> -> IObservable<'TValue> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TValue  
  The type of value.

#### Parameters

- observableFactory  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TValue\>\>  
  The observable factory function to invoke for each observer that subscribes to the resulting sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TValue\>  
The observable sequence whose observers trigger an invocation of the given observable factory function.

## Remarks

The Defer operator allows you to defer or delay the creation of the sequence until the time when an observer subscribes to the sequence. This is useful to allow an observer to easily obtain an updates or refreshed version of the sequence.

## Examples

This example demonstrates the Defer operator by creating an observable sequence of product information used by a business or consumer. The observable sequence provides access to current inventory levels. By creating a deferred observable sequence, the application can have the updated inventory levels pushed to the application by simply re-subscribing to the observable sequence.

    using System;
    using System.Reactive.Linq;
    
    namespace Example
    {
    
      class Program
      {
    
        static void Main()
        {
          //*****************************************************************************************************//
          //*** Product inventories change from time to time. This example demonstrates the Defer operator    ***//
          //*** by creating an observable sequence of the Product class. The creation of the sequence is      ***//
          //*** deferred until the observer calls Subscribe and a new observable sequence is always generated ***//
          //*** at that time with the latest inventory levels to be sent to the observer.                     ***//
          //*****************************************************************************************************//
          ProductInventory myInventory = new ProductInventory();
          IObservable<Product> productObservable = Observable.Defer(myInventory.GetUpdatedInventory);
    
    
          //******************************************************//
          //*** Generate a simple table in the console window. ***//
          //******************************************************//
          Console.WriteLine("Current Inventory...\n");
          Console.WriteLine("\n{0,-13} {1,-37} {2,-18}", "Product Name", "Product ID", "Current Inventory");
          Console.WriteLine("{0,-13} {1,-37} {2,-18}", "============", "====================================",
                            "=================");
    
          //**********************************************************************************//
          //*** Each product in the sequence will be reported in the table using the       ***//
          //*** Observer's OnNext handler provided with the Subscribe method.              ***//
          //**********************************************************************************//
          productObservable.Subscribe(prod => Console.WriteLine(prod.ToString()));
    
    
          //******************************************************************************************************//
          //*** To get the updated sequence from the deferred observable all we have to do is subscribe again. ***//
          //******************************************************************************************************//
          Console.WriteLine("\n\nThe updated current Inventory...\n");
          Console.WriteLine("\n{0,-13} {1,-37} {2,-18}", "Product Name", "Product ID", "Current Inventory");
          Console.WriteLine("{0,-13} {1,-37} {2,-18}", "============", "====================================",
                            "=================");
    
          productObservable.Subscribe(prod => Console.WriteLine(prod.ToString()));
    
    
          Console.WriteLine("\nPress ENTER to exit...\n");
          Console.ReadLine();
        }
    
    
    
        //**************************************************************************************************//
        //***                                                                                            ***//
        //*** The Product class holds current product inventory information and includes the ability for ***//
        //*** each product to display its information to the console window.                             ***//
        //***                                                                                            ***//
        //**************************************************************************************************//
        class Product
        {
          private readonly string productName;
          private readonly string productID;
          private int currentInventory;
    
          public Product(string name, int inventory)
          {
            productName = name;
            productID = Guid.NewGuid().ToString();
            currentInventory = inventory;
          }
    
          public void RemoveInventory(int delta)
          {
            currentInventory -= delta;
    
            if (currentInventory < 0)
              currentInventory = 0;
          }
    
          public override string ToString()
          {
            return String.Format("{0,-13} {1,-37} {2,-18}", productName, productID, currentInventory);
          }
        }
    
    
    
        //*****************************************************************************************************//
        //***                                                                                               ***//
        //*** The ProductInventory class initializes all our product information and provides an Observable ***//
        //*** sequence of the product inventories through the GetUpdatedInventory() method. This method     ***//
        //*** is provided to our call to Observable.Defer() so that all subscriptions against the deferred  ***//
        //*** observable get the lastest inventory information whenever Subscribe is called.                ***//
        //***                                                                                               ***//
        //*****************************************************************************************************//
        class ProductInventory
        {
          private Product[] products = new Product[5];
          private Random random = new Random();
    
          public ProductInventory()
          {
            for (int i = 0; i < 5; i++)
            {
              //*************************************************************//
              //*** Initial inventories will be a random count under 1000 ***//
              //*************************************************************//
              products[i] = new Product("Product " + (i + 1).ToString(), random.Next(1000));
            }
          }
    
          public IObservable<Product> GetUpdatedInventory()
          {
            //***************************************************************************************************//
            //*** When inventory for each product is updated up to 50 of each product is consumed or shipped. ***//
            //***************************************************************************************************//
            for (int i = 0; i < 5; i++)
              products[i].RemoveInventory(random.Next(51));
    
            //****************************************************************************************************//
            //*** This updated observable sequence is always provided by this method when Subscribe is called. ***//
            //****************************************************************************************************//
            IObservable<Product> updatedProductSequence = products.ToObservable();
            return updatedProductSequence;
          }
        }
      }
    }

The following is example output from the example code.

    Current Inventory...
    
    
    Product Name  Product ID                            Current Inventory
    ============  ====================================  =================
    Product 1     04e76657-c403-4208-a300-a3ba42fbe218  808
    Product 2     3bc7f823-6624-4803-b673-ec2e7d8802b7  114
    Product 3     1e5755f3-6301-4faa-8e1b-35765dc73bce  2
    Product 4     f691ddef-b679-42a2-99c7-83651fbf1cc5  894
    Product 5     df1f331b-8a52-4a54-a2fb-63d69dda6c1a  467
    
    
    The updated current Inventory...
    
    
    Product Name  Product ID                            Current Inventory
    ============  ====================================  =================
    Product 1     04e76657-c403-4208-a300-a3ba42fbe218  761
    Product 2     3bc7f823-6624-4803-b673-ec2e7d8802b7  81
    Product 3     1e5755f3-6301-4faa-8e1b-35765dc73bce  0
    Product 4     f691ddef-b679-42a2-99c7-83651fbf1cc5  890
    Product 5     df1f331b-8a52-4a54-a2fb-63d69dda6c1a  440
    
    Press ENTER to exit...

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









