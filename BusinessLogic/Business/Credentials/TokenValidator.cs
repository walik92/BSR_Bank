using System;
using System.Configuration;
using BusinessLogic.Interfaces.Credentials;
using RepozytoriumDB.IRepository;

namespace BusinessLogic.Business.Credentials
{
    /// <summary>
    ///     Walidacja tokenu
    /// </summary>
    public class TokenValidator : ITokenValidator
    {
        private readonly int _defaultTokenTimeToLive = 300;
        private readonly ITokenRepository _tokenRepository;
        private readonly int _tokenTimeToLive;

        public TokenValidator(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
            if (!int.TryParse(ConfigurationManager.AppSettings["TokenTTL"], out _tokenTimeToLive))
                _tokenTimeToLive = _defaultTokenTimeToLive;
        }

        public bool IsValid(string token)
        {
            var tokenDto = _tokenRepository.GetByValueToken(token);

            return tokenDto != null && !IsExpired(tokenDto.CreateDate);
        }

        public int GetTokenTimeToLive()
        {
            return _tokenTimeToLive;
        }

        private bool IsExpired(DateTime createDateTime)
        {
            var span = DateTime.Now - createDateTime;
            return span.TotalSeconds > _tokenTimeToLive;
        }
    }
}