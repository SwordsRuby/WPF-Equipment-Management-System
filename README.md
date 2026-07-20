```markdown
# Equipment Management System

## Описание проекта
Проект представляет собой десктопное приложение для управления базой данных оборудования. Приложение разработано на **C#** с использованием **WPF** и **Entity Framework**. Оно позволяет авторизоваться, просматривать, добавлять, редактировать и удалять записи об оборудовании, а также выполнять поиск по ключевым словам.

---

## Функциональные возможности
- **Авторизация** — вход в систему по логину и паролю (только для администраторов).
- **Просмотр оборудования** — отображение списка оборудования с фото, названием, ценой и типом.
- **Поиск** — фильтрация оборудования по названию, цене или типу.
- **Добавление** — добавление нового оборудования с возможностью загрузки фото.
- **Редактирование** — изменение данных существующего оборудования.
- **Удаление** — удаление оборудования из базы данных.
- **Выход** — завершение работы приложения.

---

## Технологии
- **Язык**: C# (WPF)
- **ORM**: Entity Framework 6 (Code First)
- **База данных**: SQL Server (LocalDB)
- **Формат данных**: XAML, XML
- **Шаблоны**: T4 (генерация кода из EDMX)

---

## Структура проекта

```
End_Project/
├── DB/
│   ├── admin_table.cs          # Модель администратора
│   ├── tech_table.cs           # Модель оборудования
│   ├── tech_type_table.cs      # Модель типов оборудования
│   ├── ModelDB.edmx            # EDMX-модель базы данных
│   ├── ModelDB.Context.cs      # Контекст базы данных
│   └── ModelDB.tt              # T4-шаблон для генерации кода
├── Pages/
│   ├── authorization_page.xaml # Страница авторизации
│   ├── authorization_page.xaml.cs
│   ├── view_page.xaml          # Страница просмотра оборудования
│   └── view_page.xaml.cs
├── Windows/
│   ├── add_window.xaml         # Окно добавления оборудования
│   ├── add_window.xaml.cs
│   ├── edit_window.xaml        # Окно редактирования оборудования
│   └── edit_window.xaml.cs
├── Images/                     # Ресурсы (иконки, фон)
├── MainWindow.xaml             # Главное окно с Frame
├── MainWindow.xaml.cs
├── App.xaml                    # Ресурсы приложения
├── App.xaml.cs
├── App.config                  # Строка подключения к БД
├── packages.config             # NuGet-пакеты
└── README.md
```

---

## Установка и запуск

1. **Клонировать репозиторий**:
   ```bash
   git clone https://github.com/SwordsRuby/WPF-Equipment-Management-System.git
   ```

2. **Открыть решение** в Visual Studio (файл `.sln`).

3. **Восстановить NuGet-пакеты** (Entity Framework, SQL Server).

4. **Настроить базу данных**:
   - Убедитесь, что SQL Server LocalDB установлен.
   - Строка подключения находится в `App.config`.
   - База данных `TechnicalDB` должна быть создана, либо выполните миграцию.

5. **Запустить приложение** (F5).

---

## Использование

1. **Авторизация**:
   - Введите логин и пароль (4 цифры).
   - По умолчанию данные проверяются по таблице `admin_table`.

2. **Просмотр**:
   - На главной странице отображаются карточки оборудования.
   - Каждая карточка содержит фото, название, цену, тип и кнопки "Edit"/"Delete".

3. **Поиск**:
   - В поле "Search" введите ключевое слово (разделяйте пробелами для нескольких).
   - Поиск работает по названию, цене и типу.

4. **Добавление**:
   - Нажмите "Add" → заполните поля → выберите фото → "Add entry".

5. **Редактирование**:
   - Нажмите "Edit" на карточке → измените данные → "Edit entry".

6. **Удаление**:
   - Нажмите "Delete" → подтвердите удаление.

---

## Конфигурация

**Строка подключения** (в `App.config`):
```xml
<connectionStrings>
  <add name="TechnicalDBEntities" 
       connectionString="metadata=res://*/DB.ModelDB.csdl|res://*/DB.ModelDB.ssdl|res://*/DB.ModelDB.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(localdb)\localdb;initial catalog=TechnicalDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" 
       providerName="System.Data.EntityClient" />
</connectionStrings>
```

**Стили кнопок** — определены в `App.xaml`.

---

## Зависимости
- **.NET Framework 4.7.2**
- **Entity Framework 6.2.0**
- **SQL Server LocalDB**
