namespace rename
{
    using System;
    using System.Collections.Generic;
    using System.IO;


    public interface IAssignableSource { }

    public class MyAttr : Attribute
    {

        public string Name { get; set; }
        public MyAttr(string name)
        {
            Name = name;
        }
    }

    [MyAttr("Carl")]
    public class OSHelper : IAssignableSource
    {
        [MyAttr("lied")]
        public void Rename(string inputDir, string oldString, string newString)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(inputDir);
                var filter = new List<string>() { ".git", ".vs" };
                if (filter.Contains(dirInfo.Name)) return;

                if (dirInfo.Name.Contains(oldString))
                {
                    var name = dirInfo.Name.Replace(oldString, newString);
                    if (!string.IsNullOrEmpty(name))
                    {
                        var newDirName = dirInfo.Parent.FullName + "\\" + name;
                        dirInfo.MoveTo(newDirName);
                    }
                    else Console.WriteLine("Empty name in directory path after replacement!");
                }


                var files = Directory.GetFiles(dirInfo.FullName);
                foreach (var file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    var content = File.ReadAllText(file);
                    if (content.Contains(oldString))
                        File.WriteAllText(file, content.Replace(oldString, newString));

                    if (fileInfo.Name.Contains(oldString))
                    {
                        var newName = fileInfo.Name.Replace(oldString, newString);
                        if (!string.IsNullOrEmpty(newName))
                            fileInfo.MoveTo(fileInfo.Directory.FullName + "\\" + newName);
                        else Console.WriteLine("Empty name in file path after replacement!");
                    }
                }


                var dirs = Directory.GetDirectories(dirInfo.FullName);
                if (dirs.Length > 0)
                {
                    foreach (var dir in dirs)
                    {
                        Rename(dir, oldString, newString);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
