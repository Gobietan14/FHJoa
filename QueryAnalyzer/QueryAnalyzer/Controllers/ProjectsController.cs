using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueryAnalyzer.Models;
using QueryAnalyzer.Utilities;
using QueryAnalyzer.Backend;
using System.Collections.Generic;
using QueryAnalyzer.Backend.Export;
using Microsoft.AspNetCore.Http;
using QueryAnalyzer.Backend.Import;

namespace QueryAnalyzer.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly QueryAnalyzerContext _context;
        private const string SessionExcelData = "SessionExcelData";
        private IExport exportService;
        private IImport importService;

        public ProjectsController(QueryAnalyzerContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var project = await _context.Project?.LastOrDefaultAsync();
            if (project == null)
            {
                project = new Project();
                project.Issues.Add(new Issue()
                {
                    Name = "Bla",
                    Description = "123",
                    ID = 1
                });
            }
            else
            {
                var credential =  await _context.Credential.FirstOrDefaultAsync(e => e.ID == project.CredentialID);
                project.Credential = credential;
                var issues = await _context.Issue.Where(e => e.ProjectId == project.ID).ToListAsync();
                project.Issues = issues;
                //var credential = await _context.Credential.Where(e => e.)
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Connect([Bind("ID, Credential, Issues")] Project project)
        {
            if (ModelState.IsValid)
            {
                importService = ImportFactory.GetImportFrom("jira");
                //importService.Connect(new Credential()
                //{
                //    Uri = "https://queryexport.atlassian.net",
                //    Username = "anne.katrin.gobiet@gmail.com",
                //    Password = "Anne1234!"
                //});
                //if (project.ID == 0)
                //{
                //    _context.Add(project);
                //    await _context.SaveChangesAsync();
                //}
                importService.Connect(new Credential()
                {
                    Uri = project.Credential.Uri,
                    Username = project.Credential.Username,
                    Password = project.Credential.Password
                });

                var res = await importService.GetIssuesForProject();
                project.Issues = res;

                _context.Add(project);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExportData([Bind("ID, Credential, Issues")] Project project)
        {

            var resultProj = await _context.Project.FirstOrDefaultAsync(e => e.ID == project.ID);
            if(resultProj!= null)
            {
                resultProj.Issues = await _context.Issue.Where(e => e.ProjectId == resultProj.ID).ToListAsync();
            exportService = ExportFactory.GetExportTo("xlsx");

            var stream = exportService.Generate(resultProj);
            var response = File(stream, "application/octet-stream", "excelExport.xlsx"); // FileStreamResult
            return response;
            }
            return null;
        }
    }
}
