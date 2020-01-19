# SportsStore

Это решение – результат выполнения проекта интернет-магазина, требования к которому приведены в книге А. Фримена "ASP.NET Core MVC 2 с примерами на C# для профессионалов".

Перед запуском приложения нужно:
- Установить JS-библиотеки с помощью Library Manager, запустив из консоли `dotnet tool run libman restore`
- Создать базу данных, запустив из консоли `dotnet dotnet-ef database update --context AppDbContext` и `dotnet dotnet-ef database update --context AppIdentityDbContext`

Страница администратора доступна по адресу */Admin/Index* (имя пользователя *Admin*, пароль *Secret123$*).
