﻿using LMS.Core;
using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMS.Controllers
{
    public class LMSController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Test ()
        {
            return Ok(Books.GetAll(null));
        }
        [HttpPost]
        public IHttpActionResult Add(Book b)
        {
            Books.Add(b);
            return Ok();
        }
    }
}
