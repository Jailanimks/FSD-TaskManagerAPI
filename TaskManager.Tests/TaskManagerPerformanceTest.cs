using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataLayer;
using TaskManager.BusinessLayer;
using System.Threading;
using NUnit.Framework;
using NUnit;
using System.Globalization;
using NBench.Util;
using NBench;

namespace TaskManager.Tests
{

   public  class TaskManagerPerformanceTest : PerformanceTestStuite<TaskManagerPerformanceTest>
    {
        #region Variables
        private Counter _counter;
        private ITaskRepository taskRepository = new TaskRepository();
        private List<TaskData> randomTasks = new List<TaskData>();
        #endregion

        #region Setup
        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            _counter = context.GetCounter("TaskCounter");
           randomTasks = SetupTasks();
        
        }

        public List<TaskData> SetupTasks()
        {
            List<TaskData> tasks = taskRepository.GetAllTasks().ToList();
            return tasks;
        }
        #endregion
    
        [PerfBenchmark(Description = "Test to ensure get all Tasks.",NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TaskCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("TaskCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("TaskCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]

        public void GetAllTasksBenchmark(BenchmarkContext context)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            int countTask = randomTasks.Count;
            List<TaskData> allTasks = taskRepository.GetAllTasks().ToList();
            _counter.Increment();

        }

        
        [PerfBenchmark(Description = "Test to ensure Add a Task.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TaskCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("TaskCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("TaskCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void AddTaskBenchmark()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            TaskData newTask = new TaskData()
            {
                TaskName = "Testing1",
                ParentTaskId = 5,
                Priority = 10,
                StartDate = DateTime.ParseExact("8/25/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("9/26/2018", "M/d/yyyy", CultureInfo.InvariantCulture)
            };

            taskRepository.AddTask(newTask);
            _counter.Increment();
        }



        [PerfBenchmark(Description = "Test to ensure Update a Task.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TaskCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("TaskCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("TaskCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void UpdateTaskBenchmark()
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
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Test to ensure to delete a Task.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TaskCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("TaskCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("TaskCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void DeleteTaskBenchmark()
        {
            int maxTaskID = randomTasks.Max(a => a.TaskId);
            TaskData lastTask = randomTasks.Last();
            taskRepository.RemoveTask(lastTask.TaskId);
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Test to ensure to get a Task by TaskID.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TaskCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("TaskCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("TaskCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void GetTaskByIdBenchmark()
        {
            TaskData firstTask = randomTasks.First();
            TaskData getTask = taskRepository.GetTaskById(firstTask.TaskId);

      
            _counter.Increment();
        }


        [PerfCleanup]
        public void Cleanup(BenchmarkContext context)
        {
         
            taskRepository = null;
        }

    }
}
