using FrontVentasUsuarioFinal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FrontVentasUsuarioFinal.Startup))]
namespace FrontVentasUsuarioFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //createUsuarios();
        }

        //private void createUsuarios()
        //{

        //    ApplicationDbContext context = new ApplicationDbContext();

        //    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        //    var user = new ApplicationUser();
        //    user.UserName = "S000001";
        //    user.NombreColaborador = "usuario de prueba";
        //    user.NumeroUsuario = 100;
        //    user.Email = "s000001@econtactsol.com";
        //    var okkk= userManager.Create(user, "S000001!");

        //}
    }
}
