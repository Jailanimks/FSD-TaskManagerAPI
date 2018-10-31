using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Description;
using TaskManager.DataLayer;
using TaskManager.BusinessLayer;
using System.Web.Http.Cors;

namespace TaskManager.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TaskAPIController : ApiController
    {

        ITaskRepository objTaskRepo = new TaskRepository();


        // GET: api/Task
        public List<TaskData> GetTasks()
        {
            List<TaskData> taskDetail = null;  
            try
            {
                taskDetail = objTaskRepo.GetAllTasks();
            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }

            return taskDetail;
        }


        // GET: api/Task/5
        [ResponseType(typeof(TaskData))]
        public IHttpActionResult GetTask(int id)
        {
            IHttpActionResult retResult = null;
            TaskData taskData = new TaskData();
            try
            {
                taskData = objTaskRepo.GetTaskById(id);
                if (taskData == null)
                {
                    retResult =  NotFound();
                }

                retResult= Ok(taskData);

            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }

            return retResult;
        }



        // PUT: api/Task/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTask(int id, TaskData objtask)
        {
            
            TaskData taskData = new TaskData();
            if (!ModelState.IsValid)
            {
                return  BadRequest(ModelState);
            }

            if (id != objtask.TaskId)
            {
                return BadRequest();
            }
            
            try
            {
                this.objTaskRepo.EditTask(objtask);
            }
            catch (DbUpdateConcurrencyException)
            {
                taskData = objTaskRepo.GetTaskById(id);
                if (taskData == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }



        // POST: api/Task
        [ResponseType(typeof(TaskData))]
        public IHttpActionResult PostTask(TaskData objtask)
        {
            TaskData taskData = new TaskData();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
         
            try
            {
                this.objTaskRepo.AddTask(objtask);
            }
            catch (DbUpdateException)
            {
                taskData = objTaskRepo.GetTaskById(objtask.TaskId);

                if (taskData == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = objtask.TaskId }, objtask);
        }

        // DELETE: api/Task/5
        [ResponseType(typeof(TaskData))]
        public IHttpActionResult DeleteTask(int id)
        {
            TaskData taskData = new TaskData();
            taskData = objTaskRepo.GetTaskById(id);
            if (taskData == null)
            {
                return NotFound();
            }
            this.objTaskRepo.RemoveTask(id);
            return Ok(taskData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                objTaskRepo = null;
            }
            base.Dispose(disposing);
        }
        
    }
}
