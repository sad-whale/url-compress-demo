# url-compress-demo
Демо сервис сжатия ссылок

Реализован, используя asp net core, ef core и angularjs.

Для идентификации пользователя используется кука user_id. При её отсутствии, в нее записывается ГУИД.
Наверное, не самый лучший вариант, т.к. при очистке кук или при смене браузера, ранее сжатые ссылки будут недоступны.

Слои приложения по одтельным библиотека не разносил - деление на уровне папок и неймспейсов:
  - Commands, CommandHandlers, Persistance, Migrations, Queries - слой доступа к данным. Старался придерживаться принципа CQRS
  - Services, Models, ErrorHandling, Controllers - логика приожения
  - wwwroot/app - фронт-енд. Так как это тестовый проект, много времени интерфейсу не уделял
