using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Eet.Dto
{
    public class Certificate
    {
        public Certificate(string password, byte[] data, bool useMachineKeyStore = false)
        {
            X509Certificate2 = new X509Certificate2(data, password, GetKeyStorageFlags(useMachineKeyStore));
            PrivateKey = X509Certificate2.GetRSAPrivateKey() ?? throw new ArgumentException("The provided certificate does not have an RSA key.");
        }

        public RSA PrivateKey { get; }

        public X509Certificate2 X509Certificate2 { get; }

        private X509KeyStorageFlags GetKeyStorageFlags(bool useMachineKeyStore)
        {
            if (useMachineKeyStore)
            {
                return X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet;
            }

            return X509KeyStorageFlags.DefaultKeySet;
        }
    }
}
