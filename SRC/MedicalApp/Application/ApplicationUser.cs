using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalApp.Application
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        // Adicione outras propriedades personalizadas aqui
    }
}
