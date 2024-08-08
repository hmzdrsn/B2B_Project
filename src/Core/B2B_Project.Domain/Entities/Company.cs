using B2B_Project.Domain.Common;
using B2B_Project.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string CompanyName { get; set; }
        public string? RegistrationNumber { get; set; } // Şirketin ticaret sicil numarası.
        public string? TaxID { get; set; } //Vergi kimlik numarası.
        public string? Industry { get; set; } // Şirketin faaliyet gösterdiği sektör.
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Logo { get; set; } //image url olablir.
        public Guid PrimaryAppUserID { get; set; } //birincil iletişim için şirket kişisi
        public AppUser AppUser { get; set; }
        public Guid? SecondaryAppUserID { get; set; }//ikincil şirket kişisi
        public AppUser? AppUser2{ get; set; }
    }
}
