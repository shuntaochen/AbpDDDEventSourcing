using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace rename
{
    class Program
    {
        static void Main(string[] args)
        {

            //att();

            Console.WriteLine("Hello World!");
            var osHelper = new OSHelper();
            var quit = false;
            while (!quit)
            {
                Console.WriteLine("input directory path:");
                var input = Console.ReadLine();
                Console.WriteLine("old string:");
                var oldString = Console.ReadLine();
                Console.WriteLine("new string:");
                var newString = Console.ReadLine();
                osHelper.Rename(input, oldString, newString);
            }
        }

        private static void att()
        {
            var types = Assembly.GetExecutingAssembly();
            types.DefinedTypes.ToList().ForEach(o =>
            {
                if (o.IsAssignableFrom(typeof(IAssignableSource)))
                {

                    var at = o.GetCustomAttributes().FirstOrDefault(x => x.GetType().IsAssignableFrom(typeof(MyAttr)));
                    var y = at as MyAttr;
                    var name = y.Name;


                }

            });
        }
    }
}
