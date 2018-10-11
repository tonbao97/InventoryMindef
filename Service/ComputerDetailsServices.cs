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
    public interface IComputerDetailsService
    {

        IEnumerable<ComputerDetails> GetComputerDetailss();
        ComputerDetails GetComputerDetailsById(int ComputerDetailsId);
        void CreateComputerDetails(ComputerDetails ComputerDetails);
        void EditComputerDetails(ComputerDetails ComputerDetailsToEdit);
        void DeleteComputerDetails(int ComputerDetailsId);
        void SaveComputerDetails();

    }
    public class ComputerDetailsService : IComputerDetailsService
    {
        #region Field
        private readonly IComputerDetailsRepository ComputerDetailsRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public ComputerDetailsService(IComputerDetailsRepository ComputerDetailsRepository, IUnitOfWork unitOfWork)
        {
            this.ComputerDetailsRepository = ComputerDetailsRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<ComputerDetails> GetComputerDetailss()
        {
            var ComputerDetailss = ComputerDetailsRepository.GetAll();
            return ComputerDetailss;
        }

        public ComputerDetails GetComputerDetailsById(int ComputerDetailsId)
        {
            var ComputerDetails = ComputerDetailsRepository.GetById(ComputerDetailsId);
            return ComputerDetails;
        }

        public void CreateComputerDetails(ComputerDetails ComputerDetails)
        {
            ComputerDetailsRepository.Add(ComputerDetails);
            SaveComputerDetails();
        }

        public void EditComputerDetails(ComputerDetails ComputerDetailsToEdit)
        {
            ComputerDetailsRepository.Update(ComputerDetailsToEdit);
            SaveComputerDetails();
        }

        public void DeleteComputerDetails(int ComputerDetailsId)
        {
            //Get ComputerDetails by id.
            var ComputerDetails = ComputerDetailsRepository.GetById(ComputerDetailsId);
            if (ComputerDetails != null)
            {
                ComputerDetailsRepository.Delete(ComputerDetails);
                SaveComputerDetails();
            }
        }

        public void SaveComputerDetails()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
