using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Artour.WebAPI.Controllers
{
    [Route("api/service")]
    public class ServiceController : ControllerBase
    {
        [HttpGet("connection")]
        public ActionResult CheckConnection()
        {
            return Ok();
        }

        [HttpGet("restart")]
        public ActionResult Restart()
        {
            Task.Run(() =>
            {
                Thread.Sleep(500);
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = "cmd.exe";
                myProcess.StartInfo.Arguments = @"/C cd " + Directory.GetCurrentDirectory() + " & start.bat";
                myProcess.Start();
                Environment.Exit(0);
            });

            return Ok();
        }
    }
}
