using System.Collections.Generic;
using System.Threading;
using os2;
using System.Linq;

namespace OS2_
{
    public class Task
    {
        public Thread thread; // поток откуда запускаем
        private string path; //путь к директории
        public List<string> Directories = new List<string>();
        public Task(string path)
        {
            this.path = path;
        }

        private void GetAllDirectories()
        {
            List<string> subDirs = RsdnDirectory.GetDirectories(path).Skip(2).ToList();
            if (subDirs.Count() == 0)
            {
                Directories.Add(path);
            }
            foreach (var dir in RsdnDirectory.GetAllDirectories(path))
            {
                subDirs = RsdnDirectory.GetDirectories(dir).Skip(2).ToList();
                if (subDirs.Count() == 0)
                {
                    Directories.Add(dir);
                }
            }
        }

        //запуск потока
        public void Start()
        {
            thread = new Thread(GetAllDirectories); //создаем поток, передаем метод получения всех каталогов
            thread.Start();
        }
    }
}