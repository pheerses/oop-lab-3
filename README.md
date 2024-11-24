
# Магазин (Лабораторная работа №3)
---

Цели лабораторной работы:
- Реализовать приложение для управления магазинами, товарами и их связями.
- Организовать проект с разделением на логические слои:
  - Core: Модели данных и интерфейсы.
  - DAL: Слой доступа к данным (реализация через SQLite и файлы).
  - BLL: Бизнес-логика.
  - Client: Консольное приложение для запуска и проверки функциональности.

---

Функциональность:
- Создание магазинов и товаров.
- Добавление товаров в магазин (указание цены и количества).
- Поиск магазина, где товар самый дешёвый.
- Определение товаров, доступных для покупки на заданную сумму.
- Покупка партии товаров в магазине.
- Поиск магазина с минимальной стоимостью для покупки набора товаров.

---

Структура проекта:
```
oop-lab-3/
├── MyShop.sln             # Решение
├── Client/                # Консольное приложение
│   ├── Client.csproj
│   ├── Program.cs
│   └── appsettings.json   # Конфигурационный файл
├── Core/                  # Модели и интерфейсы
│   ├── Core.csproj
│   ├── Models/
│   │   ├── Store.cs
│   │   ├── Product.cs
│   │   └── StoreProduct.cs
│   └── Interfaces/
│       ├── IStoreRepository.cs
│       ├── IProductRepository.cs
│       └── IStoreProductRepository.cs
├── BLL/                   # Бизнес-логика
│   ├── BLL.csproj
│   └── Services/
│       ├── IShopService.cs
│       └── ShopService.cs
├── DAL/                   # Слой доступа к данным
│   ├── DAL.csproj
│   ├── ShopDbContext.cs
│   ├── FileRepositories/  # Реализация через файлы
│   ├── DatabaseRepositories/  # Реализация через SQLite
│   └── Migrations/        # Миграции EF
├── .gitignore            
└── README.md             
```

---

Запуск:

1. Установить зависимости:
```
dotnet restore
```

2. Создать миграции и обновить базу данных:
```
dotnet ef migrations add InitialCreate -p DAL -s Client
dotnet ef database update -p DAL -s Client
```

3. Запустить приложение:
```
dotnet run --project Client
```

---

Технологии и библиотеки:
- C#
- .NET 8.0
- Entity Framework Core
- SQLite
- Microsoft.Extensions.DependencyInjection для DI.
