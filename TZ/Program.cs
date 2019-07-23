using System;
using System.Xml;
using System.IO;

namespace TZ
{
    /// <summary>
    /// Тестовое задание 3
    /// Разработать консольное приложение которое выполняет функции: 
    /// 1.	Запрашивает у пользователя название города 
    /// 2.	Запрашивает информацию о погоде с сайта https://openweathermap.org/current 
    /// 3.	Выводит в консоль значение температуры, влажности, заката, восхода в понятной для человека форме
    /// 4.	Сохраняет в текстовый файл полученные значения с текущей датой в названии файла
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя города");
            var city = Console.ReadLine();
            string url = "http://api.openweathermap.org/data/2.5/weather?q="+city+ ",ru&APPID=bf6d3d1bebde9b7184a65a9cb14f28db&lang=ru&mode=xml";
            XmlTextReader reader = new XmlTextReader(url);
            string date =Convert.ToString(DateTime.Now);
            date = date.Replace(":", "_").Replace(" ","_").Replace(".","_");
            date = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+@"\"+date+".txt";
            using (StreamWriter sw = new StreamWriter(date, false, System.Text.Encoding.Default))
            {
                while (reader.Read())
                {
                    if (reader.HasAttributes)
                    {
                        if(reader.Name == "temperature")
                        {
                            double i = Convert.ToDouble(reader[0].Replace('.',',')) - 273;
                            Console.WriteLine("Температура на данный момент равна  {0} 'C",i);
                            sw.WriteLine("Температура на данный момент равна  {0} 'C", i);
                        }

                        if (reader.Name == "humidity")
                        {
                            Console.WriteLine("Влажность воздуха составляет {0} %", reader[0]);
                            sw.WriteLine("Влажность воздуха составляет {0} %", reader[0]);
                        }
                       
                        if (reader.Name == "sun")
                        {
                            Console.WriteLine("Закат  {0}\nВосход {1}", reader[1],reader[0]);
                            sw.WriteLine("Закат  {0}\nВосход {1}", reader[1], reader[0]);
                        }

                        reader.MoveToElement();
                    }
                }
                Console.ReadKey();
            }
        }
    }
}
