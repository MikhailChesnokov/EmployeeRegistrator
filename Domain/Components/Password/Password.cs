namespace Domain.Components.Password
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Exceptions;



    public class Password : IComponent
    {
        private const int SaltLength = 40;

        private const int MinPasswordLength = 8;



        private static readonly HashAlgorithm HashAlgorithm = SHA256.Create();

        private static readonly Random Random = new Random();



        [Obsolete("Only for reflection", true)]
        public Password() { }

        public Password(string password)
        {
            SetSalt();
            SetPassword(password);
        }



        public byte[] Hash { get; protected set; }

        public byte[] Salt { get; protected set; }



        private void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            if (password.Length < MinPasswordLength)
                throw new TooShortPasswordException(
                    $"Длина пароля должна составлять не менее чем {MinPasswordLength} символов");

            if (Salt == null || Salt.Length != SaltLength)
                throw new InvalidOperationException("Incorrect salt");

            byte[] buffer = CreatePasswordSaltBuffer(password);

            Hash = HashAlgorithm.ComputeHash(buffer);
        }

        private void SetSalt()
        {
            Salt = new byte[SaltLength];
            Random.NextBytes(Salt);
        }



        public bool CheckPassword(string password)
        {
            byte[] passwordBuffer = CreatePasswordSaltBuffer(password);

            return HashAlgorithm.ComputeHash(passwordBuffer).SequenceEqual(Hash);
        }

        private byte[] CreatePasswordSaltBuffer(string password)
        {
            byte[] buffer = new byte[password.Length + SaltLength];
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            Array.Copy(passwordBytes, buffer, password.Length);
            Array.Copy(Salt, 0, buffer, password.Length, Salt.Length);

            return buffer;
        }
    }
}