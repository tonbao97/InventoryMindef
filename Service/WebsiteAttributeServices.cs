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
    public interface IWebsiteAttributeService
    {

        IEnumerable<WebsiteAttribute> GetWebsiteAttributes();
        WebsiteAttribute GetWebsiteAttributeById(int WebsiteAttributeId);
        void CreateWebsiteAttribute(WebsiteAttribute WebsiteAttribute);
        void EditWebsiteAttribute(WebsiteAttribute WebsiteAttributeToEdit);
        void DeleteWebsiteAttribute(int WebsiteAttributeId);
        void SaveWebsiteAttribute();

    }
    public class WebsiteAttributeService : IWebsiteAttributeService
    {
        #region Field
        private readonly IWebsiteAttributeRepository WebsiteAttributeRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public WebsiteAttributeService(IWebsiteAttributeRepository WebsiteAttributeRepository, IUnitOfWork unitOfWork)
        {
            this.WebsiteAttributeRepository = WebsiteAttributeRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<WebsiteAttribute> GetWebsiteAttributes()
        {
            var WebsiteAttributes = WebsiteAttributeRepository.GetAll();
            return WebsiteAttributes;
        }

        public WebsiteAttribute GetWebsiteAttributeById(int WebsiteAttributeId)
        {
            var WebsiteAttribute = WebsiteAttributeRepository.GetById(WebsiteAttributeId);
            return WebsiteAttribute;
        }

        public void CreateWebsiteAttribute(WebsiteAttribute WebsiteAttribute)
        {
            WebsiteAttributeRepository.Add(WebsiteAttribute);
            SaveWebsiteAttribute();
        }

        public void EditWebsiteAttribute(WebsiteAttribute WebsiteAttributeToEdit)
        {
            WebsiteAttributeRepository.Update(WebsiteAttributeToEdit);
            SaveWebsiteAttribute();
        }

        public void DeleteWebsiteAttribute(int WebsiteAttributeId)
        {
            //Get WebsiteAttribute by id.
            var WebsiteAttribute = WebsiteAttributeRepository.GetById(WebsiteAttributeId);
            if (WebsiteAttribute != null)
            {
                WebsiteAttributeRepository.Delete(WebsiteAttribute);
                SaveWebsiteAttribute();
            }
        }

        public void SaveWebsiteAttribute()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
