namespace H4TestSikkerhedApp.Code
{
    public class AsymetriskEncrypter
    {
        public static string Encrypt(string textToEncrypt, string publicKey)
        {
            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);

                byte[] byteArrayTextToEncrypt = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                byte[] encryptedDataAsByteArray = rsa.Encrypt(byteArrayTextToEncrypt, true);
                var encryptedDataAsString = Convert.ToBase64String(encryptedDataAsByteArray);

                return encryptedDataAsString;
            }
        }
    }
}
