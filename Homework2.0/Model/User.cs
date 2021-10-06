using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2._0
{
    /// <summary>
    /// Класс постояльцев гостиницы.
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        ///  Уникальный идентификатор.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия.
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Отчество.
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// ФИО.
        /// </summary>
        public string FullName { get { return ($"{Surname} {Name} {Patronymic}"); } }
        /// <summary>
        /// Номер паспорта.
        /// </summary>
        public string PassportID {get;set; }
        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime DOB { get; set; }
        /// <summary>
        /// Коллекция объектов ReservationInfos содержащих информацию о бронировании номеров.
        /// </summary>
        public List<ReservationInfo> ReservationInfos { get; set; }
        /// <summary>
        /// Конструктор класса User
        /// </summary>
        /// <param name="Name">Имя.</param>
        /// <param name="Surname">Фамилия.</param>
        /// <param name="Patronymic">Отчество.</param>
        /// <param name="PassportID">Серия-номер паспорта.</param>
        /// <param name="DOB">Дата рождения.</param>
        public User(string Name,string Surname, string Patronymic,string PassportID,DateTime DOB)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Surname = Surname;
            this.Patronymic = Patronymic;
            this.PassportID = PassportID;
            this.DOB = DOB;
            ReservationInfos = new List<ReservationInfo>();
        }
        public User()
        {
        }
    }
}
