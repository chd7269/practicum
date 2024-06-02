using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Code;

namespace MoneySystemServer.Controllers
{
    [IsActive]
    public class TaskController : GlobalController
    {
        private ITaskService TaskService;
        public TaskController( ITaskService taskService)
        {
            this.TaskService = taskService;
        }

        [HttpGet]
        public GResult<List<TaskDTO>> GetTasks()
        {
            return Success(TaskService.GetTasks(UserId.Value));

        }

        [HttpPost]
        public Result AddTask(TaskDTO task)
        {
            TaskService.AddTask(task, UserId.Value);
            return Success();
        }
        [HttpPut]
        public Result UpdateTask(TaskDTO task)
        {
            TaskService.UpdateTask(task);
            return Success();
        }

        [HttpDelete("{Id}")]
        public Result DeleteDebt(int Id)
        {
            var isTaskExist = TaskService.DeleteTask(Id);
            if (!isTaskExist)
                return Fail();
            return Success();
        }

    }
}
