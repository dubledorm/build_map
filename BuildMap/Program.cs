﻿using System.Net;
using System.Text;
using BuildMap;

HttpListener server = new HttpListener();
// установка адресов прослушки
server.Prefixes.Add($"http://127.0.0.1:8888/{RequestHandler.base_url}/");//http://127.0.0.1:8888/mapping/
while (true)
{
    server.Start(); // начинаем прослушивать входящие подключения

    // получаем контекст
    var context = await server.GetContextAsync();
    RequestHandler request_handler = new RequestHandler(context.Request);

    var response = context.Response;
    // отправляемый в ответ код htmlвозвращает
    string responseText = request_handler.handler();
    //@"<!DOCTYPE html>
    //<html>
    //    <head>
    //        <meta charset='utf8'>
    //        <title>METANIT.COM</title>
    //    </head>
    //    <body>
    //        <h2>Hello METANIT.COM</h2>
    //    </body>
    //</html>";
    byte[] buffer = Encoding.UTF8.GetBytes(responseText);
    // получаем поток ответа и пишем в него ответ
    response.ContentLength64 = buffer.Length;
    using Stream output = response.OutputStream;
    // отправляем данные
    await output.WriteAsync(buffer);
    await output.FlushAsync();

    Console.WriteLine("Запрос обработан");
}
server.Stop();
//using System;
//using System.IO;

//namespace BuildMap;

//internal class Program
//{
//    static void Main(string[] args)
//    {
//        Building building = new Building(1);
//        //PointIdSMapping p = new PointIdSMapping(building.roads);
//        PathFinder pathFinder = new PathFinder(building.roads);
//        List<Road> finded_path = pathFinder.find(1, 1);
//        Console.WriteLine();
//        PathPresenter pathPresenter = new PathPresenter(building, finded_path, 1);
//        Console.WriteLine(pathPresenter.toPoint());

//        foreach (Road road in finded_path)
//        {
//            Console.Write($"{road.idS} <-> {road.idE}({road.weight}) | ");
//        }
//        Console.WriteLine();
//    }
//}
// Качество - это субъективная оценка соответствия результата ожидания
// Эффективность - соотношение затрат к результату