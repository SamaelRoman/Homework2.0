using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Homework2._0
{
    /// <summary>
    /// Класс контекса базы данных(Файла)
    /// </summary>
    [Serializable]
    public sealed class ContextDB1
    {
        /// <summary>
        /// Путь к файлу в котором хранится сериализованный контекст БД(Включая название)
        /// </summary>
        public string PathToFileDB { get; set; }
        /// <summary>
        /// Список постояльцев.
        /// </summary>
        public List<User> Users { get; set; }
        /// <summary>
        /// Коллекция объектов ReservationInfos содержащих информацию о бронировании номеров.
        /// </summary>
        public List<ReservationInfo> ReservationInfos { get; set; }
        /// <summary>
        /// Коллекция возможных категорий номеров.
        /// </summary>
        public List<RoomCategory> RoomCategories { get; set; }
        /// <summary>
        /// Колекция номеров.
        /// </summary>
        public List<Room> Rooms { get; set; }
        /// <summary>
        /// Конструктор класса ContextDB1
        /// </summary>
        /// <param name="PathToFileDB">Путь к файлу в котором хранится сериализованный контекст БД</param>
        public ContextDB1(string PathToFileDB)
        {
            this.PathToFileDB = PathToFileDB;
            Users = new List<User>();
            ReservationInfos = new List<ReservationInfo>();
            RoomCategories = new List<RoomCategory>();
            Rooms = new List<Room>();
            if (!File.Exists(PathToFileDB))
            {
                using (File.Create(PathToFileDB)) { };  
            }
        }
        public ContextDB1()
        {

        }
        /// <summary>
        /// Обновляет контекст данных
        /// </summary>
        /// <returns>Возвращает объект класса ContextDB1 наполненный актуальными данными.</returns>
        public ContextDB1 RefreshChanges()
        {
            if (!File.Exists(PathToFileDB))
            {
                throw new Exception("Не найден файл базы данных!");
            }
            using (var DBFile = new FileStream(PathToFileDB,FileMode.Open,FileAccess.Read))
            {
                XmlSerializer ContextDB1Serializer = new XmlSerializer(typeof(ContextDB1));
                try
                {
                    return (ContextDB1)ContextDB1Serializer.Deserialize(DBFile);
                }
                catch (Exception)
                {

                    throw new Exception("Файл для хранения объектов пуст.");
                }
                
            }
        }
        /// <summary>
        /// Производит сохранение изменений над объектом контекста.
        /// </summary>
        public void SaveChanges()
        {
            if (!File.Exists(PathToFileDB))
            {
                throw new Exception("Не найден файл базы данных!");
            }
            using (var DBFile = new FileStream(PathToFileDB,FileMode.Create, FileAccess.Write))
            {
                XmlSerializer ContextDB1Serializer = new XmlSerializer(typeof(ContextDB1));
                ContextDB1Serializer.Serialize(DBFile, this);
            }
        }
        public void ClearAll()
        {
            ReservationInfos.Clear();
            RoomCategories.Clear();
            Rooms.Clear();
            Users.Clear();
        }

    }
}
