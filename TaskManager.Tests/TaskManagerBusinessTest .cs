using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataLayer;
using TaskManager.BusinessLayer;
using NUnit.Framework;
using System.Globalization;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Tests
{
    [TestFixture]
    public class TaskManagerBusinessTest
    {

        #region Variables
        ITaskRepository taskRepository;
        List<TaskData> randomTasks;
        #endregion

        #region Setup
        [SetUp]
        public void Setup()
        {
            taskRepository = new TaskRepository();
            randomTasks = new List<TaskData>();
            randomTasks = SetupTasks();
        }

        public List<TaskData> SetupTasks()
        {
            List<TaskData> tasks = taskRepository.GetAllTasks().ToList();
            return tasks;
        }
        #endregion

        #region Test Methods
        [Test]
        public void TestAddTask()
        {
            int maxTaskIDBeforeAdd = randomTasks.Max(a => a.TaskId);
            TaskData newTask = new TaskData()
            {
                TaskName = "Testing1", 
                ParentTaskId = 5,
                Priority = 10,
                StartDate = DateTime.ParseExact("8/25/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("9/26/2018", "M/d/yyyy", CultureInfo.InvariantCulture)
            };
           
            taskRepository.AddTask(newTask);
            randomTasks = SetupTasks();
            TaskData dbTask = new TaskData();
            dbTask = randomTasks.Last();
            Assert.That(maxTaskIDBeforeAdd + 1, Is.EqualTo(randomTasks.Last().TaskId));

        }

        [Test]
        public void TestUpdateTask()
        {
            TaskData firstTask = randomTasks.First();
            TaskData UpdateTask = new TaskData()
            {
                TaskId = firstTask.TaskId,
                TaskName = firstTask.TaskName,
                ParentTaskId = firstTask.ParentTaskId,
                StartDate = DateTime.ParseExact("9/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("11/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                Priority = firstTask.Priority
            };
            taskRepository.EditTask(UpdateTask);
         
          
            Assert.AreEqual(firstTask.TaskId, UpdateTask.TaskId);
            Assert.That(firstTask.StartDate,Is.EqualTo(DateTime.ParseExact("9/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture)));
            Assert.That(firstTask.EndDate, Is.EqualTo(DateTime.ParseExact("11/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture)));

        }

        [Test]
        public void TestDeleteTask()
        {
            int maxTaskID = randomTasks.Max(a => a.TaskId);
            TaskData lastTask = randomTasks.Last();

            taskRepository.RemoveTask(lastTask.TaskId);
            Assert.That(maxTaskID + 1, Is.GreaterThan(randomTasks.Max(a => a.TaskId)));
        }

        [Test]
        public void TestGetAllTasks()
        {
            int countTask = randomTasks.Count;
            List<TaskData> allTasks = taskRepository.GetAllTasks().ToList();
            Assert.IsNotNull(allTasks);
            Assert.AreEqual(countTask, allTasks.Count);
        }

        [Test]
        public void TestGetTaskById()
        {
            TaskData firstTask = randomTasks.First();
            TaskData getTask = taskRepository.GetTaskById(firstTask.TaskId);
            Assert.IsNotNull(getTask);
            Assert.AreEqual(firstTask.TaskId, getTask.TaskId);

        }
        #endregion
    }
}
