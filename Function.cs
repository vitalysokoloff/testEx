using System;
using System.Reflection;
using System.IO;

namespace testEx
{
    public static class Utils
    {
        public static string GetNames(string path)
        {
            string names = ""; 
            // Получаем файлы в папке
            string [] fileEntries = Directory.GetFiles(path, "*.dll");

            for (int k = 0; k < fileEntries.Length; k++ )
            {
                names += fileEntries[k] + "\n";
                Assembly assembly = Assembly.LoadFrom(fileEntries[k]);
                Type [] types = assembly.GetTypes();

                // Заполняем строку именами классов
                for (int i = 0; i < types.Length; i++)
                {
                    names += types[i].Name + "\n";

                    // Получаем методы класса
                    MethodInfo[] methods = types[i].GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    // Заполняем строку именами методов
                    for (int j = 0; j < methods.Length; j++)
                    {
                        // Отфильтровываем только публичные и протектед
                        if (methods[j].IsFamily || methods[j].IsPublic)
                            names += "- " + methods[j].Name + "\n";
                    }
                }
            }
            return names;
        }
    }
}
