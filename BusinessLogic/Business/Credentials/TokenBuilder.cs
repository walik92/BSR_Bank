using System;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Threading.Tasks;
using BusinessLogic.Interfaces.Credentials;
using BusinessLogic.Model;
using RepozytoriumDB.IRepository;

namespace BusinessLogic.Business.Credentials
{
    /// <summary>
    ///     Budowanie tokenu
    /// </summary>
    public class TokenBuilder : ITokenBuilder
    {
        private readonly ICredentialsValidator _credentialsValidator;
        private readonly ITokenRepository _tokenRepository;

        private readonly int _tokenSize = 128;
        private readonly ITokenValidator _tokenValidator;

        public TokenBuilder(ITokenRepository tokenRepository, ICredentialsValidator credentialsValidator,
            ITokenValidator tokenValidator)
        {
            _tokenRepository = tokenRepository;
            _credentialsValidator = credentialsValidator;
            _tokenValidator = tokenValidator;
        }

        public async Task<TokenModel> Build(CredentialModel creds)
        {
            var client = await _tokenRepository.ClientRepository.GetByIdAsync(creds.Id);

            if (client == null)
                throw new FaultException($"Client by Id {creds.Id} isn't exist");

            if (!_credentialsValidator.IsValid(creds, client))
                throw new FaultException("Wrong password");

            var tokenValue = BuildSecureToken();
            _tokenRepository.Add(tokenValue, client);
            await _tokenRepository.Save();

            var token = new TokenModel
            {
                TimeToLive = _tokenValidator.GetTokenTimeToLive(),
                Value = tokenValue
            };

            return token;
        }

        private string BuildSecureToken()
        {
            var buffer = new byte[_tokenSize];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetNonZeroBytes(buffer);
            }
            return Convert.ToBase64String(buffer);
        }
    }
}