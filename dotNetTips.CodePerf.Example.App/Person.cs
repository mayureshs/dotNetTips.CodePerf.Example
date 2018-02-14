using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetTips.CodePerf.Example.App
{
   internal class Person
    {
        private Guid _id;
        private string _email;
        private string _firstName;
        private string _lastName;
        private DateTime _bornOn;

        public Person(Guid id, string email)
        {
            Id = id;
            Email = email;
        }

        public Guid Id { get => _id; private set => _id = value; }
        public string Email { get => _email; private set => _email = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public DateTime BornOn { get => _bornOn; set => _bornOn = value; }
    }
}
