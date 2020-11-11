using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace APIADS.Controllers
{
    public class BaseController: ControllerBase
    {
        public BaseController()
        {
        }

        /// <summary>
        /// Obtiene el identificador de la empresa actual de la autorización.
        /// </summary>
        /// <returns>Identificador de la empresa.</returns>
        public int GetCveEmpresa()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    string id = claims.Where(p => p.Type == "CveEmpresa").FirstOrDefault()?.Value;

                    if (!string.IsNullOrEmpty(id))
                    {
                        return int.Parse(id);
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Obtiene el identificador del usuario actual de la autorización.
        /// </summary>
        /// <returns>Identificador del usuario.</returns>
        public int GetCveUsuario()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    string id = claims.Where(p => p.Type == "CveUsuario").FirstOrDefault()?.Value;

                    if (!string.IsNullOrEmpty(id))
                    {
                        return int.Parse(id);
                    }
                }
            }

            return -1;
        }
    }
}
