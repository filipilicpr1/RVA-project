using Server.Enums;

namespace Server.Interfaces.TokenMakerInterfaces
{
    public interface ITokenMakerFactory
    {
        ITokenMaker CreateTokenMaker(ETokenMaker tokenMaker);
    }
}
