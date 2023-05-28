using Models;
using Repo;
using Services.RequestDB.InterfaceDB;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestDB
{
    public class CryptoAlgService : ICryptoAlgService
    {
        private IRepository<Cryptoalgorithm> algRepo;
        private IRepository<NumOfKey> numKeyRepo;
        private IRepository<Mode> modeRepo;

        public CryptoAlgService(
            IRepository<Cryptoalgorithm> algRepo,
            IRepository<NumOfKey> numKeyRepo,
            IRepository<Mode> modeRepo)
        {
            this.algRepo = algRepo;
            this.modeRepo = modeRepo;
            this.numKeyRepo = numKeyRepo;
        }

        public CryptoAlg_S GetAlg(int id)
        {
            var alg = algRepo.Get(id);
            return new CryptoAlg_S
            {
                Name = alg.Name,
                Class = alg.Class,
                Flag = false
            };
        }

        public IEnumerable<CryptoAlg_S> GetAlgs()
        {
            return algRepo
                .GetAll()
                .Select(a => new CryptoAlg_S { 
                    Name = a.Name, 
                    Class = a.Class, 
                    Flag = false 
                });
        }

        public IEnumerable<CryptoAlg_S> GetAlgs(string TypeKey)
        {
            var classifications = numKeyRepo.GetAll().Where(c=>c.TypeKey == TypeKey);
            var algs = algRepo.GetAll();
            return algs.Join(classifications,
                a => a.NumOfKeyId,
                c => c.ID,
                (a, c) => new
                {
                    Name = a.Name,
                    Class = a.Class,
                    NumOfKeyId = c.ID,
                    Flag = false
                })
                .Where(a => a.NumOfKeyId != null)
                .Select(a => new CryptoAlg_S
                {
                    Name = a.Name,
                    Class = a.Class,
                    Flag = false
                });
        }

        public IEnumerable<Mode_S> GetModes()
        {
            return modeRepo
                .GetAll()
                .Select(m => new Mode_S
                {
                    Name = m.Name,
                    Class = m.Class,
                    Flag = false
                });
        }

        public IEnumerable<Mode_S>? GetModes(int numofkeyID)
        {
            numKeyRepo.Include("Mode");
            var modes = numKeyRepo.Get(numofkeyID).Mode;
            if(modes != null)
            {
                return modes.Select(m =>
                new Mode_S
                {
                    Name = m.Name,
                    Class = m.Class,
                    Flag = false
                });
            }
            return null;
        }

        public Mode_S GetMode(string Name)
        {
            var mode = modeRepo.GetAll()
                .Single(m => m.Name == Name);
            return new Mode_S
            {
                Name = mode.Name,
                Class = mode.Class
            };
        }
    }
}
