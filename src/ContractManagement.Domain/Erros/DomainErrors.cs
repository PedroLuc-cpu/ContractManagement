using ContractManagement.Domain.Shared;

namespace ContractManagement.Domain.Erros
{
    public static class DomainErrors
    {
        public static class Member
        {
            public static readonly Error InvalidCredentials = new(
                "Member.InvalidCredentials",
                "Member has invalid credentials"
                );
        }
        public static class Email
        {
            public static readonly Error Empty = new(
                "Email.Empty",
                "Email is empty.");

            public static readonly Error InvalidFormat = new(
                "Email.InvalidFormat",
                "Email format is invalid.");
        }

        public static class FirstName
        {
            public static readonly Error Empty = new(
                "FirstName.Empty",
                "First name is empty.");

            public static readonly Error TooLong = new(
                "LastName.TooLong",
                "FirstName name is too long.");
        }

        public static class LastName
        {
            public static readonly Error Empty = new(
                "LastName.Empty",
                "Last name is empty.");

            public static readonly Error TooLong = new(
                "LastName.TooLong",
                "Last name is too long.");
        }
        public static class Password
        {
            public static readonly Error Empty = new(
                "Password.Empty",
                "Password is empty.");
            public static readonly Error TooShort = new(
                "Password.TooShort",
                "Password is too short.");
        }

        public static class UserName
        {
            public static readonly Error Empty = new(
                "UserName.Empty",
                "User name is empty.");
            public static readonly Error TooLong = new(
                "UserName.TooLong",
                "User name is too long.");
        }
        public static class Role
        {
            public static readonly Error NameEmpty = new(
                "Role.NameEmpty",
                "Role name is empty.");
        }
        public static class Money
        {
            public static readonly Error NegativeValue = new(
                "Money.NegativeValue",
                "Monetary value cannot be negative.");
        }

        public static class Period
        {
            public static readonly Error NegativeValue = new(
                "Periodo.FimMaiorInicio",
                "O periodo do fim é maior que o inicio."
                );
        }

    }
}
