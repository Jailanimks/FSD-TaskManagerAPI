using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataLayer;


namespace TaskManager.BusinessLayer
{
    public class TaskRepository : ITaskRepository
    {
        DatabaseContext taskContext = new DatabaseContext();
        private TaskData objdata = new TaskData();

        public void AddTask(TaskData objTask)
        {
            taskContext.Entry(objTask).State = System.Data.Entity.EntityState.Detached;
            taskContext.Tasks.Add(objTask);
            taskContext.SaveChanges();
        }

        public void EditTask(TaskData objTask)
        {

            var objdata = taskContext.Tasks.Find(objTask.TaskId);
            if (objdata != null)
            {
                objdata.TaskName = objTask.TaskName;
                objdata.ParentTaskId = objTask.ParentTaskId;
                objdata.StartDate = objTask.StartDate;
                objdata.EndDate = objTask.EndDate;
                objdata.Priority = objTask.Priority;
                objdata.IsTaskEnded = objTask.IsTaskEnded;
            }
            taskContext.Entry(objdata).CurrentValues.SetValues(objTask); 
            taskContext.SaveChanges();
        }

        public IQueryable<TaskData> GetAllTasks()
        {
            return taskContext.Tasks;
        }
                
        public TaskData GetTaskById(int Id)
        {
            objdata = (from obTask in taskContext.Tasks where obTask.TaskId == Id select obTask).FirstOrDefault();
            return objdata;
        }

        public void RemoveTask(int Id)
        {
            taskContext.Tasks.Remove(taskContext.Tasks.Find(Id));
            taskContext.SaveChanges();      
        }
               
    }
}
