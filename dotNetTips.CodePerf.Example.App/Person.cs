// ***********************************************************************
// Assembly         : dotNetTips.CodePerf.Example.App
// Author           : David McCarter
// Created          : 02-14-2018
//
// Last Modified By : David McCarter
// Last Modified On : 08-23-2018
// ***********************************************************************
// <copyright file="Person.cs" company="dotNetTips.com - McCarter Consulting">
//     2018 David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace dotNetTips.CodePerf.Example
{
    /// <summary>
    /// Class Person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// The identifier
        /// </summary>
        private Guid _id;
        /// <summary>
        /// The email
        /// </summary>
        private string _email;
        /// <summary>
        /// The first name
        /// </summary>
        private string _firstName;
        /// <summary>
        /// The last name
        /// </summary>
        private string _lastName;
        /// <summary>
        /// The born on
        /// </summary>
        private DateTime _bornOn;

        /// <summary>
        /// Initializes a new instance of the <see cref="Person" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="email">The email.</param>
        public Person(Guid id, string email)
        {
            Id = id;
            Email = email;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get => _id; private set => _id = value; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get => _email; private set => _email = value; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get => _firstName; set => _firstName = value; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get => _lastName; set => _lastName = value; }

        /// <summary>
        /// Gets or sets the born on.
        /// </summary>
        /// <value>The born on.</value>
        public DateTime BornOn { get => _bornOn; set => _bornOn = value; }
    }

    /// <summary>
    /// Class PersonFixed.
    /// </summary>
    /// <seealso cref="System.IEquatable{dotNetTips.CodePerf.Example.PersonFixed}" />
    /// <seealso cref="System.IEquatable{dotNetTips.CodePerf.Example.App.PersonFixed}" />
    public class PersonFixed : IEquatable<PersonFixed>
    {
        /// <summary>
        /// The identifier
        /// </summary>
        private Guid _id;

        /// <summary>
        /// The email
        /// </summary>
        private string _email;

        /// <summary>
        /// The first name
        /// </summary>
        private string _firstName;

        /// <summary>
        /// The last name
        /// </summary>
        private string _lastName;

        /// <summary>
        /// The born on
        /// </summary>
        private DateTime _bornOn;

        /// <summary>
        /// Initializes a new instance of the <see cref="Person" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="email">The email.</param>
        public PersonFixed(Guid id, string email)
        {
            Id = id;
            Email = email;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get => _id; private set => _id = value; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get => _email; private set => _email = value; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get => _firstName; set => _firstName = value; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get => _lastName; set => _lastName = value; }

        /// <summary>
        /// Gets or sets the born on.
        /// </summary>
        /// <value>The born on.</value>
        public DateTime BornOn { get => _bornOn; set => _bornOn = value; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as PersonFixed);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(PersonFixed other)
        {
            return other != null && (Id.Equals(other.Id) && Email.Equals(other.Email));
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            var hashCode = -1058553241;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            return hashCode;
        }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="fixed1">The fixed1.</param>
        /// <param name="fixed2">The fixed2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(PersonFixed fixed1, PersonFixed fixed2)
        {
            return EqualityComparer<PersonFixed>.Default.Equals(fixed1, fixed2);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="fixed1">The fixed1.</param>
        /// <param name="fixed2">The fixed2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(PersonFixed fixed1, PersonFixed fixed2)
        {
            return !(fixed1 == fixed2);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
