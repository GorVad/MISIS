
![Alt-текст](https://i.imgur.com/Zmt3yu7.png "Изображение приложения")
## Build
Please make sure you have the following prerequisites:
- A desktop platform with the .NET Framework 4.7.2 SDK or higher installed.
- When working with the codebase, we recommend using an IDE with intelligent code completion and syntax highlighting, such as Visual Studio 2019+, JetBrains Rider or Visual Studio Code.

## Стандартные библиотеки

- System
- System.Data
- System.Linq
- System.Windows
- System.Windows.Controls
- System.Windows.Media
- System.Windows.Shapes
- System.Collections.Generic
- System.ComponentModel
- System.Runtime.CompilerServices

Эти библиотеки являются стандартными для C# и не требуют дополнительной установки.

## Дополнительные библиотеки

Для обеспечения функционирвания используется ряд сторонних библиотек, которые можно установить через менеджер пакетов _NuGet_ в Visual Studio.
 
## [MySql.Data] [MySql.Data]
![alt text](https://api.nuget.org/v3-flatcontainer/mysql.data/8.0.23/icon)

> Библиотека предоставляет коннектор к MySql базам данных
> Когда соединение с установлено, позволяет вам выполнять операции с базой данных. 
> Эту задачу можно решить с помощью объекта MySqlCommand.
> После того, как он был создан, вы можете вызвать три основных метода:
> ExecuteReader для запроса базы данных.
> ExecuteNonQuery для вставки, обновления и удаления данных.
> ExecuteScalar, чтобы вернуть одно значение.

В нашем проекте используется версия 8.0.23

## [xunit] [xunit]


> Фреймворк для тестирования, цель которой заключается в максимальной простоте и согласовании с функциями платформы.При установке этого пакета устанавливаются xunit.core, xunit.assert и xunit.analyzers.

В нашем проекте используется версия 2.4.1

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [MySql.Data]: <https://www.nuget.org/packages/MySql.Data/8.0.23?_src=template>
   [xunit]: <https://github.com/xunit/xunit>
 


## Downloading the source code
Clone the repository:
```
git clone https://github.com/GorVad/GOOSLE
```
To update the source code to the latest commit, run the following command inside the Goosle directory:
```
git pull 
```


