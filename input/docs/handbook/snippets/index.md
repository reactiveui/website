NoTitle: true
---
Snippets are short code templates that can be inserted into your code. They are used to reduce the amount of typing when writing repetitive code. The snippets are activated by writing the shortcut of the snippet and hitting **Tab** (**Tab, Tab** in Visual Studio).

The **snippets** folder in the [ReactiveUI repository](https://github.com/reactiveui/reactiveui/) on Github contains snippets for inserting common code when using ReactiveUI. There are snippets available for Visual Studio, Visual Studio for Mac, Visual Studio Code, JetBrains Resharper and JetBrains Rider.

All the ReactiveUI snippet shortcuts start with the letters **rui** making them easy to find when using auto complete in your editor of choice.

## Installing the snippets

### Visual Studio

You have 2 options.

1. Copy the snippets in the following directory:  
`C:\Users\<your-username>\Documents\Visual Studio 2022\Code Snippets\Visual C#\My Code Snippets\`

Or:

1. Store the snippets in a folder of your choice on your computer
1. Open Visual Studio
1. Open the **Tools** menu bar item
1. Click on **Code Snippets Manager**
1. Change language to **CSharp**
1. Click on **Add**
1. Navigate to the folder you stored the snippets in and click on **Select Folder**

### Visual Studio for Mac

1. Store the snippet files in the following folder: `~/Library/VisualStudio/<version>/Snippets`

### Visual Studio Code

1. Copy the contents of **ReactiveUI.json**
1. Open Visual Studio Code
1. Open **File** menu bar item (**Code** on MacOS)
1. Click on **Preferences** > **User snippets**
1. Paste contents inside the angle brackets (`{}`)

### JetBrains Resharper

1. Copy the contents of **RxUI.dotsettings**
1. Navigate to the **UserSettings.dotsettings** inside your project root folder
1. Paste the contents inside a `<LiveTemplatesManager></LiveTemplatesManager>` XML element

### JetBrains Rider

1. Store the snippet file in the following folder:
   1. **Windows**: `C:\Users\<your-username>\.Rider<version>\config\templates`
   1. **MacOS**: `~/Library/Preferences/Rider<version>/templates`
