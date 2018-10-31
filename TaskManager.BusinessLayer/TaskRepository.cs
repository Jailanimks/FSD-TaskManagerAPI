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
       
    
        public void AddTask(TaskData objTask)
        {
            using (var taskContext = new DatabaseContext())
            { 
                 taskContext.Entry(objTask).State = System.Data.Entity.EntityState.Added;
                taskContext.Tasks.Add(objTask);
                taskContext.SaveChanges();
            }
        }

        public void EditTask(TaskData objTask)
        {
            TaskData objdata = new TaskData();
            using (var taskContext = new DatabaseContext())
            {
                 objdata = taskContext.Tasks.Find(objTask.TaskId);
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
        }

        public List<TaskData> GetAllTasks()
        {
            List<TaskData> tData= null;
            using (var taskContext = new DatabaseContext())
            {
                tData= taskContext.Tasks.ToList();
            }
            return tData;
        }
                
        public TaskData GetTaskById(int Id)
        {
            TaskData objdata = new TaskData();
            objdata = null;
            using (var taskContext = new DatabaseContext())
            {
                objdata = (from obTask in taskContext.Tasks where obTask.TaskId == Id select obTask).FirstOrDefault();
                
            }
            return objdata;
        }

        public void RemoveTask(int Id)
        {
            using (var taskContext = new DatabaseContext())
            {
                taskContext.Tasks.Remove(taskContext.Tasks.Find(Id));
                taskContext.SaveChanges();
            }
        }
               
    }
}
