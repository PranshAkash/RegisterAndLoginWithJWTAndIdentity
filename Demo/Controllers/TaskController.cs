using Demo.Helper;
using Hangfire;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet(nameof(ScheduleJob))]
        public IActionResult ScheduleJob()
        {
            try
            {
                RecurringJob.AddOrUpdate(() => _taskService.TakeBackup(), CronExpression.Every_15_Min);
                return Ok("Scheduled");
            }
            catch (Exception ex)
            {
                return Ok("Something went wrong");
            }
        }
    }
}
