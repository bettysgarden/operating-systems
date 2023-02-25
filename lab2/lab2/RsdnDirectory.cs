using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace os2
{
    public class RsdnDirectory
    {
        /// <summary>
        /// Формирует путь, требуемый функцией FindFirstFile()
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string MakePath(string path)
        {
            return Path.Combine(path, "*");
        }
        /// <summary>
        /// Возвращает список файлов или каталогов находящихся по заданному пути path. 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isGetDirs"></param>
        /// Если true -- функция возвращает список каталогов, иначе файлов
        /// <returns> Список файлов или каталогов. </returns>
        private static IEnumerable<string> GetInternal(string path, bool isGetDirs)
        {
            WIN32_FIND_DATA findData; // найденная информация -- файл или каталог только для чтения
            // получаем информацию о текущем файле и дескриптор перечислителя.
            // Этот дескриптор требуется передавать функции FindNextFile() для получения след файлов
            IntPtr findHandle = FindFirstFile(MakePath(path), out findData);

            // если произошла ошибка нужно вынуть информацию об ошибке и перепаковать ее в исключение
            try
            {
                if (findHandle == INVALID_HANDLE_VALUE)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            catch (Win32Exception exception)
            {
                FindClose(findHandle);
            }

            try
            {
                do
                    if (isGetDirs // если нам нужны директории 
                            ? (findData.dwFileAttributes & // Атрибуты искомого файла.
                               FileAttributes.Directory) != 0
                            : (findData.dwFileAttributes &
                               FileAttributes.Directory) == 0)
                        yield return findData.cFileName;
                while (FindNextFile(findHandle, out findData));
            }
            finally
            {
                FindClose(findHandle);
            }
        }
        /// <summary>
        /// Возвращает список файлов для некоторого пути.
        /// </summary>
        /// <param name="path">
        /// Каталог, для которого нужно получить список файлов.
        /// </param>
        /// <returns> Список файлов каталога. </returns>
        public static IEnumerable<string> GetFiles(string path)
        {
            return GetInternal(path, false);
        }
        /// <summary>
        /// Возвращает список каталогов для некоторого пути.
        /// !! Функция не перебирает вложенные подкаталоги.
        /// </summary>
        /// <param name="path">
        /// Каталог, для которого нужно получить список подкаталогов.
        /// </param>
        /// <returns>Список подкаталогов каталога.</returns>
        public static IEnumerable<string> GetDirectories(string path)
        {
            return GetInternal(path, true);
        }
        /// <summary>
        /// Возвращает список относительных путей ко всем подкаталогам (в том числе вложенным) для заданного пути.
        /// </summary>
        /// <param name="path">
        /// Путь, для которого нужно получить подкаталоги.
        /// </param>
        /// <returns>Список подкаталогов.</returns>
        public static IEnumerable<string> GetAllDirectories(string path)
        {
            // сначала перебираем подкаталоги первого уровня вложенности
            foreach (var subDir in GetDirectories(path))
            {
                // игнорируем имя текущего каталога и родительского
                if (subDir == ".." || subDir == ".")
                    continue;

                // комбинируем базовый путь и имя подкаталога
                string relativePath = Path.Combine(path, subDir);

                // возвращаем пользователю относительный путь
                yield return relativePath;

                // создаем рекурсивно итератор для каждого подкаталога и возвращаем каждый его элемент в качестве элементов текущего итератора
                // этот прием позволяет обойти ограничение итераторов...
                foreach (var subDir2 in GetAllDirectories(relativePath))
                {
                    yield return subDir2;
                }
            }
        }

        public static IEnumerable<string> GetAllFiles(string path)
        {
            // возвращаем файлы из текущего каталога
            foreach (var file in GetFiles(path))
            {
                yield return file;
            }

            //проходимся по подкаталогам и возвращаем файлы из них
            foreach (var subDir in GetDirectories(path))
            {
                // игнорируем имя текущего каталога и родительского
                if (subDir == ".." || subDir == ".")
                    continue;

                // комбинируем базовый путь и имя подкаталога
                string relativePath = Path.Combine(path, subDir);


                foreach (var file in GetAllFiles(relativePath))
                {
                    yield return file;
                }


            }
        }

        #region Импорт из kernel32

        private const int MAX_PATH = 256;

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        [BestFitMapping(false)]

        // Структура WIN32_FIND_DATA описывает файл, найденный функцией FindFirstFile, FindFirstFileEx или FindNextFile.
        private struct WIN32_FIND_DATA
        {
            public FileAttributes dwFileAttributes; // Атрибуты найденного файла
            public FILETIME ftCreationTime; // Время создания файла
                                            // Структура FILETIME, которая устанавливается, когда файл или каталог создавались.
                                            // Если лежащая в основе файловая система не поддерживает время создания,
                                            // этот член структуры равняется нулю.            
            public FILETIME ftLastAccessTime; // Время последнего доступа к файлу
                                              // Структура FILETIME. Для файла, структура устанавливает, когда последний раз читали из или писали в него или, в случае,
                                              // если он исполняемый файл, запускали его. Для каталога, структура определяет, когда создавался каталог.
                                              // Если лежащая в основе файловая система не поддерживает время последнего обращения к файлу, член структуры ftLastAccessTime равняется нулю.
                                              // В файловой системе FAT, указанная дата и файлов и каталогов должен быть исправляет, но время дня будет всегда устанавливаться в полночь.                                 
            public FILETIME ftLastWriteTime;  // Время последней модификации файла
                                              // Структура FILETIME. Для файла, структура устанавливает, когда последний раз была запись в файл, он обрезался или переписывался
            public int nFileSizeHigh; // Старшее  двойное слово (DWORD) значения размера файла, в байтах.
                                      // Это значение равняется нулю, если размер файла не больше, чем определяет его MAXDWORD. 
            public int nFileSizeLow; // Младшее двойное слово (DWORD) значения размера файла, в байтах.
            public int dwReserved0; // Если член структуры dwFileAttributes включает атрибут FILE_ATTRIBUTE_REPARSE_POINT, этот член структуры устанавливает тэг монтирования.
                                    // В противном случае, это значение не определяется и не должно использоваться.
            public int dwReserved1;// Зарезервированный для будущего использования.

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string cFileName; // Символьная строка с нулем в конце, которая устанавливает имя файла.

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternate; // Символьная строка с нулем в конце, которая устанавливает альтернативное имя файла.
                                      // Это имя является в классическом формате имени файла 8.3 (имя файла.расширение).

        }

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);
        // Функция FindFirstFile() создает дескриптор поиска и фозвращает информацию lpFindFileData о первом файле,
        // имя которого соответсвует указанному образцу в lpFileName (указатель на символьную строку с нулем в конце, которая определяет правильный каталог или путь
        // и имя файла, которое может содержать символы подстановки * и ?)

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool FindNextFile(IntPtr hFindFile, out WIN32_FIND_DATA lpFindFileData);
        // Функция FindNextFile() продолжает поиск в том же каталоге, получая от функции FindFirstFile() дескриптор поиска hFindFile,
        // и возвращая информацию об очередном файле в lpFindFileData

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool FindClose(IntPtr hFindFile);

        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        #endregion

    }
}