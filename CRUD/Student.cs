using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    class Student
    {
        private int id;
        private string first_name;
        private string last_name;
        private string city;
        private string department;
        
        #region Encapsulation
        public int Id { get => id; set => id = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public string City { get => city; set => city = value; }
        public string Department { get => department; set => department = value; }
        #endregion Encapsulation

        // Constructur
        public Student() { }
        public Student(int id, string f_name, string l_name, string city, string dep)
        {
            this.Id = id;
            this.First_name = f_name;
            this.Last_name = l_name;
            this.City = city;
            this.Department = dep;
        }
    }
}
