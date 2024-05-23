using System.Security.Cryptography;
using System.Text;

namespace H4TestSikkerhedApp.Code
{
    public enum HashedValueReturnFormats
    {
        SimpleString,
        ByteArrray,
        BitString,
        UtfString,
        HexadecimalString
    }

    public class HashingHandler
    {
        #region Standard hashing methods

        /// <summary>
        /// MD5 hashing is deprecated, but still OK for manipulation check.
        /// </summary>
        public dynamic MD5Hashing(string textToHash, HashedValueReturnFormats hashedValueReturnFormats)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(textToHash);
            byte[] hashedValue = md5.ComputeHash(inputBytes);

            return ReturnSpecifyedFormat(hashedValue, hashedValueReturnFormats);
        }

        public dynamic SHAHashing(string textToHash, HashedValueReturnFormats hashedValueReturnFormats)
        {
            // SH1 hashing is deprecated. Following is recommended istead: SHA256, SHA384, SHA3_256, SHA3_384, SHA3_512 and SHA512.
            //SHA1 sha = SHA1.Create();

            SHA256 sha = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(textToHash);
            byte[] hashedValue = sha.ComputeHash(inputBytes);

            return ReturnSpecifyedFormat(hashedValue, hashedValueReturnFormats);
        }

        public dynamic HMACHashing(string textToHash, HashedValueReturnFormats hashedValueReturnFormats)
        {
            byte[] myKey = Encoding.ASCII.GetBytes("NielsErMinFavoritLærer");
            byte[] inputBytes = Encoding.ASCII.GetBytes(textToHash);

            // - HMAC == Hash-based Massage Authentication Code
            // - Uses a hash function and a secret key.
            // - Uses "Generic cryptographic hash functions" to hash, any of these can apply : MD5, SHA1, SHA256, SHA384, SHA3_256, SHA3_384, SHA3_512 and SHA512.
            HMACSHA256 hmac = new HMACSHA256();
            //HMACSHA512 hmacSHA512 = new HMACSHA512();
            hmac.Key = myKey;

            byte[] hashedValue = hmac.ComputeHash(inputBytes);

            return ReturnSpecifyedFormat(hashedValue, hashedValueReturnFormats);
        }

        //public dynamic HashAndProcessData(string textToHash, HashedValueReturnFormats hashedValueReturnFormats)
        //{
        //    byte[] inputBytes = Encoding.ASCII.GetBytes(textToHash);
        //    HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA256;
        //    byte[] hashedValue = CryptographicOperations.HashData(hashAlgorithmName, inputBytes);

        //    return ReturnSpecifyedFormat(hashedValue, hashedValueReturnFormats);
        //}

        #endregion

        #region Advance hashing methods

        public dynamic PBKDF2Hashing(string textToHash, string salt, HashedValueReturnFormats hashedValueReturnFormats)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(textToHash);
            byte[] saltAsbytes = Encoding.ASCII.GetBytes(salt);

            var hashAlgorithm = new HashAlgorithmName("SHA256");

            byte[] hashedValue = Rfc2898DeriveBytes.Pbkdf2(inputBytes, saltAsbytes, 10, hashAlgorithm, 32);

            return ReturnSpecifyedFormat(hashedValue, hashedValueReturnFormats);
        }
     
        public string BCryptHashing(string textToHash)
        {
            return BCrypt.Net.BCrypt.HashPassword(textToHash);
        }

        public bool BCryptVerifyHashing(string textToHash, string hashedValue)
        {
            return BCrypt.Net.BCrypt.Verify(textToHash, hashedValue);

            //return BCrypt.Net.BCrypt.Verify(textToHash, hashedValue, true);

            //return BCrypt.Net.BCrypt.Verify(textToHash, hashedValue, true, BCrypt.Net.HashType.SHA256);
        }

        #endregion

        #region Formats for returning a hashed value 

        private dynamic ReturnSpecifyedFormat(byte[] hashedValue, HashedValueReturnFormats hashedValueReturnFormats)
        {
            switch (hashedValueReturnFormats)
            {
                case HashedValueReturnFormats.SimpleString:
                    return Convert.ToBase64String(hashedValue);
                case HashedValueReturnFormats.ByteArrray:
                    return hashedValue;
                case HashedValueReturnFormats.BitString:
                    return BitConverter.ToString(hashedValue);
                case HashedValueReturnFormats.UtfString:
                    return Encoding.UTF8.GetString(hashedValue, 0, hashedValue.Length);
                case HashedValueReturnFormats.HexadecimalString:
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in hashedValue)
                        sb.Append(b.ToString("X2"));
                    return sb.ToString();
                default:
                    return null;
            }
        }

        #endregion
    }
}
