# mysql-web-api
Выполнение тестового задания:


Тестовое задание C# Asp.net (Linux)

Создать сервер с http json api (C#, asp.net, Linux), позволяющий получать и добавлять записи в таблицу Товары (MySql). 
Структура таблицы:
ID
Наименование товара

Пример запроса GET (получить список товаров):
http://178.57.218.210:98/items?token=TS3qVh70xrM59VC9OxqK3UZV

Пример запроса POST (добавление товара):
curl -X POST "http://178.57.218.210:98/items?token=TS3qVh70xrM59VC9OxqK3UZV" -H "accept: application/json" -H "Content-Type: application/json-patch+json" -d "{ \"name\": \"Новый товар\",}"


15.07.2020
