using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2._0
{
    [Serializable]
    public class RoomCategory
    {
        /// <summary>
        /// Уникальный идентификатор категории.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Комнты в опеределенной категории
        /// </summary>
        public List<Room> Rooms { get; set; }
        /// <summary>
        /// Конструктор класса RommCategory
        /// </summary>
        public RoomCategory()
        {
            
        }
        /// <summary>
        /// Конструктор класса RommCategory
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        public RoomCategory(string Name)
        {
            Id = Guid.NewGuid();
            this.Name = Name;
            Rooms = new List<Room>();
        }
    }
}
