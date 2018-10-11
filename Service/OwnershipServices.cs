using Data.Infrastructure;
using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IOwnershipService
    {

        IEnumerable<Ownership> GetOwnerships();
        Ownership GetOwnershipById(int OwnershipId);
        void CreateOwnership(Ownership Ownership);
        void EditOwnership(Ownership OwnershipToEdit);
        void DeleteOwnership(int OwnershipId);
        void SaveOwnership();

    }
    public class OwnershipService : IOwnershipService
    {
        #region Field
        private readonly IOwnershipRepository OwnershipRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public OwnershipService(IOwnershipRepository OwnershipRepository, IUnitOfWork unitOfWork)
        {
            this.OwnershipRepository = OwnershipRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Ownership> GetOwnerships()
        {
            var Ownerships = OwnershipRepository.GetAll();
            return Ownerships;
        }

        public Ownership GetOwnershipById(int OwnershipId)
        {
            var Ownership = OwnershipRepository.GetById(OwnershipId);
            return Ownership;
        }

        public void CreateOwnership(Ownership Ownership)
        {
            OwnershipRepository.Add(Ownership);
            SaveOwnership();
        }

        public void EditOwnership(Ownership OwnershipToEdit)
        {
            OwnershipRepository.Update(OwnershipToEdit);
            SaveOwnership();
        }

        public void DeleteOwnership(int OwnershipId)
        {
            //Get Ownership by id.
            var Ownership = OwnershipRepository.GetById(OwnershipId);
            if (Ownership != null)
            {
                OwnershipRepository.Delete(Ownership);
                SaveOwnership();
            }
        }

        public void SaveOwnership()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
