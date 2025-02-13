using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MandateThat;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace StatementIQ.Common.Web.Extensions
{
    public static class CertificateExtensions
    {
        private static readonly IDictionary<string, Action<RSA, byte[]>> KeyTypes =
            new Dictionary<string, Action<RSA, byte[]>>
            {
                {
                    "BEGIN PRIVATE KEY",
                    (rsa, privateKeyBytes) => { rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _); }
                },
                {
                    "BEGIN RSA PRIVATE KEY",
                    (rsa, privateKeyBytes) => { rsa.ImportRSAPrivateKey(privateKeyBytes, out _); }
                }
            };

        public static async Task UsePemCertificate(this ListenOptions listenOptions)
        {
            Mandate.That(listenOptions, nameof(listenOptions)).IsNotNull();

            var publicKeyPath = Environment.GetEnvironmentVariable(
                "Public_Certificate_Path");

            var privateKeyPath = Environment.GetEnvironmentVariable(
                "Private_Certificate_Path");

            // OpenSSL / Cert-Manager / Kubernetes-style Certificates
            if (!string.IsNullOrEmpty(publicKeyPath) && !string.IsNullOrEmpty(privateKeyPath))
            {
                var cert = await LoadPemCertificate(publicKeyPath, privateKeyPath)
                    .ConfigureAwait(false);

                listenOptions.UseHttps(cert);

                return;
            }

            var pfxPath = Environment.GetEnvironmentVariable(
                "ASPNETCORE_Kestrel__Certificates__Default__Path");

            var pfxPassword = Environment.GetEnvironmentVariable(
                "ASPNETCORE_Kestrel__Certificates__Default__Password");

            listenOptions.UseHttps(new X509Certificate2(pfxPath, pfxPassword));
        }

        private static async Task<X509Certificate2> LoadPemCertificate(
            string certificatePath, string privateKeyPath)
        {
            Mandate.That(certificatePath, nameof(certificatePath)).IsNotNullOrWhiteSpace();
            Mandate.That(privateKeyPath, nameof(privateKeyPath)).IsNotNullOrWhiteSpace();

            using var publicKey = new X509Certificate2(certificatePath);

            var privateKeyText = await File.ReadAllTextAsync(privateKeyPath);
            var privateKeyBlocks = privateKeyText.Split("-", StringSplitOptions.RemoveEmptyEntries);
            var privateKeyBytes = Convert.FromBase64String(privateKeyBlocks[1]);

            var keyPair = CreateKeyPair(publicKey, privateKeyBlocks, privateKeyBytes);

            return new X509Certificate2(keyPair.Export(X509ContentType.Pfx));
        }

        private static X509Certificate2 CreateKeyPair(
            X509Certificate2 publicKey, string[] privateKeyBlocks, byte[] privateKeyBytes)
        {
            Mandate.That(publicKey, nameof(publicKey)).IsNotNull();
            Mandate.That(privateKeyBlocks, nameof(privateKeyBlocks)).IsNotNull();
            Mandate.That(privateKeyBytes, nameof(privateKeyBytes)).IsNotNull();

            using var rsa = CreateRSAWithPrivateKey(privateKeyBlocks.First(), privateKeyBytes);

            return publicKey.CopyWithPrivateKey(rsa);
        }

        private static RSA CreateRSAWithPrivateKey(string keyType, byte[] privateKeyBytes)
        {
            Mandate.That(keyType, nameof(keyType)).IsNotNullOrWhiteSpace();
            Mandate.That(privateKeyBytes, nameof(privateKeyBytes)).IsNotNull();

            var rsa = RSA.Create();

            KeyTypes.TryGetValue(keyType, out var action);
            action?.Invoke(rsa, privateKeyBytes);

            return rsa;
        }
    }
}