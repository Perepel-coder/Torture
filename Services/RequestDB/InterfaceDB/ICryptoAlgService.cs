using Models;
using Services.Models;
using System.Collections.Generic;

namespace Services.RequestDB.InterfaceDB
{
    public interface ICryptoAlgService
    {
        IEnumerable<CryptoAlg_S> GetAlgs(string TypeKey);
        IEnumerable<CryptoAlg_S> GetAlgs();
        IEnumerable<Mode_S> GetModes();
        CryptoAlg_S GetAlg(int id);
        Mode_S GetMode(string Name);
        public IEnumerable<Mode_S>? GetModes(int numofkeyID);
    }
}
