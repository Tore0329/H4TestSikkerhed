namespace H4TestSikkerhedApp.Code
{
    public class AsymetriskEncryptionHandler
    {
        private string _privateKey;
        private string _publicKey;

        public AsymetriskEncryptionHandler()
        {
            if (!File.Exists("privateKey.pem"))
            {
                using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider())
                {
                    _privateKey = rsa.ToXmlString(true);
                    _publicKey = rsa.ToXmlString(false);

                    File.WriteAllText("privateKey.pem", _privateKey);
                    File.WriteAllText("publicKey.pem", _publicKey);
                }
            }
            else
            {
                _privateKey = File.ReadAllText("privateKey.pem");
                _publicKey = File.ReadAllText("publicKey.pem");
            }
        }

        public string EncryptAsymetrisk(string textToEncrypt)
        {
            return AsymetriskEncrypter.Encrypt(textToEncrypt, _publicKey);
        }

        public string DecryptAsymetrisk(string textToDecrypt)
        {
            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_privateKey);

                byte[] byteArrayTextToDecrypt = Convert.FromBase64String(textToDecrypt);
                byte[] decryptedDataAsByteArray = rsa.Decrypt(byteArrayTextToDecrypt, true);
                string decryptedDataAsString = System.Text.Encoding.UTF8.GetString(decryptedDataAsByteArray);

                return decryptedDataAsString;
            }
        }
    }
}
