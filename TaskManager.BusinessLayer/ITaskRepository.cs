using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataLayer;

namespace TaskManager.BusinessLayer
{
   public interface ITaskRepository
    {
        void AddTask(TaskData objTask);
        void EditTask(TaskData objTask);
        void RemoveTask(int Id);
        List<TaskData> GetAllTasks();
        TaskData GetTaskById(int Id);
    }
}
