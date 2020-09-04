using System;
using System.Security.Cryptography;
using System.Text;
using SQLite;

namespace Demo.Models
{
    public class UserModel
    {
        [PrimaryKey]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Email { get; set; }

        [Ignore]
        public string Password { get; set; }

        public string EncryptedPassword { get; set; }

        public UserModel()
        {
        }

        public string CryptPassword(string password)
        {
            var key = "SolutionsDemo@" + DateTime.UtcNow.ToString("yyyy-MM-dd");

            var inputArray = Encoding.UTF8.GetBytes(password);
            var tripleDES = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var cTransform = tripleDES.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }
}
