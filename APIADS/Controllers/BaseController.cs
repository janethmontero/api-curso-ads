using Microsoft.AspNetCore.Mvc;

namespace APIADS.Controllers
{
    public class BaseController: ControllerBase
    {
        public BaseController()
        {
        }

        public int GetCveEmpresa()
        {
            return -1;
        }

        public int GetCveUsuario()
        {
            return -1;
        }
    }
}
