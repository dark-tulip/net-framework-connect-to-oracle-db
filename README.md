# net-framework-connect-to-oracle-db
May help to find the way of connection to remote oracle-db using C#

## 1. Подключение к удаленной БД Оракл с Net Framework и извлечение данных с использованием LINQ (Language integrated query)

Подключаясь к базе данных ОРАКЛ (удаленному серверу), с помощью ```NUGET```. Скачиваем необходимые зависимости, добавляем строку подключения

### 1.1 Поднимаем удаленную базу данных оракла

Настраиваем конфиги файла ```TNSNAMES.ORA``` на клиенте, запускаем удаленную базу данных, записываем ip адрес для подключения

![image](https://user-images.githubusercontent.com/89765480/170867836-16aa48f2-1724-476f-9963-7eb88d9d0045.png)


### 1.2 Создаем приложение Windows Forms – (.NET Framework)

![image](https://user-images.githubusercontent.com/89765480/170867805-c5febab1-81b8-46fe-929a-34b0a3d8e4f8.png)


### 1.3 Application configuration

В файл ```App.config``` – добавляем новый конфиг – строку подсоединения, адрес, порт, протокол, имя БД, пользователя и пароль, или все что было в файле TNS names.ora
```
<connectionStrings>
	<add name="con" connectionString="Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.252.129)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=hr;Password=123;" />
</connectionStrings>
```


### 1.4. Скачиваем зависимости

Скачиваем зависимости – Ссылки – Управление пакетами NuGet
Oracle Managed Data Access

![image](https://user-images.githubusercontent.com/89765480/170867880-45c967c7-529b-456a-b5fc-35d6bd5a9c51.png)


### 1.5 Виды запуска приложения Windows Forms

Чтобы смотреть результат из консоли тоже, нужно поменять тип приложения на Консольное приложение (форма также будет выходить при каждом запуске)

![image](https://user-images.githubusercontent.com/89765480/170867965-97b1b455-061b-45fa-9247-cb27a550b87f.png)


### 1.6 Основные структуры хранения данных из таблицы

Приступаем к выводу данных из таблицы. Немного терминов про использованные в программе структуры данных

* ```Data Table``` - Представляет одну таблицу данных в памяти
* При создании DataTable необходимо определить ее схему путем добавления ```DataColumn``` объектов в DataColumnCollection объект (доступ к которым выполняется через Columns свойство)
* Чтобы добавить строки в объект  ```DataTable```, необходимо сначала использовать NewRow метод для возврата нового ```DataRow``` объекта
* ```OracleDataAdapter``` заполненяет объект ```DataSet```, создает необходимые таблицы и столбцы для возвращаемых данных. Также служит связующим звеном между набором данных и базой для их извлечения и сохранения

### 1.7 Добавляем элементы на форму

![image](https://user-images.githubusercontent.com/89765480/170867918-59df2f39-1702-4032-bfae-1cc7fad2e443.png)


Из панели инструментов настраиваем GUI приложения


## 2. Извлечение данных

### 2.1 Стандартный подход вывода информации

![image](https://user-images.githubusercontent.com/89765480/170868108-68d76d76-9122-454a-8c35-fdfdb31dbc47.png)


Извлекаем каждый столбец отдельно, весь запрос строится в строковом выражении SQL Запроса

![image](https://user-images.githubusercontent.com/89765480/170868146-fc24b9f4-305a-4d3d-9d59-4d0c382c37e5.png)

### 2.2 С использованием LINQ (все остальные примеры приведены в файле Form1.cs)

![image](https://user-images.githubusercontent.com/89765480/170868259-ef45d4eb-548b-49cf-9b26-1d80b180bf77.png)


## 2.3 Генерация XML файла (по статичной выборке извлеченных данных)

![image](https://user-images.githubusercontent.com/89765480/170868339-ee5e5f97-54db-447c-be9e-ce5a9422bf6d.png)


Выводит только те департаменты где в названиях которых есть заглавная  буква А, дальше по усмотрению программы можно изменять LINQ запрос и генерировать XML file


![image](https://user-images.githubusercontent.com/89765480/170868375-b82ee0dc-b12f-4951-8222-d56cb7a457dc.png)




