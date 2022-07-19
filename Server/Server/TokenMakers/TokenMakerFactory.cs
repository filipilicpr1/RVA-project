using Server.Enums;
using Server.Interfaces.TokenMakerInterfaces;

namespace Server.TokenMakers
{
    public class TokenMakerFactory : ITokenMakerFactory
    {
        public ITokenMaker CreateTokenMaker(ETokenMaker tokenMaker)
        {
            if(tokenMaker == ETokenMaker.JWT)
            {
                return new JWTMaker();
            }
            return null;
        }
    }
}
