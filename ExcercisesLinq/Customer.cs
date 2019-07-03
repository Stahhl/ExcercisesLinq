using System;
using System.Collections.Generic;
using System.Text;

public enum Gender
{
    NULL,
    Female,
    Male,
    Other
}
public enum PlayTennis
{
    NULL,
    Never,
    Once,
    Seldom,
    Yearly,
    Monthly,
    Weekly,
    Daily,
    Often,
}
public enum LikeFruit
{
    NULL,
    True,
    False,
}

namespace ExcercisesLinq
{
    public class Customer
    {
        public Customer
        #region constructor
            (
            int Id,
            string FirstName,
            string LastName,
            string Email,
            Gender Gender,
            int Age,
            PlayTennis PlayTennis,
            LikeFruit LikeFruit,
            string Ip
            )
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Gender = Gender;
            this.PlayTennis = PlayTennis;
            this.LikeFruit = LikeFruit;
            this.Age = Age;
            this.Ip = Ip;

            this.FullName = FirstName + " " + LastName;
        }
        #endregion

        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public Gender Gender { get; private set; }
        public PlayTennis PlayTennis { get; private set; }
        public LikeFruit LikeFruit { get; private set; }
        public int Age { get; private set; }
        public string Ip { get; private set; }
    }
}
