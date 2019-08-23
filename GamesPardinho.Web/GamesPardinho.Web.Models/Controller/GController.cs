using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Controller
{
    public class GController : Microsoft.AspNetCore.Mvc.Controller
    {
        public ObjectResult Internal(object value)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, value);
        }

        public StatusCodeResult Internal()
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
