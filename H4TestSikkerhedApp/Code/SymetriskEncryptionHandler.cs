using Microsoft.AspNetCore.DataProtection;

namespace H4TestSikkerhedApp.Code
{
    public class SymetriskEncryptionHandler
    {
        // builder.Services.AddDataProtection(); skal tilføjes til servicen.
        private readonly IDataProtector _proctor;

        public SymetriskEncryptionHandler(IDataProtectionProvider protector)
        {
            _proctor = protector.CreateProtector("h4serversideprogrammering3");
        }

        public string EncryptSymetrisk(string textToEncrypt) =>
            _proctor.Protect(textToEncrypt);

        public string DecryptSymetrisk(string textToDecrypt) =>
            _proctor.Unprotect(textToDecrypt);
    }
}
